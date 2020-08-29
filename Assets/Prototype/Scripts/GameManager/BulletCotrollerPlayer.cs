using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletCotrollerPlayer : MonoBehaviour
{
    [SerializeField]
    private GameObject bulletType;

    private PlayerAnimator playerAnimator;
    private PlayerController playerController;
    private Transform shootPoint;

    [SerializeField]
    private float bulletSpeed;
    [SerializeField]
    private float bulletDamage;
    [SerializeField]
    private float cooldownTime;
    private bool canShoot;

    private string enemy;
    private string friend;

    void Start()
    {
        playerAnimator = GetComponent<PlayerAnimator>();
        playerController = GetComponent<PlayerController>();
        shootPoint = playerController.shootPoint;
        bulletSpeed = bulletType.GetComponent<BulletAnimator>().bulletSpeed;
        bulletDamage = bulletType.GetComponent<BulletAnimator>().bulletDamage;
        canShoot = true;
        friend = PlayerManager.instance.player.gameObject.name;
        enemy = EnemyManager.instance.enemyType.gameObject.name;
    }

    void Update()
    {
        /* Debug.Log("some enemies: " + (playerAnimator.enemiesCounter > 0) + 
            "; not rolling: " + !playerController.RollAnimationIsPlaying() +
            "; can shoot: " + canShoot +
            "; angle correct: " + playerController.HeadIsFacingWeapon()); */

        if (playerAnimator.enemiesCounter > 0 && !playerController.RollAnimationIsPlaying() && canShoot && playerController.HeadIsFacingWeapon())
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
