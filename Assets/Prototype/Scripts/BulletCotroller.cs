using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletCotroller : MonoBehaviour
{
    [SerializeField]
    private GameObject bulletType;
    [SerializeField]
    private GameObject player;
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
        playerAnimator = player.GetComponent<PlayerAnimator>();
        playerController = player.GetComponent<PlayerController>();
        shootPoint = playerController.shootPoint;
        bulletType.GetComponent<BulletAnimator>().bulletSpeed = bulletSpeed;
        canShoot = true;
    }

    // Update is called once per frame
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
