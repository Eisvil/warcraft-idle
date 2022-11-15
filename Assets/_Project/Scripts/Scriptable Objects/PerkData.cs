using UnityEngine;

[CreateAssetMenu(fileName = "PerkData", menuName = "Perk/Perk Data", order = 1)]
public class PerkData : ScriptableObject
{
    public PerkName Id;
    public string Name;
    public Sprite Icon;
}
