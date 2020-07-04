using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    private Animator animator;
    private PlayerController playerController;

    private float speedPercent;
    private bool rollForward;

    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        playerController = GetComponentInChildren<PlayerController>();
    }

    void Update()
    {
        animator.SetFloat("speedPercent", playerController.speedPercent);
        animator.SetBool("rollForward", playerController.rollForward);

        playerController.DebugLog();
    }
}
