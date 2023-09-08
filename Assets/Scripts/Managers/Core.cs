using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Data
{
    public void SaveData<T>(string path, T jsonClass) where T : class
    {
        File.WriteAllText(path, JsonUtility.ToJson(jsonClass));
    }

    public string LoadData(string path)
    {
        if (File.Exists(path) == true)
        {
            return File.ReadAllText(path);
        }
        else
        {
            return null;
        }
    }

    public T LoadData<T>(string path) where T : new()
    {
        if (File.Exists(path) == true)
        {
            return JsonUtility.FromJson<T>(File.ReadAllText(path));
        }
        else
        {
            return new T();
        }
    }
}

public class Core : MonoBehaviour
{
    Data Data { get; set; } = new Data();

    #region Singleton
    public static Core I { get; private set; } = null;

    private void Awake()
    {
        if (I == null)
        {
            I = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    #endregion

    public void Start()
    {
        
    }

    public void SaveData<T>(string path, T jsonClass) where T : class
    {
        Data.SaveData(path, jsonClass);
    }

    public string LoadData(string path)
    {
        return Data.LoadData(path);
    }

    public T LoadData<T>(string path) where T : new()
    {
        return Data.LoadData<T>(path);
    }
}
