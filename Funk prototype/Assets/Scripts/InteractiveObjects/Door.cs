using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : IInteractable
{
   
    public override void Interact()
    {
        Close();
    }
    private void Close()
    {
        StartCoroutine(SetClosed());
    }

  
    private IEnumerator SetClosed()
    {
        GetComponent<Transform>().localScale = Vector3.zero;
        yield return new WaitForSeconds(3f);
        GetComponent<Transform>().localScale = initialSize ;
    }
}
