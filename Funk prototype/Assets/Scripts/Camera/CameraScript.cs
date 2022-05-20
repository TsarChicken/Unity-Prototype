using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using DG.Tweening;
public class CameraScript : MonoBehaviour, ICamera, IEventObservable
{
    private PlayerInfo player;
    private WeaponManager weapon;
    private Rotator rotator;
    private CinemachineVirtualCamera virtualCam;
    [SerializeField]
    private float zoomIn, zoomOut;
    private float currentZoom = 0f;
    private float time = 0f;
    private void Awake()
    {
        player = PlayerInfo.instance;
        rotator = GetComponent<Rotator>();
        //StartCoroutine(Zoom(currentZoom, zoomIn));
        currentZoom = zoomIn;
        virtualCam = GetComponentInChildren<CinemachineVirtualCamera>();
        virtualCam.m_Lens.OrthographicSize = currentZoom;
        print("CAMERA SIZE   " + virtualCam.m_Lens.OrthographicSize);
    }
    private void Start()
    {
        print("CAMERA SIZE   " + virtualCam.m_Lens.OrthographicSize);

    }
    private void FixedUpdate()
    {
        //transform.rotation = player.transform.rotation;
    }

    public void RotateCamera()
    {

        //rotator.HandleRotation(player.PlayerPhysics.fallMultiplier);

    }
    public void SetPlayer()
    {
        player = PlayerInfo.instance;
        print("CAMERA PLAYER");
        weapon = player.Weapons;

    }

    public void HandleZoom()
    {
        float previousZoom = currentZoom;
        if (currentZoom == zoomIn)
        {
            currentZoom = zoomOut;
        }
        else
        {
            currentZoom = zoomIn;
        }
        VariablesBuffer.instance.CurrentCameraZoom = currentZoom;
        StartCoroutine(Zoom(previousZoom, currentZoom));
       
    }

    public void ZoomIn()
    {
        float previousZoom = currentZoom;
        currentZoom = zoomIn;
        VariablesBuffer.instance.CurrentCameraZoom = currentZoom;
        StartCoroutine(Zoom(previousZoom, currentZoom));

    }
    public void ZoomOut()
    {
        float previousZoom = currentZoom;
        currentZoom = zoomOut;
        VariablesBuffer.instance.CurrentCameraZoom = currentZoom;
        StartCoroutine(Zoom(previousZoom, currentZoom));
    }
    IEnumerator Zoom(float start, float end)
    {
        time = 0f;
        while (virtualCam.m_Lens.OrthographicSize != end)
        {
            virtualCam.m_Lens.OrthographicSize = Mathf.Lerp(start, end, time);
            time += Time.deltaTime;
            yield return null;
        }
        StopCoroutine(Zoom(start, end));
        yield return null;

    }


    public void GravityShake()
    {
        virtualCam.transform.DOShakePosition(30f);
    }

    public void OnEnable()
    {
        //transform.rotation = player.transform.rotation;
        virtualCam.m_Lens.OrthographicSize = VariablesBuffer.instance.CurrentCameraZoom;
        if (weapon)
        {
            weapon.onDrawWeapon.AddListener(ZoomOut);
            weapon.onHideWeapon.AddListener(ZoomIn);
        }      

    }
    public void OnDisable()
    {
        if (weapon)
        {
            weapon.onDrawWeapon.RemoveListener(ZoomOut);
            weapon.onHideWeapon.RemoveListener(ZoomIn);
        }

    }


}
