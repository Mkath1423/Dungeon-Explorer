
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
            Debug.Log("no item");

            icon.sprite = null;
            iconObject.SetActive(false);
     
        }
        else
        {
            Debug.Log("item"); 
            icon.sprite = item.sprite;
            iconObject.SetActive(true);

        }
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
        Debug.Log(item.sprite);
        icon.sprite = item.sprite;
        
    }
    
    public void ClearSlot()
    {
        item = null;

        icon.sprite = null;
        iconObject.SetActive(false);
    }

}
