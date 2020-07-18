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
        Debug.Log("some enemies: " + (playerAnimator.enemiesCounter > 0) + 
            "; not rolling: " + !playerController.RollAnimationIsPlaying() +
            "; can shoot: " + canShoot +
            "; angle correct: " + playerController.HeadIsFacingWeapon());

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
