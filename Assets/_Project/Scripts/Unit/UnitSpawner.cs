using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class UnitSpawner<T> : MonoBehaviour where T: Unit
{
    [SerializeField] private bool _isEnemy;
    
    private readonly List<T> _pool = new List<T>();
    private bool _isInitialize;
    
    protected List<T> Templates;
    protected List<int> PoolSizes;
    protected int[] SelectedUnitLevels;
    protected int[] SelectedIds;
    protected float[] UnitsSpawnTime;
    protected float[] UnitsTimer;

    private void Update()
    {
        CountDown();
    }
    
    private void CountDown()
    {
        if(_isInitialize == false || !BattleManager.Instance.IsBattleGoing) return;
        
        for (var i = 0; i < UnitsTimer.Length; i++)
        {
            if(GetCountOfInactiveUnits(SelectedIds[i]) == 0) continue;
            
            UnitsTimer[i] += Time.deltaTime;

            if (!(UnitsTimer[i] >= UnitsSpawnTime[i])) continue;
            
            SpawnUnit(SelectedIds[i]);

            UnitsTimer[i] = 0f;
        }
    }
    
    private void TryClear()
    {
        if(_pool.Count == 0) return;
        
        foreach (var unit in _pool)
        {
            Destroy(unit);
        }
        
        _pool.Clear();
    }

    protected abstract void SpawnUnit(int id);

    protected void InitPool()
    {
        TryClear();

        for (var i = 0; i < Templates.Count; i++)
        {
            var template = Templates[i];
            
            for (var j = 0; j < PoolSizes[i]; j++)
            {
                var unit = Instantiate(template, transform);
                
                unit.Init(SelectedIds[i], UnitDataStorage.Instance.TryGetUnitBasicStats(i, SelectedUnitLevels[i]), _isEnemy);

                _pool.Add(unit);
 
                unit.gameObject.SetActive(false);
            }
        }

        _isInitialize = true;
    }

    protected T TryGetUnit(int id)
    {
        return _pool.FirstOrDefault(unit => unit.gameObject.activeSelf == false && unit.Id == id);
    }

    protected int GetCountOfInactiveUnits(int id)
    {
        return _pool.Count(unit => unit.gameObject.activeSelf == false && unit.Id == id);
    }

    public List<T> GetActiveUnits()
    {
        return _pool.Where(unit => unit.gameObject.activeSelf).ToList();
    }
}
