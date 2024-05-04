using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehav : MonoBehaviour
{
    private float speed = 25f;
    public float maxLifetime = 3f; // Maximum lifetime of the bullet before it gets destroyed

    private Vector3 targetPoint;
    private float timer = 0f;

    public void MoveTowards(Vector3 target)
    {
        targetPoint = target;
    }

    void Update()
    {
        if (targetPoint != Vector3.zero)
        {
            Vector3 direction = (targetPoint - transform.position).normalized;
            transform.position += direction * speed * Time.deltaTime;

            timer += Time.deltaTime;

            if (Vector3.Distance(transform.position, targetPoint) < 0.1f || timer >= maxLifetime)
            {
                Destroy(gameObject);
            }
        }
    }
}
