using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private float lifetime;
    private Vector3 direction;
    private float velocity;
    private bool isAlive;

    public void Init(Vector3 direction, float velocity = 20.0f, float lifetime = 1.0f)
    {
        this.lifetime = lifetime;
        this.velocity = velocity;
        this.direction = direction;
    }

    private void Update()
    {
        if (isAlive)
        {
            transform.position += direction * Time.deltaTime * velocity;
        }
    }

    public void Shoot()
    {
        StartCoroutine(DeathTimer(lifetime));
        isAlive = true;
    }

    private IEnumerator DeathTimer(float timer)
    {
        yield return new WaitForSeconds(timer);
        Destroy(gameObject);
    }
}
