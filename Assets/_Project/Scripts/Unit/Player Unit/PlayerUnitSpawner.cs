using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerUnitSpawner : UnitPool<PlayerUnit>
{
    [SerializeField] private Castle _castle;
    [SerializeField] private Transform _spawnPoint;

    private float[] _unitsSpawnTime;
    private float[] _unitsTimer;

    public void Init()
    {
        SelectedIds = PlayerDeckManager.Instance.SelectedUnitsId;
        Templates = SelectedIds.Select(unitId => UnitDataStorage.Instance.TryGetPlayerUnit(unitId, UnitRace.Human)).ToList();
        PoolSizes = SelectedIds.Select(unitId => UnitDataStorage.Instance.TryGetUnitPoolSize(unitId, 0)).ToList();
        
        _unitsSpawnTime = SelectedIds.Select(unitId => UnitDataStorage.Instance.TryGetUnitSpawnTime(unitId, 0)).ToArray();
        _unitsTimer = new float[_unitsSpawnTime.Length];

        InitPool();
    }
    
    private void Update()
    {
        CountDown();
    }

    private void CountDown()
    {
        if(IsInitialize == false) return;
        
        for (var i = 0; i < _unitsTimer.Length; i++)
        {
            if(GetCountOfInactiveUnits(SelectedIds[i]) == 0) continue;
            
            _unitsTimer[i] += Time.deltaTime;

            if (!(_unitsTimer[i] >= _unitsSpawnTime[i])) continue;
            
            SpawnUnit(SelectedIds[i]);

            _unitsTimer[i] = 0f;
        }
    }

    private void SpawnUnit(int id)
    {
        var unit = TryGetUnit(id);

        unit.Reset();
        unit.transform.position = _spawnPoint.position;
        unit.gameObject.SetActive(true);
    }
}
