using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayScreen : UIScreen
{
    [SerializeField] private WaveAnnouncement _waveAnnouncement;
    
    public WaveAnnouncement WaveAnnouncement => _waveAnnouncement;
}
