using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletAnimator : MonoBehaviour
{
    public float bulletSpeed = 8.97f;
    public float bulletDamage = 10f;
    public Rigidbody rb;
    public string bullet;
    public string enemy;
    public string friend;
    GameObject hitObject;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        bullet = transform.gameObject.name;
        rb.velocity = transform.forward * bulletSpeed;
    }

    private void OnTriggerEnter(Collider hitInfo)
    {
        if (hitInfo.name == enemy)
        {
            GameObject hitObject = hitInfo.transform.gameObject;
            // Debug.Log(hitObject.name);
                
            // shit code
            if (friend == PlayerManager.instance.player.gameObject.name)
            {
                hitObject.GetComponent<EnemyController>().Hit(bulletDamage);
            }
            else
            {
                hitObject.GetComponent<PlayerController>().Hit(bulletDamage);
            }
        }

        if (hitInfo.name != bullet)
        Destroy(gameObject);
    }
}
