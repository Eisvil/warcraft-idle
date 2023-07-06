using System.Linq;
using UnityEngine;

public class UnitDataStorage : Singleton<UnitDataStorage>
{
    [SerializeField] private UnitData[] _unitData;

    private bool CheckId(int id)
    {
        return _unitData.Any(data => data.Id == id);
    }
    
    public PlayerUnit TryGetPlayerUnit(int id)
    {
        return CheckId(id) == false ? null : _unitData.First(data => data.Id == id).PlayerTemplate;
    }
    
    public EnemyUnit TryGetEnemyUnit(int id)
    {
        return CheckId(id) == false ? null : _unitData.First(data => data.Id == id).EnemyTemplate;
    }

    public UnitStats TryGetUnitBasicStats(int id, int level)
    {
        return CheckId(id) == false ? null : _unitData.First(data => data.Id == id).BasicStats[level];
    }

    public int TryGetUnitPoolSize(int id, int level)
    {
        return CheckId(id) == false ? -1 : _unitData.First(data => data.Id == id).PoolSize[level];
    }
    
    public float TryGetUnitSpawnTime(int id, int level)
    {
        return CheckId(id) == false ? -1f : _unitData.First(data => data.Id == id).UnitsSpawnTime[level];
    }

    public UnitData TryGetUnitData(int id)
    {
        return CheckId(id) == false ? null : _unitData.First(data => data.Id == id);
    }
}
