using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemStorage : MonoBehaviour
{
    public Item currentItem;
    public int itemAmount;
    public Dictionary<string, dynamic> itemTags;

    private void Start()
    {
        // if there is an item here
        if (currentItem != null)
        {
            // find and store its tags
            // tags are the same for each subclass
            // dictionary cant be set in the editor
            itemTags = currentItem.tags;
        }
    }

    public void ClearData()
    {
        currentItem = null;
        itemAmount = 0;
        itemTags = null;
    }

    public void SetData(Item newItem, int newItemAmount, Dictionary<string, dynamic> newItemTags)
    {
        currentItem = newItem;
        itemAmount = newItemAmount;
        itemTags = newItemTags;

    }

}
