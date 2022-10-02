using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControler : Entities
{
    public Rigidbody2D rb;
    private Vector2 moveVelocity;

    public Item heldItem;
    public GameObject weaponDrawPoint;

    public GameObject inventory;
    public GameObject hotBar;

    public GameObject hotBarParent;
    public HotBarSlotManager[] hotBarSlots;
    private float selectedSlot = 0;

    private bool isInventoryOpen;

    public Item itemToAdd;

    private void Start()
    {

        hotBarSlots = hotBarParent.GetComponentsInChildren<HotBarSlotManager>();

        heldItem = hotBarSlots[0].item;
        hotBarSlots[0].ChangeColor();

        isInventoryOpen = false;
        inventory.SetActive(false);

        inventory.GetComponent<InventoryManager>().AddItem(itemToAdd, 1);
    }

    void Update()
    {

        // calculate move vector
        Vector2 moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        moveVelocity = moveInput.normalized * speed;

        // make the player face the correc direction
        if (moveInput.x == 1) transform.localScale = new Vector2(1, 1);
        if (moveInput.x == -1) transform.localScale = new Vector2(-1, 1);

        // intanciate the held item spriteobject as a child of player

        heldItem = hotBarSlots[(int)selectedSlot].item;
        if (heldItem != null) weaponDrawPoint.GetComponent<SpriteRenderer>().sprite = heldItem.sprite;
        else weaponDrawPoint.GetComponent<SpriteRenderer>().sprite = null;

        // Call the use method of the item when mounse button 0 is pressed
        if (Input.GetMouseButtonDown(0) && !isInventoryOpen && heldItem != null) heldItem.Use(weaponDrawPoint, 0f);


        // switch the held item whent the scroll wheel in moved
        if (Input.GetAxisRaw("Mouse ScrollWheel") != 0)
        {
            float mouseScrollAmount = Input.GetAxisRaw("Mouse ScrollWheel");
            hotBarSlots[(int)selectedSlot].ChangeColor();

            if (mouseScrollAmount > 0)
            {
                if (selectedSlot == 6) selectedSlot = 0;
                else selectedSlot += 1;
            }

            else if(mouseScrollAmount < 0)
            {
                if (selectedSlot == 0) selectedSlot = 6;
                else selectedSlot -= 1;
            }

            hotBarSlots[(int)selectedSlot].ChangeColor();
        }

        // open the inventory when e is pressed
        if (Input.GetKeyDown(KeyCode.E))
        {
            ToggleGameobject(inventory);
            ToggleGameobject(hotBar);
        }

        if (inventory.activeSelf) isInventoryOpen = true;
        else isInventoryOpen = false;
    }

    void FixedUpdate()
    {
        // if the inventory is not open then move the player based on the previous calculations
        if (!isInventoryOpen) rb.velocity = new Vector2(moveVelocity.x, moveVelocity.y);
    }

    void ToggleGameobject(GameObject target)
    {
        if (target.activeSelf == true) target.SetActive(false);
        else target.SetActive(true);
    }
}
