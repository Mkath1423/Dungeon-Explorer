using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControler : MonoBehaviour
{
    public float speed;

    public Rigidbody2D rb;
    private Vector2 moveVelocity;


    
    void Update()
    {
        Vector2 moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        moveVelocity = moveInput.normalized * speed;
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + moveVelocity * Time.fixedDeltaTime);
    }
}
