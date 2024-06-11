using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Projectile : MonoBehaviour
{
    // Initiate variables
    public Transform target;
    public float speed;
    public int damage;
    // Start is called before the first frame update
    void Start()
    {
        // Rotate the projectile towards the target
        if (target != null)
        {
            Vector3 direction = (target.position - transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Lerp(transform.rotation, lookRotation, Time.deltaTime * speed);
        }
    }

    // Update is called once per frame
    void Update()
    {
        // When target is null, it no longer exists and this
        // object has to be removed
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        // Move the projectile towards the target
        float step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, target.position, step);

        // Check if the distance between this object and
        // the target is smaller than 0.2. If so, apply damage and destroy this object.
        if (Vector3.Distance(transform.position, target.position) < 0.2f)
        {
            // Apply damage to the target
            Enemy enemy = target.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.Damage(damage);
            }

            // Destroy the projectile
            Destroy(gameObject);
        }
    }


}