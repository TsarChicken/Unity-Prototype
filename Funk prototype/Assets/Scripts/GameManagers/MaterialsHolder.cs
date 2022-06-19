using UnityEngine;

public class MaterialsHolder : Singleton<MaterialsHolder>
{
    public override void Awake()
    {
    }
    public Material defaultMaterial;
    public Material hologramMaterial;
    public Material dissolveMaterial;
    public Material inactiveOutlineMaterial;
    public Material activeOutlineMaterial;
    public Material HurtMaterial;
    public Material DissolveMaterial;

}
