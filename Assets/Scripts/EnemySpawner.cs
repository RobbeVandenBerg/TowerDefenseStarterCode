using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    // Initiate variables
    public static EnemySpawner Instance;

    public List<GameObject> Path1 = new List<GameObject>();
    public List<GameObject> Path2 = new List<GameObject>();
    public List<GameObject> enemies = new List<GameObject>();

    private int ufoCounter = 0;

    private void SpawnEnemy(int type, Enums.Path path)
    {
        var newEnemy = Instantiate(enemies[type], Path1[0].transform.position, Path1[0].transform.rotation);
        var script = newEnemy.GetComponentInParent<Enemy>();
        script.path = path;
        script.target = Path1[1];
        // Increment enemy counter
        GameManager.Instance.AddInGameEnemy();
    }
    // Assign a path to each enemy
    public GameObject RequestTarget(Enums.Path path, int index)
    {
        List<GameObject> currentPath = null;
        switch (path)
        {
            case Enums.Path.Path1:
                currentPath = Path1;
                break;
            case Enums.Path.Path2:
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

    //Spawn tester
    /*private void SpawnTester()
    {
        SpawnEnemy(0, Enums.Path.Path1);
    }
    private void Start()
    {
        InvokeRepeating("SpawnTester", 1f, 1f);
    }*/

    public void StartWave(int number)
    {
        // reset counter 
        ufoCounter = 0;
        switch (number)
        {
            case 1:
                InvokeRepeating("StartWave1", 1f, 1.5f);
                break;
        }
    }
    public void StartWave1()
    {
        ufoCounter++;
        // leave some gaps 
        if (ufoCounter % 6 <= 1) return;
        if (ufoCounter < 30)
        {
            SpawnEnemy(0, Enums.Path.Path1);
        }
        else
        {
            // the last Enemy will be level 2 
            SpawnEnemy(1, Enums.Path.Path1);
        }
        if (ufoCounter > 30)
        {
            CancelInvoke("StartWave1"); // the reverse of InvokeRepeating 
            // depending on your singleton declaration, Get might be somthing else 
            GameManager.Instance.EndWave(); // let the gameManager know. 
        }
    }
}