using System.Collections;
using UnityEngine;
using Cinemachine;
public class CameraScript : MonoBehaviour, IEventObservable
{
    

    [SerializeField]
    private CinemachineVirtualCamera pieceCamera, actionCamera;
    private CinemachineVirtualCamera _currentCamera;

    private PlayerInfo _player;
    private WeaponManager _weapon;

    [SerializeField]
    private float shakeIntensity, shakeTime;

    private void Awake()
    {
        _player = PlayerInfo.instance;
        _weapon = _player.Weapons;
        actionCamera.gameObject.SetActive(false);
        _currentCamera = pieceCamera;
        _currentCamera.gameObject.SetActive(true);
    }
   
    private void ZoomIn()
    {
        _currentCamera.gameObject.SetActive(false);
        _currentCamera = pieceCamera;
        _currentCamera.gameObject.SetActive(true);

    }
    private void ZoomOut()
    {
        _currentCamera.gameObject.SetActive(false);
        _currentCamera = actionCamera;
        _currentCamera.gameObject.SetActive(true);
        
    }

    public void ShakeCamera()
    {
        StartCoroutine(ShakeCoroutine(shakeIntensity, shakeTime));
    }

    private IEnumerator ShakeCoroutine(float intensity, float time)
    {
        CinemachineBasicMultiChannelPerlin channelPerlin =
            _currentCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

        channelPerlin.m_AmplitudeGain = shakeIntensity;

        yield return new WaitForSeconds(time);

        channelPerlin.m_AmplitudeGain = 0f;

    }

    private void ActivateCamera()
    {
        _currentCamera.gameObject.SetActive(false);
        if (_weapon.HasActiveWeapon())
        {
            _currentCamera = actionCamera;
        }
        else
        {
            _currentCamera = pieceCamera;
        }
        _currentCamera.gameObject.SetActive(true);
    }
    public void OnEnable()
    {
        if (_weapon)
        {
            _weapon.onDrawWeapon.AddListener(ZoomOut);
            _weapon.onHideWeapon.AddListener(ZoomIn);
        }
        ActivateCamera();

    }
    public void OnDisable()
    {
        if (_weapon)
        {
            _weapon.onDrawWeapon.RemoveListener(ZoomOut);
            _weapon.onHideWeapon.RemoveListener(ZoomIn);
        }

    }

}
