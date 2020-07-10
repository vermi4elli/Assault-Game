﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    private Animator animator;
    private PlayerController playerController;
    [SerializeField]
    private GameObject weapon;

    private int speedPercentHash = Animator.StringToHash("speedPercent");
    private int rollForwardHash = Animator.StringToHash("rollForward");

    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        playerController = GetComponentInChildren<PlayerController>();
    }

    void Update()
    {
        animator.SetFloat(speedPercentHash, playerController.speedPercent);
        animator.SetBool(rollForwardHash, playerController.rollForwardAwaken);

        if (weapon.activeSelf)
        {
            animator.SetLayerWeight(1, 1);
        }

        playerController.DebugLog();
    }
}
