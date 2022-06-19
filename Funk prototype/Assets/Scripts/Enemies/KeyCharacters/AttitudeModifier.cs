using UnityEngine;

public abstract class AttitudeModifier : MonoBehaviour, IDataPersistence
{
   
    public void LoadData(GameData data)
    {
        throw new System.NotImplementedException();
    }

    public void SaveData(ref GameData data)
    {
        throw new System.NotImplementedException();
    }
    public virtual void OnEnable()
    {

    }
    public virtual void OnDisable()
    {

    }
}
