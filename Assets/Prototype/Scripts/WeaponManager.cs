using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace MyCompany.Assault.Prototype
{
    public class WeaponManager : MonoBehaviour
    {
        [System.Serializable]
        public struct WeaponManagerData
        {
            public GameObject projectilePrefab;
            public Transform projectileSpawnPoint;
        }

        private GameObject projectilePrefab;
        private Transform projectileSpawnPoint;

        public WeaponManager(WeaponManagerData weaponData)
        {
            projectilePrefab = weaponData.projectilePrefab;
            projectileSpawnPoint = weaponData.projectileSpawnPoint;
        }

        public void Use()
        {
            SpawnProjectile();
        }

        private void SpawnProjectile()
        {
            GameObject spawnedProjectile = GameObject.Instantiate(projectilePrefab
                , projectileSpawnPoint.transform.position
                , projectileSpawnPoint.transform.rotation);
            Projectile projectile = spawnedProjectile.AddComponent<Projectile>();
            projectile.Init(projectileSpawnPoint.transform.forward);
            projectile.Shoot();
        }
    }
}