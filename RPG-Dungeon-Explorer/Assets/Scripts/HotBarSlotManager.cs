using UnityEngine;
using UnityEngine.UI;

public class HotBarSlotManager : MonoBehaviour
{
    public SlotManager linkedSlot;

    public Item item;
    public int itemAmount;

    public Image icon;
    public GameObject iconObject;

    public Color defaultColor;
    public Color selectedColor;
    private bool isSelected = false;

    private void Start()
    {
        if (linkedSlot.item == null)
        {
            icon.sprite = null;
            iconObject.SetActive(false);

        }
        else
        {
            item = linkedSlot.item;
            itemAmount = linkedSlot.itemAmount;

            icon.sprite = item.sprite;
            iconObject.SetActive(true);
        }
    }

    private void Update()
    {
        if (item != linkedSlot.item)
        {
            if(linkedSlot.item == null)
            {
                item = null;
                itemAmount = 0;

                icon.sprite = null;
                iconObject.SetActive(false);
            }
            else
            {
                item = linkedSlot.item;
                itemAmount = linkedSlot.itemAmount;

                icon.sprite = item.sprite;
                iconObject.SetActive(true);
            }
        }
    }

    public void ChangeColor()
    {
        if (isSelected)
        {
            isSelected = false;
            gameObject.GetComponent<Image>().color = defaultColor;
        }
        else
        {
            isSelected = true;
            gameObject.GetComponent<Image>().color = selectedColor;
        }
    }
}
