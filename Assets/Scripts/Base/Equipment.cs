// ��??�^
public enum EquipmentType 
{
    LeftHand = 1,   // ���]
    RightHand = 2,  // �E�]
    LeftFoot = 3,   // ���r
    RightFoot = 4,  // �E�r
    Breast = 5,     // ���b
    Weapon = 6,     // ����
}
public class Equipment
{
    // ��??�^
    public EquipmentType type;

    // �h�䑕?�I??�S����
    public float DefencePercent;

    // ��?�I�ϋv
    public float Hp;

    // ����I�U?����
    public int Turns;

    // ����I�ŏ�?�Q
    public float MinDamage;

    // ����I�ő�?�Q
    public float MaxDamage;


}
