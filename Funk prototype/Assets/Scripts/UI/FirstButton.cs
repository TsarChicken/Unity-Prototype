using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FirstButton : MonoBehaviour
{
    public Button primaryButton;

    private void OnEnable()
    {
        primaryButton.Select();
    }
}
