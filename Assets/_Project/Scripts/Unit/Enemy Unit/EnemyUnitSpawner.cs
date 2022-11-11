using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyUnitSpawner : UnitPool<EnemyUnit>
{
    [SerializeField] private Transform[] _spawnPoints;
    [SerializeField] private float _spawnTime;

    private float _timer;

    public void Init()
    {
        SelectedIds = PlayerDeckManager.Instance.SelectedUnitsId;
        Templates = SelectedIds.Select(unitId => UnitDataStorage.Instance.TryGetEnemyUnit(unitId, UnitRace.Human)).ToList();
        PoolSizes = SelectedIds.Select(unitId => UnitDataStorage.Instance.TryGetUnitPoolSize(unitId, 0)).ToList();

        InitPool();
    }
    
    private void Update()
    {
        CountDown();
    }

    private void CountDown()
    {
        if(GetCountOfInactiveUnits(0) == 0) return;
        
        _timer += Time.deltaTime;

        if (!(_timer >= _spawnTime)) return;
        
        SpawnUnit(0);
        _timer = 0;
    }
    
    private void SpawnUnit(int id)
    {
        var unit = TryGetUnit(id);
        
        unit.Reset();
        unit.transform.position = _spawnPoints[Random.Range(0, _spawnPoints.Length)].position;
        unit.gameObject.SetActive(true);
        unit.TryFindTarget();
    }
}
