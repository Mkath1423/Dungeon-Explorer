using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public SlotManager slot1, slot2;


    private void Update()
    {
        // if we selected both items
        if (slot1 != null && slot2 != null)
        {

            switch (MoveType())
            {
                case "swap":
                    SwapSelection();
                    break;

                case "combine":
                    CombineSelection(slot1.item.tags["maxStackable"]);
                    break;

                case "nothing":
                    break;
            }
           
        }
    }
    
    public string MoveType()
    {
        if(slot1.item == null || slot2.item == null)
        {
            return "swap";
        }
        else if (slot1.item.itemName == slot2.item.itemName)
        {
            slot1.item.tags.TryGetValue("stackable", out dynamic isStackable);
            if (isStackable) return "combine";
            else return "nothing";

        }
        else return "swap";        
    }


    public void StoreSelection(SlotManager slot)
    {
        // if we have not already stored a slection
        if (slot1 == null)
        {
            // store the GameObject and ItemStorage script in the first selection
            slot1 = slot;

        }
        // if we have already stored a slection
        else
        {
            // store the GameObject and ItemStorage script in the second selection
            slot2 = slot;
        }

        Debug.Log("selection stored");
    }

    public void ClearSelection()
    {
        // clear out both selections (used after switching or combing them)
        slot1 = null;
        slot2 = null;

        Debug.Log("selection cleared");
    }

    public bool ValidateMove()
    {
        bool valid = false;

        string[] slot1Types = slot1.slotType;
        string[] slot2Types = slot2.slotType;

        

        foreach(string tag in slot1Types)
        {
            if(tag == "any" || slot2.item.tags["itemType"] == tag)
            {
                valid = true;
                break;
            }
            else
            {
                valid = false;
            }
        }

        foreach (string tag in slot2Types)
        {
            if (tag == "any" || slot1.item.tags["itemType"] == tag)
            {
                valid = true;
                break;
            }
            else
            {
                valid = false;
            }
        }


        return valid;
    }


    public void CombineSelection(int max)
    {

        if (ValidateMove())
        {
            // if the selections will not combine to over their max stack value
            if (slot1.itemAmount + slot2.itemAmount <= max)
            {
                Debug.Log("Not too many items");
                // add the amounts to second selection then clear the data and sprite of the first selection 
                slot2.itemAmount += slot1.itemAmount;

                slot1.ClearSlot();
            }

            // if the amount will combine to over their max stack value
            else
            {
                Debug.Log("too many items");

                // set the second selection amount to the max and subtract the right amount from the first selection
                slot1.itemAmount = max - slot2.itemAmount;
                slot2.itemAmount = max;

            }
        }

        ClearSelection();
    }

    public void SwapSelection()
    {
        // if the item type matches the slot type or the slot can have anything
        if (ValidateMove())
        {
            Item tempItem = slot1.item;
            int tempAmount = slot1.itemAmount;

            bool isSlot1Empty = true, isSlot2Empty = true;

            if (slot1.item != null) isSlot1Empty = false;
            if (slot2.item != null) isSlot2Empty = false;


            if (!isSlot2Empty) slot1.AddItem(slot2.item, slot2.itemAmount);
            else slot1.ClearSlot();

            if (!isSlot1Empty) slot2.AddItem(tempItem, tempAmount);
            else slot2.ClearSlot();


            Debug.Log("selection swaped");
        }

        ClearSelection();
    }

    
}
