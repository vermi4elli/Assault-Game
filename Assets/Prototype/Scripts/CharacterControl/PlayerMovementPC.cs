﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementPC : MonoBehaviour
{
    [SerializeField]
    private GameObject player;
    
    //needed for movement of the character
    private Vector2 movement = Vector2.zero;
    public float movSpeed = 10f;

    //needed for the rotation of the character
    private Vector2 mousePos = Vector2.zero;
    private Vector2 mousePressPos = Vector2.zero;
    private bool mouseLeftKeyPressed = false;

    void Update()
    {
        movement.x = Input.GetAxis("Horizontal");
        movement.y = Input.GetAxis("Vertical");

        GetMousePos();
    }

    private void GetMousePos()
    {
        //controlls the fact if the LKM is pressed or not
        if (Input.GetMouseButtonDown(0))
        {
            mousePressPos = Input.mousePosition;
            mouseLeftKeyPressed = true;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            mouseLeftKeyPressed = false;
        }

        //controlls the mouse position when the key is holded down
        if (mouseLeftKeyPressed)
        {
            mousePos = Input.mousePosition;
        }
    }

    private void FixedUpdate()
    {
        player.transform.Translate(new Vector3(movement.x, 0f, movement.y) * movSpeed * Time.deltaTime);
        RotatePlayer();
    }

    private void RotatePlayer()
    {
        if (mouseLeftKeyPressed)
        {
            Vector2 direction = mousePos - mousePressPos;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg * -1 + 120;
            player.transform.rotation = Quaternion.Euler(0f, angle, 0f);
        }
    }
}
