
using UnityEngine;
using UnityEngine.UI;

public class SlotManager : MonoBehaviour
{
    public string[] slotType = { "any" };

    public Item item;
    public int itemAmount;

    public GameObject iconObject;
    private Image icon;

    private void Start()
    {
        icon = iconObject.GetComponent<Image>();

        if (item == null)
        {
            icon.sprite = null;
            iconObject.SetActive(false);
     
        }
        else
        {
            ReloadGraphics(true);
        }
    }

    public void ReloadGraphics(bool isActive)
    { 
        icon.sprite = item.sprite;
        iconObject.SetActive(isActive);
    }

    public void StoreItem(Item newItem, int newItemAmount)
    {
        item = newItem;
        itemAmount = newItemAmount;
    }

    public void StoreItem(int newItemAmount)
    {
        itemAmount += newItemAmount;

    }

    public void ClearSlot()
    {
        item = null;

        icon.sprite = null;
        iconObject.SetActive(false);
    }

    public void SendAsSelection()
    {
        GetComponentInParent<InventoryManager>().StoreSelection(this);
    }

    public void AddItem(Item newItem, int newItemAmount)
    {
        item = newItem;
        itemAmount = newItemAmount;


        iconObject.SetActive(true);
        icon.sprite = item.sprite;
        
    }
    


}
