using UnityEngine;
using System.Collections;

public class MeleeWeaponAttack : MonoBehaviour
{

    public MeleeWeapon weaponItem;
    private GameObject pivotPoint;

    private float rotationSpeed;
    private float startingRotation;
    private float targetRotation;


    private void Awake()
    {

        pivotPoint = GameObject.FindWithTag("weaponPoint");
        startingRotation = pivotPoint.transform.eulerAngles.z;
        targetRotation = startingRotation + weaponItem.attackAngle;

        rotationSpeed = (targetRotation - startingRotation) / weaponItem.attackSpeed;
    }
    
    private void Update()
    {
        if (pivotPoint.transform.eulerAngles.z < targetRotation)
        {
            pivotPoint.transform.eulerAngles += new Vector3(0, 0, rotationSpeed * Time.deltaTime);
        }
        else
        {
            pivotPoint.transform.eulerAngles = new Vector3(0, 0, 0);
            Destroy(gameObject);
        }
    }
}
