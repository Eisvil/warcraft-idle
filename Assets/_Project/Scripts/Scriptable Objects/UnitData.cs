using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "UnitData", menuName = "Unit/UnitData", order = 1)]
public class UnitData: ScriptableObject
{
    public int Id;
    public UnitStats[] BasicStats;
    public int[] PoolSize;
    public float[] UnitsSpawnTime;
    public PlayerUnit[] PlayerTemplates;
    public EnemyUnit[] EnemyTemplates;
}
