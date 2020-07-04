using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private const float moveSpeed = 4f;
    public float speedPercent;
    public bool rollForward;

    [SerializeField]
    private GameObject player;
    [SerializeField]
    private FloatingJoystick floatingJoystick;

    void Update()
    {
        UpdateSpeedPercentValue();
        UpdateRollForwardValue();
    }

    private void UpdateRollForwardValue()
    {
        rollForward = Input.GetButton("Jump");
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
