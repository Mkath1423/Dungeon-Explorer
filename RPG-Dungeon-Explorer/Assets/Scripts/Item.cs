using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[CreateAssetMenu(fileName = "newItem", menuName = "Items/Item", order = 0)]
public class Item : ScriptableObject
{

    public Dictionary<string, dynamic> tags = new Dictionary<string, dynamic>();

    public string itemName;
    public float useDelay;
    public Sprite sprite;

    public float timer;

    public bool canUse = true;
    

    public virtual void Use(GameObject player, float rotation)
    {
        if (!canUse)
        {
            Debug.Log("Cannot Use");
            return;
        }
        else canUse = false;

        Debug.Log(itemName + " used");
    }

    public virtual void OnValidate()
    {
        Debug.Log(itemName + " is awake");
        timer = useDelay;
    }

    public void Update()
    {
        if (!canUse)
        {
            timer -= Time.deltaTime;
            Debug.Log(timer);
        }  
        if(timer <= 0)
        {
            timer = useDelay;
            canUse = true;
        }
    }

}

[CreateAssetMenu(fileName = "newEquipable", menuName = "Items/Equipable", order = 1)]
public class Equipable : Item 
{
    public List<string> missionNames;
    public List<string> actionKeys;
    public List<int> actionAmounts;

    public virtual void Awake()
    {
        tags.Add("stackable", false);
    }
}

[CreateAssetMenu(fileName = "newArmor", menuName = "Items/Armor", order = 2)]
public class Armor : Equipable
{


}


[CreateAssetMenu(fileName = "newWeapon", menuName = "Items/Weapon", order = 3)]
public class Weapon : Equipable
{
    public GameObject weaponObject;

    public override void Awake()
    {
        base.Awake();
        tags.Add("itemType", "Weapon");
    }

}

[CreateAssetMenu(fileName = "newMeleeWeapon", menuName = "Items/Melee Weapon", order = 4)]
public class MeleeWeapon : Weapon
{
    public float attackSpeed;
    public float attackAngle;

    public override void Use(GameObject weaponPoint, float rotation)
    {
        base.Use(weaponPoint, rotation);
       
        
        GameObject tempClone = Instantiate(weaponObject, weaponPoint.transform.position, Quaternion.Euler(0, 0, rotation - (attackAngle / 2)));
        tempClone.transform.SetParent(weaponPoint.transform);

    }

}

[CreateAssetMenu(fileName = "newProjectileWeapon", menuName = "Items/Projectile Weapon", order = 5)]
public class ProjectileWeapon : Weapon
{
    public float ProjectileSpeed;
    public string ProjectileShape;
    public float[] ProjectileHitbox;

    public override void Use(GameObject weaponPoint, float rotation)
    {
        base.Use(weaponPoint, rotation);
        // do attack
    }


}

[CreateAssetMenu(fileName = "Consumable", menuName = "Items/Consumable", order = 6)]

public class Consumable : Item
{
    public int maxStackable;

    public void Awake()
    {
        tags.Add("stackable", true);
        tags.Add("maxStackable", maxStackable);
        tags.Add("itemType", "Consmable");
    }

    public void Use()
    {
        // consume item
    }
}