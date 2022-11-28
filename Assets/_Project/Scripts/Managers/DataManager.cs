using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : Singleton<DataManager>
{
    private bool _isLoaded = false;
    private string _key = "Data";
    
    public Data Data;

    protected override void Awake()
    {
        base.Awake();

        StartCoroutine(Load());
    }

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
    public float Gold;
    public int Gem;
    public int[] PermanentPerkLevels;
    public int SoundVolume;
    public int MusicVolume;

    public Data()
    {
        Gold = 0f;
        Gem = 0;
        PermanentPerkLevels = new int[Enum.GetNames(typeof(PerkName)).Length];
        SoundVolume = 8;
        MusicVolume = 8;
    }
}