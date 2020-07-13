using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public float lookRadius = 5f;
    private float distanceToTarget;

    public bool agroMode = false;

    private NavMeshAgent agent;
    private Transform target;
    [SerializeField]
    public Transform shootPoint;

    void Start()
    {
        target = PlayerManager.instance.player.transform;
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        distanceToTarget = Vector3.Distance(target.position, transform.position);

        if (distanceToTarget <= lookRadius)
        {
            agent.SetDestination(target.position);
            agroMode = true;

            if (distanceToTarget <= agent.stoppingDistance)
            {
                FaceTarget();
            }
        }
        else
        {
            agent.ResetPath();
            agroMode = false;
        }
    }

    private void FaceTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = lookRotation;
    }
}
