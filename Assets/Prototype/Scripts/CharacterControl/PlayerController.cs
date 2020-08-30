using System;
using System.Collections;
using System.Collections.Generic;
using System.IO.IsolatedStorage;
using UnityEditor.Experimental.GraphView;
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
    [SerializeField]
    private float precisionAngle = 45f;

    // The stats for experience
    [SerializeField]
    private float level;

    // The stats for fights
    [SerializeField]
    private float health = 100f;
    [SerializeField]
    private float armor;

    // Passed to the PlayerAnimator to awaken animations
    public float speedPercent;
    public Quaternion shootDirection;
    public bool rollForwardAwaken;

    // Needed for the UpdateRollForwardAwakenValue function
    private float touchBegan;

    // Needed for checking if the animation is finished and checking it's name
    private Animator animator;
    private PlayerAnimator playerAnimator;

    // Needed for correct shooting
    [SerializeField]
    private GameObject player;
    [SerializeField]
    private GameObject playerHead;
    [SerializeField]
    public Transform shootPoint;

    // The joysticks
    [SerializeField]
    private FloatingJoystick moveJoystick;
    [SerializeField]
    public FloatingJoystick shootJoystick;
    public Vector2 moveJoystickDirection;
    public Vector2 shootJoystickDirection;

    private float rotationDegreeWithTwoHandedWeapon = 90f;

    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        playerAnimator = GetComponent<PlayerAnimator>();
    }

    void Update()
    {
        UpdateJoystickDirections();
        UpdateShootRotationValue();
        UpdateRollForwardAwakenValue();
        UpdateSpeedPercentValue();

        // DebugLog();
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

    public void DebugLog()
    {
        //Debug.Log("x: " + moveJoystick.Horizontal +
        //    "; y: " + moveJoystick.Vertical +
        //    "; direction: " + moveJoystick.Direction +
        //    "; speed: " + speedPercent +
        //    "; rollingForward: " + RollAnimationIsPlaying() +
        //    "; space is pressed: " + rollForwardAwaken);

        Debug.Log(moveJoystickDirection);
        //Debug.Log("x: " + moveJoystickDirection.x + "; y: " + moveJoystickDirection.y);
        //Debug.Log("x: " + Input.GetAxis("Horizontal") + "; y: " + Input.GetAxis("Vertical"));
    }

    private void UpdateJoystickDirections()
    {
        moveJoystickDirection = moveJoystick.Direction;
        shootJoystickDirection = shootJoystick.Direction;
    }

    private void UpdateSpeedPercentValue()
    {
        speedPercent = moveJoystickDirection.sqrMagnitude + 0.01f;

#if UNITY_EDITOR
        speedPercent = moveJoystickDirection.Equals(Vector2.zero) ? 0f : 1f;
#endif
    }
    private void UpdateShootRotationValue()
    {
        Vector3 lookDirection = Quaternion.Euler(0f, rotationDegreeWithTwoHandedWeapon, 0f) * 
            new Vector3(shootJoystickDirection.x, 0f, shootJoystickDirection.y) * -1;
        if (lookDirection != Vector3.zero)
        {
            Quaternion rotation = Quaternion.LookRotation(lookDirection, -Vector3.up);
            shootDirection = rotation;
        }
    }

    private void UpdateRollForwardAwakenValue()
    {
        if (Input.touchCount > 0)
        {
            bool foundMoveTouch = false;
            foreach (Touch touch in Input.touches)
            {
                if (touch.position.x < Screen.width / 2)
                {
                    foundMoveTouch = true;
                    CheckRollForwardInput(touch);
                }
            }

            if (!foundMoveTouch) rollForwardAwaken = false;
        }

#if UNITY_EDITOR

        rollForwardAwaken = Input.GetKeyDown(KeyCode.Space);
        moveJoystickDirection = new Vector2(Input.GetAxisRaw("Horizontal"), 
                                            Input.GetAxisRaw("Vertical"));

#endif
    }


    private void RollForwardPlayer()
    {
        player.transform.Translate(player.transform.forward * rollSpeed * Time.fixedDeltaTime, Space.World);
    }

    public bool RollAnimationIsPlaying()
    {
        return animator.GetCurrentAnimatorStateInfo(0).IsName("Roll forward");
    }

    public void Hit(float damageAmount)
    {
        if (health < damageAmount)
        {
            Debug.Log("You are DEAD!");
            PlayerManager.instance.player.SetActive(false);
        }
        else
        {
            health -= damageAmount;
        }
    }

    public bool HeadIsFacingWeapon()
    {
        float resultingAngle = Vector3.Angle(shootPoint.transform.forward, playerHead.transform.forward * -1);

        //Debug.Log("angle: " + resultingAngle +
        //    "; precision: " + precisionAngle +
        //    "; correct: " + (resultingAngle < precisionAngle));
        
        return (resultingAngle < precisionAngle);
    }

    public bool AnimatorIsInTransition()
    {
        return animator.IsInTransition(0) || animator.IsInTransition(1) || animator.IsInTransition(2);
    }
    

    private void MovePlayer()
    {
        if (!moveJoystick.Direction.Equals(Vector2.zero))
            player.transform.Translate(player.transform.forward * speedPercent * moveSpeed * Time.fixedDeltaTime, Space.World);

#if UNITY_EDITOR
        player.transform.Translate(player.transform.forward * speedPercent * moveSpeed * Time.fixedDeltaTime, Space.World);
#endif
    }

    private void RotatePlayer()
    {
        if (moveJoystickDirection != Vector2.zero)
        {
            Vector3 lookDirection = Quaternion.Euler(0f, 44f, 0f) * new Vector3(moveJoystickDirection.x, 0f, moveJoystickDirection.y);
            Quaternion rotation = Quaternion.LookRotation(lookDirection, Vector3.up);

            player.transform.rotation = rotation;
        }
    }

    

    private void CheckRollForwardInput(Touch touch)
    {
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
}
