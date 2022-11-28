using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Audio;
using Random = UnityEngine.Random;

public class SoundManager : Singleton<SoundManager>
{
    [SerializeField] private SoundPlayer[] _soundPlayers;
    [SerializeField] private AudioSource _musicSourceTemplate;
    [SerializeField] private AudioMixer _master;
    [Header("References")]
    [SerializeField] private AudioClip[] _musics;
    
    private AudioSource _musicSource;

    public void Init()
    {
        foreach (var soundPlayer in _soundPlayers)
        {
            soundPlayer.Init();
        }

        _musicSource = Instantiate(_musicSourceTemplate, transform);
    }

    private void Start()
    {
        Init();
        
        PlayMusic(MusicName.MainTheme);
    }
    
    public void PlaySound(SoundName soundName)
    {
        _soundPlayers.First(soundPlayer => soundPlayer.SoundName == soundName).Play();
    }

    public void PlayMusic(MusicName musicName)
    {
        _musicSource.clip = _musics[(int)musicName];
        _musicSource.Play();
    }

    public void ChangeSoundsVolume(float normalizedValue)
    {
        _master.SetFloat("Sound", Mathf.Lerp(-40, 10, normalizedValue));
    }
    
    public void ChangeMusicVolume(float normalizedValue)
    {
        _master.SetFloat("Music", Mathf.Lerp(-40, 10, normalizedValue));
    }
}

[Serializable]
public enum SoundName
{
    SwordHit,
    ButtonClick
}

[Serializable]
public enum MusicName
{
    MainTheme
}
