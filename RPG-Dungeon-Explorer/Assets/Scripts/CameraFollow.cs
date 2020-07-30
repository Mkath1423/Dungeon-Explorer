using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{


    public Rigidbody2D cam, player;

    public Vector2 min;
    public Vector2 max;
    public Vector2 cameraSize;

    private void Start()
    {
        cam.position = player.position;
    }

    private void FixedUpdate()
    {
        cam.position = new Vector2(Mathf.Clamp(player.position.x, min.x + cameraSize.x, max.x - cameraSize.x), Mathf.Clamp(player.position.y, min.y + cameraSize.y, max.y - cameraSize.y)) ;
    }




}
