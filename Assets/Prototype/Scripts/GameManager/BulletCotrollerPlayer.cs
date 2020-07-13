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
    private float cooldownTime;
    private bool canShoot;

    void Start()
    {
        playerAnimator = GetComponent<PlayerAnimator>();
        playerController = GetComponent<PlayerController>();
        shootPoint = playerController.shootPoint;
        bulletType.GetComponent<BulletAnimator>().bulletSpeed = bulletSpeed;
        canShoot = true;
    }

    void Update()
    {
        if (playerAnimator.enemiesCounter > 0 && !playerController.RollAnimationIsPlaying() && canShoot && playerController.HeadIsFacingWeapon())
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
