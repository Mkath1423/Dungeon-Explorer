using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{


    public Rigidbody2D cam, player;


    public Collider2D playerCollider;


    public float speed, bufferTimeDefault, bufferTime;

    public bool startBuffer = false, standingStill = true, bufferOver = false, inTrigger = true;
    private Vector2 direction;

    private Vector2 currentPlayerPosition, previousPlayerPosition;

    void Start()
    {
        bufferTime = bufferTimeDefault;
        previousPlayerPosition = player.position;
        currentPlayerPosition = previousPlayerPosition;
    }


    void Update()
    {
        direction = cam.position - player..position;
        direction *= speed;
    }

    private void OnTriggerStay2D(Collider2D playerCollider)
    {
        inTrigger = true;
    }

    private void OnTriggerExit2D(Collider2D playerCollider)
    {
        inTrigger = false;
    }

    private void FixedUpdate()
    {
        previousPlayerPosition = currentPlayerPosition;
        currentPlayerPosition = player.position;

        if (currentPlayerPosition == previousPlayerPosition && !bufferOver)
        {
            standingStill = true;
            startBuffer = true;
        }
        else if (!bufferOver)
        {
            standingStill = false;
        }

        if (standingStill && !bufferOver) bufferTime -= Time.deltaTime;     
        else bufferTime = bufferTimeDefault;

        if (bufferTime <= 0f)
        {
            startBuffer = false;
            bufferTime = bufferTimeDefault;
            bufferOver = true;
        }

        if ((!standingStill && !inTrigger) || bufferOver) cam.MovePosition(cam.position - direction * Time.fixedDeltaTime);


        if (currentPlayerPosition != previousPlayerPosition) bufferOver = false;
    }




}
