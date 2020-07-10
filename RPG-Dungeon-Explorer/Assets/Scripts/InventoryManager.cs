using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public GameObject firstSelection, secondSelection;
    public ItemStorage firstSelectionStorage, secondSelectionStorage;

    private void Update()
    {
        // if we selected both items
        if (firstSelection != null && secondSelection != null)
        {
            // if one of the slots is empty
            if(firstSelectionStorage.currentItem == null || secondSelectionStorage == null)
            {
                // switch the items over
                // then clear selection
                SwitchSelection();

            }
            else
            {
                //if the items are the same
                if (firstSelectionStorage.currentItem == secondSelectionStorage.currentItem)
                {
                    // if these Items can be stacked
                    
                    firstSelectionStorage.itemTags.TryGetValue("stackable", out dynamic isStackable);
                    if (isStackable == true)
                    {
                        // get their max stack value and combine the items  
                        // then clear the selection
                        firstSelectionStorage.itemTags.TryGetValue("maxStackable", out dynamic maxStackable);
                        CombineSelection(maxStackable);
                    }
                    // if these items cannot be stacked
                    else
                    {
                        // clear the selection (do nothing)
                        ClearSelection();
                    }
                }
                else
                {
                    // if the items are not the same 
                    //switch their sprites and stored data
                    // then clear the selection
                    SwitchSelection();
                }
            }
            
        }
    }
    



    public void StoreSelection(GameObject slot)
    {
        // if we have not already stored a slection
        if (firstSelection == null)
        {
            // store the GameObject and ItemStorage script in the first selection
            firstSelection = slot;
            firstSelectionStorage = slot.GetComponent<ItemStorage>();

        }
        // if we have already stored a slection
        else
        {
            // store the GameObject and ItemStorage script in the second selection
            secondSelection = slot;
            secondSelectionStorage = slot.GetComponent<ItemStorage>();
        }

        Debug.Log("selection stored");
    }

    public void ClearSelection()
    {
        // clear out both selections (used after switching or combing them)
        firstSelection = null;
        firstSelectionStorage = null;

        secondSelection = null;
        secondSelectionStorage = null;

        Debug.Log("selection cleared");
    }

    public bool ValidateMove()
    {
        bool valid = false;

        string[] firstSlotTypes = firstSelection.GetComponentInParent<SlotManager>().slotType;
        string[] secondSlotTypes = secondSelection.GetComponentInParent<SlotManager>().slotType;

        //(firstSelectionStorage.itemTags["itemType"] == secondSlotType || secondSlotType == "any") && (secondSelectionStorage.itemTags["itemType"] == firstSlotType || firstSlotType == "any")

        foreach(string item in firstSlotTypes)
        {
            if(item == "any" || secondSelectionStorage.itemTags["itemType"] == item)
            {
                valid = true;
                break;
            }
            else
            {
                valid = false;
            }
        }

        foreach (string item in secondSlotTypes)
        {
            if (item == "any" || firstSelectionStorage.itemTags["itemType"] == item)
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
            Debug.Log(max);
            // if the selections will not combine to over their max stack value
            if (secondSelectionStorage.itemAmount + firstSelectionStorage.itemAmount <= max)
            {
                Debug.Log("Not too many items");
                // add the amounts to second selection then clear the data and sprite of the first selection 
                secondSelectionStorage.itemAmount += firstSelectionStorage.itemAmount;

                firstSelection.GetComponent<Image>().sprite = null;
                firstSelectionStorage.ClearData();
            }

            // if the amount will combine to over their max stack value
            else
            {
                Debug.Log("too many items");

                // set the second selection amount to the max and subtract the right amount from the first selection
                firstSelectionStorage.itemAmount = max - secondSelectionStorage.itemAmount;
                secondSelectionStorage.itemAmount = max;

            }
        }

        ClearSelection();
    }

    public void SwitchSelection()
    {

       
        // if the item type matches the slot type or the slot can have anything
        if (ValidateMove())
        {

            // switch the sprites and data of both selections
            Sprite icon1 = firstSelection.GetComponent<Image>().sprite;
            Sprite icon2 = secondSelection.GetComponent<Image>().sprite;

            firstSelection.GetComponent<Image>().sprite = icon2;
            secondSelection.GetComponent<Image>().sprite = icon1;

            Item tempItem = secondSelectionStorage.currentItem;
            int tempAmount = secondSelectionStorage.itemAmount;

            Dictionary<string, dynamic> tempTags = secondSelectionStorage.itemTags;

            secondSelectionStorage.SetData(firstSelectionStorage.currentItem, firstSelectionStorage.itemAmount, firstSelectionStorage.itemTags);
            firstSelectionStorage.SetData(tempItem, tempAmount, tempTags);

            Debug.Log("selection swaped");
        }

        ClearSelection();
    }

    
}
