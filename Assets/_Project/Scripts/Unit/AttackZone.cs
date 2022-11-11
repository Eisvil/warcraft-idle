using System.Linq;

public abstract class AttackZone : TriggerZone
{
    protected Unit RootUnit;
    
    public void SetRootUnit(Unit unit)
    {
        RootUnit = unit;
    }
    
    public Unit GetUnitInZone()
    {
        return Units.FirstOrDefault();
    }
}
