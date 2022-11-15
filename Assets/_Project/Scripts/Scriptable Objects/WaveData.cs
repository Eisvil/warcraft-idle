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
    public int[] UnitLevels;
    public float[] UnitsSpawnTime;
    public int GoldReward;
    public int ExpReward;
}

[Serializable]
public enum WaveType
{
    Normal,
    Boss
}
