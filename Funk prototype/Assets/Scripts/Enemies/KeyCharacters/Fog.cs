using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class Fog : CharacterAction
{
    [SerializeField]
    private float increaseStep = .25f, minIntensityValue = 1.5f, maxIntensityValue = 4f;

    private SpriteRenderer _renderer;
    private Material _fogMaterial;

    private string _firstColorName = "_FogColor";
    private string _secondColorName = "_FogColor2";

    private float _intensityValue;

    private Color _firstCol;
    private Color _secondCol;

    private void Awake()
    {
        _renderer = GetComponent<SpriteRenderer>();
        _fogMaterial = _renderer.material;
        _firstCol = _fogMaterial.GetColor(_firstColorName);
        _secondCol = _fogMaterial.GetColor(_secondColorName);
       
    }

    private void OnEnable()
    {
        _intensityValue = minIntensityValue;
        SetIntensity(_intensityValue);
    }

    public override void Work()
    {
        IncreaseFog();
    }

    public override void Stop()
    {
        SetIntensity(minIntensityValue);
    }

    private void IncreaseFog()
    {
        if(_intensityValue >= maxIntensityValue)
        {
            return;
        }
        _intensityValue += increaseStep;
        SetIntensity(_intensityValue);
        
    }

    private void SetIntensity(float intensity)
    {
        _fogMaterial.SetColor(_firstColorName, _firstCol * intensity);
        _fogMaterial.SetColor(_secondColorName, _secondCol * intensity);
    }

   
}
