using System.Collections;
using UnityEngine;


public class Tower : MonoBehaviour
{
    public float attackRange = 1f;
    public float attackRate = 1f;
    public int attackDamage = 1;
    public float attackSize = 1f;

    public GameObject bulletPrefab;
    public TowerType type;

    public float projectileSpeed = 5f;
    private float nextAttackTime;

    // Draw the attack range in the editor for easier debugging 
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }

    void Update()
    {
        if (Time.time >= nextAttackTime)
        {
            Attack();
            nextAttackTime = Time.time + 1f / attackRate;
        }
    }

    void Attack()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, attackRange);
        foreach (Collider2D collider in colliders)
        {
            if (collider.CompareTag("Enemy"))
            {
                GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
                bullet.transform.localScale = new Vector3(attackSize, attackSize, 1f);

                Projectile projectile = bullet.GetComponent<Projectile>();
                if (projectile != null)
                {
                    projectile.target = collider.transform;
                    projectile.damage = attackDamage;
                    projectile.speed = projectileSpeed;
                }
                break;
            }
        }
    }
}