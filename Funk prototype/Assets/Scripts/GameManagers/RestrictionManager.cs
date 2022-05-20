using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestrictionManager: Singleton<RestrictionManager>
{

    public void Restrict()
    {
        GetComponent<AudioManager>().PlaySound();
    }
}
