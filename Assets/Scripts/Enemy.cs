using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using Unity.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // Enemy basic values
    public float speed = 1f;
    public float health = 10f;
    public int points = 1;
    public Enums.Path path { get; set; }
    public GameObject target { get; set; }
    private int pathIndex = 1;

    public void Damage(int damage)
    {
        health -= damage;

        if (health <= 0)
        {
            // If enemy should die, delete object and add score
            GameManager.Instance.AddCredits(points);
            GameManager.Instance.RemoveInGameEnemy();
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (target != null)
        {
            // Move enemy along path
            float step = speed * Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position, target.transform.position, step);
            if (Vector2.Distance(transform.position, target.transform.position) < 0.1f)
            {
                pathIndex++;
                target = EnemySpawner.Instance.RequestTarget(path, pathIndex);
                // If enemy reached end of path, attack gate and remove enemy
                if (target == null)
                {
                    Destroy(gameObject);
                    GameManager.Instance.RemoveInGameEnemy();
                    if (path == Enums.Path.Path1)
                    {
                        GameManager.Instance.AttackGate(Enums.Path.Path1);
                    }
                    else if (path == Enums.Path.Path2)
                    {
                        GameManager.Instance.AttackGate(Enums.Path.Path2);
                    }
                }
            }

        }
    }
}