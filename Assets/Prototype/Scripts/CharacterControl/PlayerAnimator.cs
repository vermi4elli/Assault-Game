using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    private Animator animator;
    private PlayerController playerController;
    [SerializeField]
    private GameObject weapon;
    [SerializeField]
    private bool weaponIsActive;

    // temp value to test the shooting animation
    public int enemiesCounter;

    // used to change the animations
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

        // turning the weapon on/off depending on the boolean weaponIsActive
        weapon.SetActive(weaponIsActive);

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

        playerController.DebugLog();
    }
}
