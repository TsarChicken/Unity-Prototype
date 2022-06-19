using UnityEngine;
using System;
using System.IO;
public class FileDataHandler
{
    private string _dataDirPath = "";
    private string _dataFileName = "";
    private bool _useEncryption = false;

    private readonly string _encryptionKeyWord = "word";

    public FileDataHandler(string dirPath, string fileName, bool useEncr)
    {
        _dataDirPath = dirPath;
        _dataFileName = fileName;
        _useEncryption = useEncr;
    }
    public bool DeleteSave()
    {
        string fullPath = Path.Combine(_dataDirPath, _dataFileName);
        if(File.Exists(fullPath) == false)
        {
            return false;
        }
        File.Delete(fullPath);
        return true;
    }
    private string EncryptDecrypt(string data)
    {
        string modifiedData = "";
        for (int i = 0; i < data.Length; i++)
        {
            modifiedData += (char)(data[i] ^ _encryptionKeyWord[i % _encryptionKeyWord.Length]);
        }
        return modifiedData;
    }
    public GameData Load()
    {
        string fullPath = Path.Combine(_dataDirPath, _dataFileName);
        GameData loadedData = null;
        var a = File.Exists(fullPath);
        if (File.Exists(fullPath))
        {
            try
            {
                string dataToLoad = "";
                using (FileStream stream = new FileStream(fullPath, FileMode.Open))
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        dataToLoad = reader.ReadToEnd();
                    }
                }
                if (_useEncryption)
                {
                    dataToLoad = EncryptDecrypt(dataToLoad);
                }
                loadedData = JsonUtility.FromJson<GameData>(dataToLoad);
            }
            catch(Exception e)
            {
                Debug.LogError("ERROR LOADING WITH FILE: " + fullPath + "\n" + e);
            }
        }
        return loadedData;
    }

    public void Save(GameData data)
    {
        string fullPath = Path.Combine(_dataDirPath, _dataFileName);
        try
        {
            Directory.CreateDirectory(Path.GetDirectoryName(fullPath));

            string dataToStore = JsonUtility.ToJson(data, true);

            if (_useEncryption)
            {
                dataToStore = EncryptDecrypt(dataToStore);
            }

            using (FileStream stream = new FileStream(fullPath, FileMode.Create))
            {
                using (StreamWriter writer = new StreamWriter(stream))
                {
                    writer.Write(dataToStore);
                }
            }
        }
        catch (Exception e)
        {
            Debug.LogError("ERROR WITH DIRECTORY: " + fullPath + "\n" + e);
        }
    }

  

}
