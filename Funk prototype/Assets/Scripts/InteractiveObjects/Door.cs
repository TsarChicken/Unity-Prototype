using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField]
    private Vector3 initialSize;
    void Awake()
    {
        initialSize = GetComponent<Transform>().localScale;
        GetComponentInChildren<Interactive>().act = Close;
    }
   


    public void Close()
    {
        StartCoroutine(SetClosed());
    }

  
    private IEnumerator SetClosed()
    {
        GetComponent<Transform>().localScale = Vector3.zero;
        yield return new WaitForSeconds(3f);
        GetComponent<Transform>().localScale = initialSize ;
        GetComponentInChildren<Interactive>().enabled = true;
    }
}
