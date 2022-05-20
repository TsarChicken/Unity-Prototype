using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialsHolder : Singleton<MaterialsHolder>
{
    public Material defaultMaterial;
    public Material hologramMaterial;
    public Material dissolveMaterial;
    public Material inactiveOutlineMaterial;
    public Material activeOutlineMaterial;

}
