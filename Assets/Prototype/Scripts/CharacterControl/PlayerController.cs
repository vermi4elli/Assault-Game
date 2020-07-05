using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private const float moveSpeed = 4f;
    public float speedPercent;
    public bool rollForward;
    private float rollBoost;

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
        UpdateRollForwardValue();
        rollBoost = this.animator.GetCurrentAnimatorStateInfo(0).IsName("Roll forward") ? 2.5f : 1f;
    }


    void FixedUpdate()
    {
        RotatePlayer();
        MovePlayer();
    }

    public void DebugLog()
    {
        Debug.Log("x: " + floatingJoystick.Horizontal +
            "; y: " + floatingJoystick.Vertical +
            "; direction: " + floatingJoystick.Direction +
            "; speed: " + speedPercent +
            "; rollForward: " + rollForward +
            "; space is pressed: " + Input.GetButton("Jump"));
    }

    private void MovePlayer()
    {
        player.transform.Translate(player.transform.forward * speedPercent * moveSpeed * rollBoost * Time.fixedDeltaTime, Space.World);
    }

    private void RotatePlayer()
    {
        Vector3 lookDirection = Quaternion.Euler(0f, 44f, 0f) * new Vector3(floatingJoystick.Direction.x, 0f, floatingJoystick.Direction.y);
        Quaternion rotation = Quaternion.LookRotation(lookDirection, Vector3.up);

        if (!floatingJoystick.Direction.Equals(Vector2.zero) && rollBoost == 1f)
            player.transform.rotation = rotation;
    }

    private void UpdateSpeedPercentValue()
    {
        speedPercent = floatingJoystick.Direction.sqrMagnitude + 0.01f;
    }

    private void UpdateRollForwardValue()
    {
        rollForward = Input.GetButton("Jump");
    }
}
