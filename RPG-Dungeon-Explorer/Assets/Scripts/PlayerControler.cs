using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControler : MonoBehaviour
{
    public float speed;

    public Rigidbody2D rb;
    private Vector2 moveVelocity;

    public Item heldItem;

    public GameObject inventory;
    public GameObject hotBar;

    public GameObject hotBarParent;
    private HotBarSlotManager[] hotBarSlots;
    private float selectedSlot = 0;

    private bool isInventoryOpen;

    private void Start()
    {
        hotBarSlots = hotBarParent.GetComponentsInChildren<HotBarSlotManager>();
        heldItem = hotBarSlots[0].item;
        isInventoryOpen = false;
    }

    void Update()
    {
        Vector2 moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        moveVelocity = moveInput.normalized * speed;

        if(Input.GetAxisRaw("Mouse ScrollWheel") != 0)
        {
            if (selectedSlot == 0 && Input.GetAxisRaw("Mouse ScrollWheel") < 0) selectedSlot = 6;
            else if (selectedSlot == 6 && Input.GetAxisRaw("Mouse ScrollWheel") > 0) selectedSlot = 0;
            else selectedSlot += Input.GetAxisRaw("Mouse ScrollWheel");

            heldItem = hotBarSlots[(int) selectedSlot].item;
            Debug.Log(Input.GetAxisRaw("Mouse ScrollWheel"));
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            ToggleGameobject(inventory);
            ToggleGameobject(hotBar);
        }

        if (inventory.activeSelf) isInventoryOpen = true;
        else isInventoryOpen = false;
    }

    void ToggleGameobject(GameObject target)
    {
        if (target.activeSelf == true) target.SetActive(false);
        else target.SetActive(true);
 
    }



    void FixedUpdate()
    {
        if(!isInventoryOpen) rb.velocity = new Vector2(moveVelocity.x, moveVelocity.y);

    }
}
