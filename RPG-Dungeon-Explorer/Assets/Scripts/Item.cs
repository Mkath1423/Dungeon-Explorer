using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[CreateAssetMenu(fileName = "newItem", menuName = "Items/Item", order = 0)]
public class Item : ScriptableObject
{

    public Dictionary<string, dynamic> tags = new Dictionary<string, dynamic>();

    public string itemName;
    public GameObject itemObject;
    public Sprite sprite;

}

[CreateAssetMenu(fileName = "newWeapon", menuName = "Items/Weapon", order = 1)]
public class Weapon : Item
{
    

    public virtual void OnValidate()
    {
        tags.Add("stackable", false);
        tags.Add("itemType", "Weapon");
    }
    
        
   
}

[CreateAssetMenu(fileName = "newMeleeWeapon", menuName = "Items/Melee Weapon", order = 2)]
public class MeleeWeapon : Weapon
{
    public float attackRadius;
    public float attackAngle;

    public override void OnValidate()
    {
        tags.Add("stackable", false);
        tags.Add("itemType", "Weapon");
    }

}

[CreateAssetMenu(fileName = "newProjectileWeapon", menuName = "Items/Projectile Weapon", order = 3)]
public class ProjectileWeapon : Weapon
{
    public float ProjectileSpeed;
    public string ProjectileShape;
    public float[] ProjectileHitbox;

    public override void OnValidate()
    {
        tags.Add("stackable", false);
        tags.Add("itemType", "Weapon");
    }
}

[CreateAssetMenu(fileName = "Consumable", menuName = "Items/Consumable", order = 4)]

public class Consumable : Item
{
    public int maxStackable;

    public void OnValidate()
    {
        tags.Add("stackable", true);
        tags.Add("maxStackable", maxStackable);
        tags.Add("itemType", "Consmable");
    }
}