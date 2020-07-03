using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public const float moveSpeed = 5f;
    private float speedPercent;
    private float horizontal;
    private float vertical;

    [SerializeField]
    private GameObject player;
    [SerializeField]
    private FloatingJoystick floatingJoystick;

    void FixedUpdate()
    {
        horizontal = Mathf.Abs(floatingJoystick.Horizontal);
        vertical = Mathf.Abs(floatingJoystick.Vertical);

        speedPercent = horizontal > vertical ?
            horizontal : vertical;

        Vector3 direction = Vector3.forward * floatingJoystick.Vertical + Vector3.right * floatingJoystick.Horizontal;

        Vector3 lookDirection = Quaternion.Euler(0f, 44f, 0f) * new Vector3(floatingJoystick.Direction.x, 0f, floatingJoystick.Direction.y);
        Quaternion rotation = Quaternion.LookRotation(lookDirection, Vector3.up);

        Debug.Log("Vertical: " + floatingJoystick.Vertical + 
            "; Horizontal: " + floatingJoystick.Horizontal + 
            "; Look Direction: " + floatingJoystick.Direction);
        
        player.transform.rotation = rotation;
        player.transform.Translate(player.transform.forward * speedPercent * moveSpeed * Time.fixedDeltaTime, Space.World);
    }
}
