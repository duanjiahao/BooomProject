using UnityEngine;

[CreateAssetMenu(fileName = "RepairTool SO", menuName = "SO/Create Item/RepairTool")]
public class RepairToolSO : ItemSO
{
    [Tooltip("修理量")]
    public float repairQuantity;
    public override void BeUsed()
    {
        base.BeUsed();

    }
}
