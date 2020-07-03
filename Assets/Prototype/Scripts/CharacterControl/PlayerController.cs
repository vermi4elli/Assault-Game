﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public const float moveSpeed = 4f;
    private float speedPercent;

    [SerializeField]
    private GameObject player;
    [SerializeField]
    private FloatingJoystick floatingJoystick;

    void FixedUpdate()
    {
        // this method is similar to the PlayerAnimator's
        // it gets the speed of the player according to how much the handle of the joystick is moved
        UpdateSpeedPercentValue();

        RotatePlayer();
        MovePlayer();
        
        DebugLogger();
    }

    private void DebugLogger()
    {
        Debug.Log("x: " + floatingJoystick.Horizontal +
            "; y: " + floatingJoystick.Vertical +
            "; direction: " + floatingJoystick.Direction +
            "; speed: " + speedPercent +
            "; zero: " + floatingJoystick.Direction.Equals(Vector2.zero));
    }

    private void MovePlayer()
    {
        player.transform.Translate(player.transform.forward * speedPercent * moveSpeed * Time.fixedDeltaTime, Space.World);
    }

    private void RotatePlayer()
    {
        Vector3 lookDirection = Quaternion.Euler(0f, 44f, 0f) * new Vector3(floatingJoystick.Direction.x, 0f, floatingJoystick.Direction.y);
        Quaternion rotation = Quaternion.LookRotation(lookDirection, Vector3.up);

        if (!floatingJoystick.Direction.Equals(Vector2.zero))
            player.transform.rotation = rotation;
    }

    private void UpdateSpeedPercentValue()
    {
        speedPercent = floatingJoystick.Direction.sqrMagnitude + 0.01f;
    }
}
