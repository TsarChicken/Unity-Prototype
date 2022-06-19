using UnityEngine;

public class CameraFixer : MonoBehaviour
{
    private void OnEnable()
    {
        transform.rotation = PlayerInfo.instance.transform.rotation;
    }
}
