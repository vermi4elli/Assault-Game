using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletControllerEnemy : MonoBehaviour
{
    [SerializeField]
    private GameObject bulletType;

    private EnemyAnimator enemyAnimator;
    private EnemyController enemyController;
    private Transform shootPoint;

    [SerializeField]
    private float bulletSpeed;
    [SerializeField]
    private float cooldownTime;
    private bool canShoot;

    void Start()
    {
        enemyController = GetComponent<EnemyController>();
        enemyAnimator = GetComponent<EnemyAnimator>();
        shootPoint = enemyController.shootPoint;
        bulletType.GetComponent<BulletAnimator>().bulletSpeed = bulletSpeed;
        canShoot = true;
    }

    void Update()
    {
        if (enemyController.agroMode && canShoot)
        {
            StartCoroutine(Shoot());
        }
    }

    private IEnumerator Shoot()
    {
        Instantiate(bulletType, shootPoint.position, shootPoint.rotation);
        canShoot = false;
        yield return new WaitForSeconds(cooldownTime);
        canShoot = true;
    }
}
