using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public static EnemySpawner Instance;

    public List<GameObject> Path1 = new List<GameObject>();
    public List<GameObject> Path2 = new List<GameObject>();
    public List<GameObject> enemies = new List<GameObject>();

    private void SpawnEnemy(int type, Path path)
    {
        var newEnemy = Instantiate(enemies[type], Path1[0].transform.position, Path1[0].transform.rotation);
        var script = newEnemy.GetComponentInParent<Enemy>();
        script.path = path;
        script.target = Path1[1];
    }
    public GameObject RequestTarget(Path path, int index)
    {
        List<GameObject> currentPath = null;
        switch (path)
        {
            case Path.Path1:
                currentPath = Path1;
                break;
            case Path.Path2:
                currentPath = Path2;
                break;
            default:
                Debug.LogError("invalid path");
                break;
        }
        if (currentPath == null || index < 0 || index >= currentPath.Count)
        {
            Debug.LogError("invalid index or path");
            return null;
        }
        else { return currentPath[index]; }
    }
    private void SpawnTester()
    {
        SpawnEnemy(0, Path.Path1);
    }
    private void Start()
    {
        InvokeRepeating("SpawnTester", 1f, 1f);
    }
}