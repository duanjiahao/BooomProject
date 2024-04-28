using UnityEngine;

[CreateAssetMenu(fileName = "Medicine SO", menuName = "SO/Create Item/Medicine")]
public class MedicineSO : ItemSO
{
    [Tooltip("治愈量")]
    public float CureAmount;

    //todo:buff类型，buff效果等等
    public float buff;

    public override void BeUsed()
    {
        base.BeUsed();

    }
}
