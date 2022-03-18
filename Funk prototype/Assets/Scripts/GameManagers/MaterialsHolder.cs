using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialsHolder : MonoBehaviour
{
    public static MaterialsHolder instance;
    public Material defaultMaterial;
    public Material hologramMaterial;
    public Material dissolveMaterial;
    public Material outlineMaterial;
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance == this)
        {
            Destroy(gameObject);
        }
    }
}
