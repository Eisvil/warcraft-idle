using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class TriggerZone : MonoBehaviour
{
    protected List<Unit> Units = new List<Unit>();
    
    public UnityAction<Unit> IsUnitEntered;

    public void Init(float size)
    {
        transform.localScale = Vector3.one * size; 
    }
    
    protected abstract void OnTriggerEnter(Collider other);

    protected void OnTriggerExit(Collider other)
    {
        if (!other.TryGetComponent(out Unit unit)) return;

        if(Units.Contains(unit))
            RemoveUnit(unit);
    }
    
    public virtual void RemoveUnit(Unit unit)
    {
        Units.Remove(unit);
    }

    public void Clear()
    {
        Units.Clear();
    }
    
    public Unit GetClosestEnemy(Vector3 unitPosition)
    {
        Unit closestUnit = null;
        var closestDistance = float.MaxValue;
        
        foreach (var unit in Units)
        {
            var unitDistance = Vector3.Distance(unitPosition, unit.transform.position);

            if (!(unitDistance < closestDistance)) continue;
            
            closestDistance = unitDistance;
            closestUnit = unit;
        }

        return closestUnit;
    }
}
