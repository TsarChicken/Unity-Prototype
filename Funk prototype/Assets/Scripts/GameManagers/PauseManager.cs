using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseManager : MonoBehaviour
{
    [SerializeField]
    private PlayerEvents input;
    private void OnEnable()
    {
        input.onPause.AddListener(Pause);
    }

    private void OnDisable()
    {
        input.onPause.RemoveListener(Pause);

    }

    private void Pause()
    {
        print("paused");
        Application.Quit();
    }
}
