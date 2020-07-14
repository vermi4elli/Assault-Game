using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAnimator : MonoBehaviour
{
    private Animator animator;
    private EnemyController enemyController;
    private NavMeshAgent agent;
    private int speedPercentHash = Animator.StringToHash("speedPercent");

    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        enemyController = GetComponentInChildren<EnemyController>();
        agent = GetComponentInChildren<NavMeshAgent>();
        animator.SetLayerWeight(1, 1);
    }

    void Update()
    {
        // Debug.Log("Current agent velocity is: " + agent.velocity);
        
        animator.SetFloat(speedPercentHash, agent.velocity.magnitude / agent.speed);

        if (enemyController.agroMode &&
            enemyController.PlayerInTheScopeView() &&
            enemyController.playerInTheFreeView)
        {
            animator.SetLayerWeight(2, 1);
        }
        else
        {
            animator.SetLayerWeight(2, 0);
        }
    }
}
