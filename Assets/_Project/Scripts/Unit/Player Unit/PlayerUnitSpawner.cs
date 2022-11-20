using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerUnitSpawner : UnitSpawner<PlayerUnit>
{
    [SerializeField] private Transform _spawnPoint;

    public void Init()
    {
        SelectedIds = PlayerDeckManager.Instance.SelectedUnitsId;
        SelectedUnitLevels = PlayerDeckManager.Instance.SelectedUnitLevels;
        Templates = SelectedIds.Select(unitId => UnitDataStorage.Instance.TryGetPlayerUnit(unitId, UnitRace.Human)).ToList();
        PoolSizes = SelectedIds.Select(unitId => UnitDataStorage.Instance.TryGetUnitPoolSize(unitId, 0)).ToList();
        UnitsSpawnTime = SelectedIds.Select(unitId => UnitDataStorage.Instance.TryGetUnitSpawnTime(unitId, 0)).ToArray();
        UnitsTimer = new float[UnitsSpawnTime.Length];

        InitPool();
    }

    protected override void TryClear()
    {
        if(Pool.Count == 0) return;
        
        foreach (var unit in Pool)
        {
            Destroy(unit.gameObject);
        }
        
        Pool.Clear();
    }

    protected override void SpawnUnit(int id)
    {
        var unit = TryGetUnit(id);

        unit.Reset();
        unit.transform.position = _spawnPoint.position;
        unit.gameObject.SetActive(true);
        unit.TryFindTarget();
    }
}
