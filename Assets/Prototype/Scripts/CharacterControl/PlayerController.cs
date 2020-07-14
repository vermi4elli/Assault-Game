﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // The move speed constants
    [SerializeField]
    private float moveSpeed = 4f;
    [SerializeField]
    private float rollSpeed = 10f;
    [SerializeField]
    private float touchContinuityTime = 0.5f;

    // Passed to the PlayerAnimator to awaken animations
    public float speedPercent;
    public bool rollForwardAwaken;

    // Needed for the UpdateRollForwardAwakenValue function
    private float touchBegan;

    // Needed for checking if the animation is finished and checking it's name
    private Animator animator;
    private PlayerAnimator playerAnimator;

    [SerializeField]
    private GameObject player;
    [SerializeField]
    private GameObject playerHead;
    [SerializeField]
    public Transform shootPoint;
    [SerializeField]
    private FloatingJoystick floatingJoystick;

    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        playerAnimator = GetComponent<PlayerAnimator>();
    }

    void Update()
    {
        UpdateSpeedPercentValue();
        UpdateRollForwardAwakenValue();
    }


    void FixedUpdate()
    {
        // Blocking the possibility to change the direction and speed of
        // the player while the Roll forward animation is playing
        if (!RollAnimationIsPlaying())
        {
            RotatePlayer();
            MovePlayer();
        }
        else
        {
            RollForwardPlayer();
        }
    }

    private void RollForwardPlayer() => player.transform.Translate(player.transform.forward * rollSpeed * Time.fixedDeltaTime, Space.World);

    public bool RollAnimationIsPlaying() => animator.GetCurrentAnimatorStateInfo(0).IsName("Roll forward");

    public bool HeadIsFacingWeapon() => Vector3.Angle(shootPoint.transform.forward, playerHead.transform.forward * -1) < 30f;

    public bool AnimatorIsInTransition() => animator.IsInTransition(0) || animator.IsInTransition(1) || animator.IsInTransition(2);

    public void DebugLog()
    {
        Debug.Log("x: " + floatingJoystick.Horizontal +
            "; y: " + floatingJoystick.Vertical +
            "; direction: " + floatingJoystick.Direction +
            "; speed: " + speedPercent +
            "; rollingForward: " + RollAnimationIsPlaying() +
            "; space is pressed: " + rollForwardAwaken);
    }

    private void MovePlayer()
    {
        if (!floatingJoystick.Direction.Equals(Vector2.zero))
            player.transform.Translate(player.transform.forward * speedPercent * moveSpeed * Time.fixedDeltaTime, Space.World);
    }

    private void RotatePlayer()
    {
        if (floatingJoystick.Direction != Vector2.zero)
        {
            Vector3 lookDirection = Quaternion.Euler(0f, 44f, 0f) * new Vector3(floatingJoystick.Direction.x, 0f, floatingJoystick.Direction.y);
            Quaternion rotation = Quaternion.LookRotation(lookDirection, Vector3.up);

            player.transform.rotation = rotation;
        }
    }

    private void UpdateSpeedPercentValue() => speedPercent = floatingJoystick.Direction.sqrMagnitude + 0.01f;

    private void UpdateRollForwardAwakenValue()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    touchBegan = Time.time;
                    break;
                case TouchPhase.Ended:
                    rollForwardAwaken = Time.time - touchBegan < touchContinuityTime;
                    break;
                default:
                    rollForwardAwaken = false;
                    break;
            }
        }

#if UNITY_EDITOR
        //if (Input.GetMouseButtonDown(0))
        //    touchBegan = Time.time;
        //if (Input.GetMouseButtonUp(0))
        //    rollForwardAwaken = Time.time - touchBegan < touchContinuityTime;
        //else rollForwardAwaken = false;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            rollForwardAwaken = true;
        }
        else rollForwardAwaken = false;
#endif
    }
}
