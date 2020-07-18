﻿using System.Collections;
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
    private GameObject testPrefab;
    [SerializeField]
    private bool weaponIsActive;
    private FloatingJoystick shootJoystick;

    // temp value to test the shooting animation
    public int enemiesCounter;

    // used to change the animations
    private int speedPercentHash = Animator.StringToHash("speedPercent");
    private int rollForwardHash = Animator.StringToHash("rollForward");

    // stores the value of the shooting direction of the player
    private Quaternion shootingDirection;

    public bool WeaponIsActive { get => weaponIsActive; set => weaponIsActive = value; }

    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        playerController = GetComponentInChildren<PlayerController>();
        shootJoystick = playerController.shootJoystick;
    }

    void Update()
    {
        animator.SetFloat(speedPercentHash, playerController.speedPercent);
        animator.SetBool(rollForwardHash, playerController.rollForwardAwaken);
        playerController.rollForwardAwaken = false;

        shootingDirection = playerController.shootDirection;
        //if (!shootingDirection.Equals(Vector3.zero))
        //{
        //    spine.transform.rotation = shootingDirection;
        //}
        //else
        //{
        //    spine.transform.rotation = PlayerManager.instance.player.transform.rotation;
        //}

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
        if (shootJoystick.Direction != Vector2.zero)
            spine.transform.rotation = shootingDirection;
    }
}
