using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : Singleton<DataManager>
{
    private bool _isLoaded = false;
    private string _key = "Data";
    
    public Data Data;

    public IEnumerator Load()
    {
        if(_isLoaded) yield break;
        
        Data = PlayerPrefs.HasKey(_key) ? JsonUtility.FromJson<Data>(PlayerPrefs.GetString(_key)) : new Data();
        _isLoaded = true;

        yield return null;
    }

    public void Save()
    {
        PlayerPrefs.SetString(_key, JsonUtility.ToJson(Data));
    }

    private void OnApplicationQuit()
    {
        Save();
    }
}

[Serializable]
public class Data
{
    public Data()
    {
        
    }
}