using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "WaveData", menuName = "Level/Wave Data", order = 1)]
public class WaveData : ScriptableObject
{
    public WaveType Type;
    public int[] UnitIds;
    public float[] UnitsSpawnTime;
    public int UnitPerksLevel;
}

[Serializable]
public enum WaveType
{
    Normal,
    Boss
}
