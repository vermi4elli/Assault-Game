using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private const float moveSpeed = 4f;
    private const float rollSpeed = 10f;
    public float speedPercent;
    public bool rollForwardAwaken;

    private Animator animator;

    [SerializeField]
    private GameObject player;
    [SerializeField]
    private FloatingJoystick floatingJoystick;

    void Start()
    {
        animator = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        UpdateSpeedPercentValue();
        UpdateRollForwardAwakenValue();
    }


    void FixedUpdate()
    {
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

    private void RollForwardPlayer()
    {
        player.transform.Translate(player.transform.forward * rollSpeed * Time.fixedDeltaTime, Space.World);
    }

    private bool RollAnimationIsPlaying()
    {
        return animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1 && animator.GetCurrentAnimatorStateInfo(0).IsName("Roll forward");
    }

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

    private void UpdateRollForwardAwakenValue()
    {
        rollForwardAwaken = Input.GetKeyDown(KeyCode.Space);
    }
    
    private void UpdateRollForwardAwakenValueTest()
    {
        rollForwardAwaken = Input.GetKeyDown(KeyCode.Space);
    }
}
