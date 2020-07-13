using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletAnimator : MonoBehaviour
{
    public float bulletSpeed;
    public Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.velocity = transform.forward * bulletSpeed;
    }

    private void OnTriggerEnter(Collider hitInfo)
    {
        //Debug.Log(hitInfo.name);
        if (hitInfo.name != "Male Model")
            Destroy(gameObject);
    }
}
