using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

public class EnemyUnitSpawner : UnitSpawner<EnemyUnit>
{
    [SerializeField] private Transform[] _spawnPoints;
    [SerializeField] private int _poolSize;

    private float _timer;

    public void Init(WaveData waveData)
    {
        SelectedIds = waveData.UnitIds;
        SelectedUnitLevels = new int[waveData.UnitIds.Length];
        Templates = SelectedIds.Select(unitId => UnitDataStorage.Instance.TryGetEnemyUnit(unitId, UnitRace.Undead)).ToList();

        PoolSizes = new List<int>();
        
        for (var i = 0; i < SelectedIds.Length; i++)
        {
            PoolSizes.Add(_poolSize);
        }

        UnitsSpawnTime = waveData.UnitsSpawnTime;
        UnitsTimer = new float[UnitsSpawnTime.Length];

        InitPool();
    }

    private void OnUnitDying(Unit unit)
    {
        unit.IsDying -= OnUnitDying;
        
        Wallet.Instance.AddExp(LevelManager.Instance.CurrentWave.ExpReward);
    }

    protected override void SpawnUnit(int id)
    {
        var unit = TryGetUnit(id);
        
        unit.Reset();
        unit.transform.position = _spawnPoints[Random.Range(0, _spawnPoints.Length)].position;
        unit.gameObject.SetActive(true);
        unit.TryFindTarget();
        unit.IsDying += OnUnitDying;
    }
}
