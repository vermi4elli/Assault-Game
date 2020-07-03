using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    [SerializeField]
    private FloatingJoystick floatingJoystick;

    private Animator animator;
    private float speedPercent;

    void Start()
    {
        animator = GetComponentInChildren<Animator>();
    }

    void FixedUpdate()
    {
        UpdateSpeedPercentValue();
        animator.SetFloat("speedPercent", speedPercent);
        
        //DebugLogger();
    }

    private void DebugLogger()
    {
        Debug.Log("Speed percent: " + speedPercent);
    }

    private void UpdateSpeedPercentValue()
    {
        speedPercent = floatingJoystick.Direction.sqrMagnitude + 0.01f;
    }
}
