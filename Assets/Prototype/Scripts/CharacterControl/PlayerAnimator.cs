using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    const float locomotionAnimationSmoothTime = .1f;

    Animator animator;
    [SerializeField]
    private GameObject player;
    public float speed;

    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        //speed = GetComponent<PlayerMovementMobile>().moveSpeed;
    }

    Vector3 lastPosition = Vector3.zero;
    void FixedUpdate()
    {
        float speedPercent = (player.transform.position - lastPosition).magnitude / .05f;
        Debug.Log("Speed percent: " + speedPercent + 
            "; Speed value: " + (player.transform.position - lastPosition).magnitude);
        lastPosition = player.transform.position;

        //animator.SetFloat("speedPercent", speedPercent, locomotionAnimationSmoothTime, Time.deltaTime);
        animator.SetFloat("speedPercent", speedPercent);
    }
}
