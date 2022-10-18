using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class UnitPool<T> : MonoBehaviour where T: Unit
{
    [SerializeField] private bool _isEnemy;
    
    private readonly List<T> _pool = new List<T>();
    
    protected List<T> Templates;
    protected List<int> PoolSizes;
    protected int[] SelectedIds;
    protected bool IsInitialize;
    
    private void TryClear()
    {
        if(_pool.Count == 0) return;
        
        foreach (var unit in _pool)
        {
            Destroy(unit);
        }
        
        _pool.Clear();
    }
    
    protected virtual void InitPool()
    {
        TryClear();

        for (var i = 0; i < Templates.Count; i++)
        {
            var template = Templates[i];
            
            for (var j = 0; j < PoolSizes[i]; j++)
            {
                var unit = Instantiate(template, transform);
                
                unit.Init(SelectedIds[i], UnitDataStorage.Instance.TryGetUnitBasicStats(i, 0), _isEnemy);

                _pool.Add(unit);
 
                unit.gameObject.SetActive(false);
            }
        }

        IsInitialize = true;
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
