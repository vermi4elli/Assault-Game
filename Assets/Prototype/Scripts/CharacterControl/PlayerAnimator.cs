using System.Collections;
using System.Collections.Generic;
//using System.Diagnostics.Eventing.Reader;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    private Animator animator;
    private PlayerController playerController;
    [SerializeField]
    private GameObject weapon;
    [SerializeField]
    private GameObject spine;
    [SerializeField]
    private GameObject characterRotation;
    [SerializeField]
    private GameObject testPrefab;
    [SerializeField]
    private bool weaponIsActive;

    private Vector2 moveJoystickDirection;
    private Vector2 shootJoystickDirection;
    private float rotationDifference;

    // temp value to test the shooting animation
    public int enemiesCounter;

    // used to change the animations
    private int speedPercentHash = Animator.StringToHash("speedPercent");
    private int rollForwardHash = Animator.StringToHash("rollForward");
    private int rotationDifferenceHash = Animator.StringToHash("rotationDifference");

    // stores the value of the shooting direction of the player
    private Quaternion shootingDirection;

    public bool WeaponIsActive { get => weaponIsActive; set => weaponIsActive = value; }

    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        playerController = GetComponentInChildren<PlayerController>();
    }

    void Update()
    {
        shootJoystickDirection = playerController.shootJoystickDirection;
        moveJoystickDirection = playerController.moveJoystickDirection;

        shootingDirection = playerController.shootDirection;
        rotationDifference = Vector2.Angle(shootJoystickDirection, moveJoystickDirection);

        //Debug.Log("shoot direction: " + shootJoystickDirection + 
        //    "move direction: " + moveJoystickDirection + 
        //    "rotation difference: " + rotationDifference +
        //    "character rotation will be applied" + (shootJoystickDirection == Vector2.zero));

        animator.SetFloat(rotationDifferenceHash, rotationDifference);
        animator.SetFloat(speedPercentHash, playerController.speedPercent);
        animator.SetBool(rollForwardHash, playerController.rollForwardAwaken);

        playerController.rollForwardAwaken = false;

        // turning the weapon on/off depending on the boolean weaponIsActive
        weapon.SetActive(WeaponIsActive);

        // turning the hold animation on
        if (weapon.activeSelf)
        {
            animator.SetLayerWeight(1, 1);

            // turning the shoot animation on
            if (enemiesCounter > 0 && !playerController.RollAnimationIsPlaying())
            {
                animator.SetLayerWeight(2, 1);
            }
            else
            {
                animator.SetLayerWeight(2, 0);
            }
        }
        else
        {
            animator.SetLayerWeight(1, 0);
        }

        //Debug.Log("Shooting direction: " + shootingDirection);

        //playerController.DebugLog();
    }

    private void LateUpdate()
    {
        if (shootJoystickDirection != Vector2.zero)
        {
            spine.transform.rotation = shootingDirection;
        }
    }
}
