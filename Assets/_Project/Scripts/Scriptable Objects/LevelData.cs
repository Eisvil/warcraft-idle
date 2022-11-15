using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelData", menuName = "Level/Level Data", order = 1)]
public class LevelData : ScriptableObject
{
    public WaveData[] Waves;
}
