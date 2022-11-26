using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SoundPlayer : MonoBehaviour
{
    [SerializeField] private SoundName _soundName;
    [SerializeField] private AudioSource _sourceTemplate;
    [SerializeField] private int _sourcesCount;
    [SerializeField] private AudioClip[] _sounds;
    
    private List<AudioSource> _sources = new List<AudioSource>();

    public SoundName SoundName => _soundName;

    public void Init()
    {
        for (var i = 0; i < _sourcesCount; i++)
        {
            _sources.Add(Instantiate(_sourceTemplate, transform));
        }
    }
    
    private AudioSource TryGetSoundSource()
    {
        return _sources.FirstOrDefault(source => !source.isPlaying);
    }

    public void Play()
    {
        var source = TryGetSoundSource();
        
        if(source == null) return;
        
        source.pitch = Random.Range(0.7f, 1f);
        source.PlayOneShot(_sounds[Random.Range(0, _sounds.Length)]);
    }
}
