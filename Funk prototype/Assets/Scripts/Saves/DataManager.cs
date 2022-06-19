using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class DataManager : Singleton<DataManager>
{
    [Header("File Storage Config")]
    [SerializeField] private string fileName = "data.game";
    [SerializeField] private bool useEncryption = true;

    private GameData _gameData;
    public List<IDataPersistence> _datas = new List<IDataPersistence>();
    private FileDataHandler _dataHandler;
    public override void Awake()
    {
        _dataHandler = new FileDataHandler(Application.dataPath, fileName, useEncryption);
    }

    public void NewGame()
    {
        _gameData = new GameData();
    }

    public bool DeleteSave()
    {
        return _dataHandler.DeleteSave();
    }

    public void LoadGame()
    {
        _gameData = _dataHandler.Load();

        if(_gameData == null)
        {
            NewGame();
        }
        for(int i = 0; i < _datas.Count; i++)
        {
            _datas[i].LoadData(_gameData);
        }
    }

    public void SaveGame()
    {
        for (int i = 0; i < _datas.Count; i++)
        {
            _datas[i].SaveData(ref _gameData);
        }
        _dataHandler.Save(_gameData);
    }

    public void FindAllDataPersistenceObjects()
    {
        IEnumerable<IDataPersistence> datas = FindObjectsOfType<MonoBehaviour>().
            OfType<IDataPersistence>();
       _datas = new List<IDataPersistence>(datas);

    }

   

}
