using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public SlotManager slot1, slot2;

    public SlotManager[] slots;

    private void Start()
    {
        slots = gameObject.GetComponentsInChildren<SlotManager>();
        Debug.Log("found" + slots.Length + "slots");
    }

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



        foreach (string tag in slot1Types)
        {
            if (tag == "any" || slot2.item.tags["itemType"] == tag)
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

    public string MoveType()
    {
        if (slot1.item == null || slot2.item == null)
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
                slot1.itemAmount = 0;
                slot1.ClearSlot();
            }

            // if the amount will combine to over their max stack value
            else if(slot1.itemAmount == max || slot2.itemAmount == max)
            {
                SwapSelection();
            }

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

    public void AddItem(Item newItem, int newItemAmount)
    {
        
        string newItemName = newItem.itemName;
        newItem.tags.TryGetValue("stackable", out dynamic isStackable);

        if(isStackable == null)
        {
            Debug.Log("error");
            return;
        }

        if (isStackable)
        {
            List<int> combinableSlots = FindCombinableSlots(newItem);

            if(combinableSlots.Count != 0)
            {
                int amountToAdd = newItemAmount;
                for (int i = 0; i < combinableSlots.Count; i++)
                {

                    Debug.Log(amountToAdd);
                    int max = newItem.tags["maxStackable"];


                    if (slots[i].itemAmount == max)
                    {
                        continue;
                    }

                    if(slots[combinableSlots[i]].itemAmount + amountToAdd <= max)
                    {
                        slots[combinableSlots[i]].itemAmount += amountToAdd;
                        amountToAdd = 0;
                    }
                    else
                    {
                        amountToAdd -= max - slots[combinableSlots[i]].itemAmount;
                        slots[combinableSlots[i]].itemAmount = max;
                    }

                    if (amountToAdd == 0) return;
                }

                int targetSlot = FindOpenSlot();
                slots[targetSlot].StoreItem(newItem, amountToAdd);
                slots[targetSlot].ReloadGraphics(true);
            }
            else
            {
                int targetSlot = FindOpenSlot();
                slots[targetSlot].StoreItem(newItem, newItemAmount);
                slots[targetSlot].ReloadGraphics(true);
            }
        }
        else
        {
            int targetSlot = FindOpenSlot();
            slots[targetSlot].StoreItem(newItem, newItemAmount);
            slots[targetSlot].ReloadGraphics(true);
        }
    }

    public int FindOpenSlot()
    {
        int openSlot = -1;

        for (int i = 0; i < slots.Length - 1; i++)
        {

            if (slots[i].item == null)
            {
                if (openSlot == -1) openSlot = i;
            }
            
        }

        return openSlot;
    }

    public List<int> FindCombinableSlots(Item newItem)
    {
        List<int> combinableSlots = new List<int>();

        for (int i = 0; i < slots.Length - 1; i++)
        {
            if (slots[i].item == null) continue;
            if (slots[i].item.itemName == newItem.itemName)
            {
                combinableSlots.Add(i);
            }

        }

        return combinableSlots;
    }

}
