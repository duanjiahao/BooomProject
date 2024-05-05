using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 怪物的意图
public class Intension
{
    // 目标位置（目前不可以是武器）
    public EquipmentLocation location;

    // 是防御还是攻击：0防御，1攻击
    public int AttackOrDefence;
}

public class Monster : Unit
{
    public virtual BaseConfig config { get; protected set; }
    // 怪物当前的意图
    public Intension CurrentIntension { get; set; }
    
    private GameObject breakGO;
    public override void GenerateGameObject(int id)
    {
        Root = GameObject.Instantiate(Resources.Load<GameObject>("Monster"));
        base.GenerateGameObject(id);

        breakGO = Root.transform.Find("break").gameObject;
        breakGO.SetActive(false);
        
        config = ConfigManager.Instance.GetConfig<EnemyConfig>(id);

        var enemyConfig = config as EnemyConfig;
        MaxHp = Hp = enemyConfig.enemyHP;
        Strength = enemyConfig.enemySTR;
        Ductility = enemyConfig.enemyCON;
        Dexterity = enemyConfig.enemyDEX;
        Agility = enemyConfig.enemyAGI;

        List<int> weights = new List<int>();
        weights.Add(enemyConfig.Weight1);
        weights.Add(enemyConfig.Weight2);
        weights.Add(enemyConfig.Weight3);
        weights.Add(enemyConfig.Weight4);

        equipmentSystem = new EquipmentSystem();
        var rarityList = CommonUtils.RollRange(weights, 6, false);
        for (int i = 0; i < (int)EquipmentLocation.Count; i++)
        {
            var type = (EquipmentLocation)i;
            if (type == EquipmentLocation.Weapon)
            {
                var weaponConfigList = ConfigManager.Instance.GetConfigListWithFilter<WeaponConfig>((config) =>
                {
                    return config.weapomRarity == rarityList[i] + 1;
                });

                if (weaponConfigList == null || weaponConfigList.Count == 0)
                {
                    equipmentSystem.SetEquipment(EquipmentLocation.Weapon, null, null);
                }
                else
                {
                    equipmentSystem.SetEquipment(EquipmentLocation.Weapon, null, new Weapon(weaponConfigList[Random.Range(0, weaponConfigList.Count)].id));
                }
            }
            else 
            {
                var armorConfigList = ConfigManager.Instance.GetConfigListWithFilter<ArmorConfig>((config) =>
                {
                    return config.armorRarity == rarityList[i] + 1 && CommonUtils.CheckEquipmentTypeCorrespond(type, (EquipmentType)config.armorType);
                });

                if (armorConfigList == null || armorConfigList.Count == 0)
                {
                    equipmentSystem.SetEquipment(type, null, null);
                }
                else
                {
                    equipmentSystem.SetEquipment(type, new Equipment(armorConfigList[Random.Range(0, armorConfigList.Count)].id), null);
                }
            }
        }

        Root.transform.position = new Vector3(4f, -1f, 0f);
        Root.transform.localScale = new Vector3(-1, 1, 1);
        
        _animator = Root.GetComponentInChildren<Animator>();

        //GameObject MonstereLegsUI = GameObject.Instantiate(Resources.Load<GameObject>("Equipment_Canvas"), GameObject.Find("MonsterEquipmentDurability_Panel").transform);
        //MonstereLegsUI.GetComponent<EquipmentUI>().equipmentSO = Legs.SO;
        //GameObject MonsterHeadUI = GameObject.Instantiate(Resources.Load<GameObject>("Equipment_Canvas"), GameObject.Find("MonsterEquipmentDurability_Panel").transform);
        //MonsterHeadUI.GetComponent<EquipmentUI>().equipmentSO = Head.SO;
        //GameObject MonsterLeftHandUI = GameObject.Instantiate(Resources.Load<GameObject>("Equipment_Canvas"), GameObject.Find("MonsterEquipmentDurability_Panel").transform);
        //MonsterLeftHandUI.GetComponent<EquipmentUI>().equipmentSO = LeftHand.SO;
        //GameObject MonsterrightHandUI = GameObject.Instantiate(Resources.Load<GameObject>("Equipment_Canvas"), GameObject.Find("MonsterEquipmentDurability_Panel").transform);
        //MonsterrightHandUI.GetComponent<EquipmentUI>().equipmentSO = RightHand.SO;
        //GameObject MonsterBreastUI = GameObject.Instantiate(Resources.Load<GameObject>("Equipment_Canvas"), GameObject.Find("MonsterEquipmentDurability_Panel").transform);
        //MonsterBreastUI.GetComponent<EquipmentUI>().equipmentSO = Breast.SO;
        ////UI实例化
        //RootUI = GameObject.Instantiate(Resources.Load<GameObject>("MonsterUI_Canvas"), GameObject.Find("CombatUI").transform);

        CurrentIntension = new Intension()
        {
            AttackOrDefence = 1,
            location = (EquipmentLocation)Random.Range(0, 5),
        };
        
        ReplaceEquipmentSystem.RefleshSomeoneEquipment(equipmentSystem, ReplaceEquipmentSystem.ReturnSpriteResolvers(Root));
    }

    public override void SetBreaking()
    {
        base.SetBreaking();
        breakGO.SetActive(true);
    }

    public override void StopBreaking()
    {
        base.StopBreaking();
        breakGO.SetActive(false);
    }
}
