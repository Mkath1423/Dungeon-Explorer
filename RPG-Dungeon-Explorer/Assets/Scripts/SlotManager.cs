using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotManager : MonoBehaviour
{
    public string[] slotType = { "any" };

    public void SendAsSelection(GameObject icon)
    {
        this.GetComponentInParent<InventoryManager>().StoreSelection(icon);
    }
    
}
