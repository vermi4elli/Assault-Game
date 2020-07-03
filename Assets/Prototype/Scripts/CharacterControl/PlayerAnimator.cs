using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    [SerializeField]
    private GameObject player;
    [SerializeField]
    private FloatingJoystick floatingJoystic;

    private Animator animator;
    private float horizontal;
    private float vertical;
    private float speedPercent;

    void Start()
    {
        animator = GetComponentInChildren<Animator>();
    }

    Vector3 lastPosition = Vector3.zero;
    void FixedUpdate()
    {
        horizontal = Mathf.Abs(floatingJoystic.Horizontal);
        vertical = Mathf.Abs(floatingJoystic.Vertical);

        speedPercent = horizontal > vertical ? 
            horizontal : vertical;

        Debug.Log("Speed percent: " + speedPercent);

        animator.SetFloat("speedPercent", speedPercent);
    }
}
