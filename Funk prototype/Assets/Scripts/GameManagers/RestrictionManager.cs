using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestrictionManager: MonoBehaviour
{
    private static RestrictionManager instance = null;

    private void Awake()
    {
        if (instance == null)
        { 
            instance = this; 
        }
        else if (instance == this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);

    }
    public static RestrictionManager Instance
    {
        get
        {
            return instance;
        }
    }

    public void Restrict()
    {
        Debug.Log("No Actions");
    }
}
