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
    private string friend;
    private string enemy;

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
        friend = enemyController.transform.gameObject.name;
        enemy = PlayerManager.instance.player.gameObject.name;
    }

    void Update()
    {
        if (enemyController.agroMode && 
            canShoot && 
            enemyController.PlayerInTheScopeView() && 
            enemyController.playerInTheFreeView &&
            enemyController.HeadIsFacingWeapon())
        {
            StartCoroutine(Shoot());
        }
    }

    private IEnumerator Shoot()
    {
        GameObject bullet = Instantiate(bulletType, shootPoint.position, shootPoint.rotation);
        bullet.GetComponent<BulletAnimator>().friend = friend;
        bullet.GetComponent<BulletAnimator>().enemy = enemy;
        canShoot = false;
        yield return new WaitForSeconds(cooldownTime);
        canShoot = true;
    }
}
