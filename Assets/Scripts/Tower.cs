using System;
using UnityEngine;

public class Tower : MonoBehaviour
{
    public float attackRange = 3f; // Range within which the tower can detect and attack enemies 
    public float attackRate = 1f; // How often the tower attacks (attacks per second) 
    public int attackDamage = 1; // How much damage each attack does 
    public float attackSize = 1f; // How big the bullet looks 
    public float projectileSpeed = 1f; // Speed of the projectile
    public GameObject bulletPrefab; // The bullet prefab the tower will shoot 
    public Enums.TowerType type; // the type of this tower 

    private float attackTimer; // Timer to keep track of attack rate

    // Draw the attack range in the editor for easier debugging 
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }

    void Update()
    {
        // Increment the attack timer
        attackTimer += Time.deltaTime;

        // Check if attack is possible
        if (attackTimer >= 1f / attackRate)
        {
            // Reset attack timer
            attackTimer = 0f;

            // Search for enemies within range
            Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, attackRange);

            foreach (Collider2D collider in colliders)
            {
                if (collider.CompareTag("Enemy"))
                {
                    // Shoot projectile at first enemy found
                    Shoot(collider.gameObject);
                    break;
                }
            }
        }
    }

    void Shoot(GameObject enemy)
    {
        // Instantiate the bullet prefab
        GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);

        // Set bullet properties
        Projectile projectile = bullet.GetComponent<Projectile>();
        if (projectile != null)
        {
            projectile.damage = attackDamage;
            projectile.target = enemy.transform;
            projectile.speed = projectileSpeed; // Set the speed of the projectile
            bullet.transform.localScale = new Vector3(attackSize, attackSize, 1f);
        }
    }
}