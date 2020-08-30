using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.Profiling;
//using System.Diagnostics.Eventing.Reader;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    private float scopeRadius = 45f;
    public float lookRadius = 5f;
    private float distanceToTarget;

    // The stats
    [SerializeField]
    private float health = 100;

    public bool agroMode = false;
    public bool playerInTheFreeView = false;

    // Patrol variables
    NavMeshHit navHit; // Saving the point of the NavMesh the agent should patrol to
    private float patrolTimer;
    private Vector3 patrolDestination;
    private bool canPatrol;
    
    private NavMeshAgent agent;
    private Transform target;
    [SerializeField]
    public Transform shootPoint;
    [SerializeField]
    private GameObject enemyHead;

    void Start()
    {
        target = PlayerManager.instance.player.transform;
        agent = GetComponent<NavMeshAgent>();
        canPatrol = true;
        patrolTimer = 4f;
        patrolDestination = agent.transform.position;
    }

    void Update()
    {
        UpdateDistanceToTarget();
        UpdateAgroBehaviour();

        MoveAgent();
    }

    private void UpdateAgroBehaviour()
    {
        if (!PlayerManager.instance.player.activeInHierarchy)
        {
            agroMode = false;
        }
        else
        {
            agroMode = distanceToTarget <= lookRadius;
        }
    }

    private void MoveAgent()
    {
        if (agroMode)
        {
            agent.SetDestination(target.position);

            if (playerInTheFreeView)
            {
                FaceTarget();
            }
        }
        else
        {
            if (canPatrol)
            {
                StartCoroutine(Patrol());
            }
        }
    }

    private IEnumerator Patrol()
    {
        agent.isStopped = true;

        // checking if the destination is farer than the stoppingDistance
        // because the agent won't move untill it would be
        while (Vector3.Distance(agent.transform.position, patrolDestination) <= agent.stoppingDistance) {
            patrolDestination = GetPatrolPoint();
        }
        agent.SetDestination(patrolDestination);

        agent.isStopped = false;

        canPatrol = false;
        yield return new WaitForSeconds(patrolTimer);
        canPatrol = true;
    }

    private Vector3 GetPatrolPoint()
    {
        // Getting a random point in the lookRadius of the agent
        Vector3 randomDirection = UnityEngine.Random.insideUnitSphere * lookRadius + agent.transform.position;

        // Getting that point on the NavMesh in the lookRadius of the agent
        NavMesh.SamplePosition(randomDirection, out navHit, lookRadius, NavMesh.AllAreas);
        
        return navHit.position;
    }

    private void UpdateDistanceToTarget()
    {
        distanceToTarget = Vector3.Distance(target.position, transform.position);
    }

    private void FixedUpdate()
    {
        playerInTheFreeView = PlayerInTheFreeView();
    }

    public bool PlayerInTheScopeView()
    {
        Vector3 direction = target.position - transform.position;
        return Vector3.Angle(enemyHead.transform.forward * -1, direction) < scopeRadius;
    }

    // casting a linecast from the shootpoint height to the player's same height
    public bool PlayerInTheFreeView()
    {
        RaycastHit hit;
        if (Physics.Linecast(enemyHead.transform.position, target.position, out hit))
        {
            if (hit.transform.tag == "Player")
            {
                return true;
            }
            return false;
        }
        return true;
    }

    public void Hit(float damageAmount)
    {
        if (health < damageAmount)
        {
            Debug.Log("Enemy is DEAD!");
            transform.gameObject.SetActive(false);
        }
        else
        {
            health -= damageAmount;
        }
    }

    public bool HeadIsFacingWeapon() => Vector3.Angle(shootPoint.transform.forward, enemyHead.transform.forward * -1) < 30f;

    private void FaceTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = lookRotation;
    }
}
