using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class IControllable : MonoBehaviour
{
    //public abstract void TakeControl();
    public void ControlView()
    {
        print("STUNNEDDDD");
    }

    public abstract void Block();

    public abstract void Unblock();

}
