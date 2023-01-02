using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : Singleton<DataManager>
{
    private string _key = "Data";
    
    public Data Data;

    protected override void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public IEnumerator Load()
    {
        Data = PlayerPrefs.HasKey(_key) ? JsonUtility.FromJson<Data>(PlayerPrefs.GetString(_key)) : new Data();

        yield return new WaitForSeconds(0.3f);
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