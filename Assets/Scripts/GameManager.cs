using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Enums;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;

    private ConstructionSite selectedSite;
    public GameObject TowerMenu;
    private TowerMenu towerMenu;
    public List<GameObject> Archers = new List<GameObject>();
    public List<GameObject> Swords = new List<GameObject>();
    public List<GameObject> Wizards = new List<GameObject>();
    private int health;
    private int credits;
    private int wave;
    private int currentWave;
    public TopMenu topMenu;
    private bool waveActive = false;
    public int enemyInGameCounter = 0;
    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<GameManager>();
                if (instance == null)
                {
                    GameObject obj = new GameObject("GameManager");
                    instance = obj.AddComponent<GameManager>();
                }
            }
            return instance;
        }
    }
    // Tower costs
    private Dictionary<TowerType, List<int>> towerPrefabCosts = new Dictionary<TowerType, List<int>>()
    {
        { TowerType.Archer, new List<int> { 35, 70, 140 } }, 
        { TowerType.Sword, new List<int> { 60, 135, 185 } }, 
        { TowerType.Wizard, new List<int> { 120, 180, 250 } } 
    };
    public void SelectSite(ConstructionSite site)
    {
        this.selectedSite = site;
        towerMenu.SetSite(site);
    }
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {
        towerMenu = TowerMenu.GetComponent<TowerMenu>();
        StartGame();
    }
    public void Build(TowerType type, SiteLevel level)
    {
        if (selectedSite == null)
        {
            return;
        }
        List<GameObject> towerList = null;
        switch (type)
        {
            case TowerType.Archer:
                towerList = Archers;
                break;
            case TowerType.Sword:
                towerList = Swords;
                break;
            case TowerType.Wizard:
                towerList = Wizards;
                break;
        }

        GameObject towerPrefab = towerList[(int)level];
        Vector3 buildPosition = selectedSite.GetBuildPosition();

        GameObject towerInstance = Instantiate(towerPrefab, buildPosition, Quaternion.identity);
        int towerCost = GetCost(type, level);
        AddCredits(-towerCost);
        selectedSite.SetTower(towerInstance, level, type); 
        towerMenu.SetSite(null);
    }
    public void StartGame()
    {
        credits = 500;
        health = 10;
        currentWave = 0;
        waveActive = false;
        UpdateUI();
    }
    public void UpdateUI()
    {
        if (topMenu != null)
        {
            topMenu.SetCreditsLabel("Credits: " + credits);
            topMenu.SetGateHealthLabel("Gate Health: " + health);
            topMenu.SetWaveLabel("Wave: " + currentWave);
        }
    }

    public int GetCredits()
    {
        return credits;
    }
    public void RemoveCredits(int amount)
    {
        credits -= amount;
        topMenu.SetCreditsLabel("Credits: " + credits);
    }
    public void AttackGate(Path path)
    {
        if (path == Path.Path1 || path == Path.Path2)
        {
            health--;
        }
        else
        {
            Debug.LogWarning("Unkown path: " + path);
        }
    }
    public void AddCredits(int amount)
    {
        credits += amount;
        topMenu.SetCreditsLabel("Credits: " + credits);
    }
    public int GetCost(TowerType type, SiteLevel level, bool selling = false)
    {
        int cost = 0;

        if (selling)
        {
            cost = towerPrefabCosts[type][(int)level] / 2;
        }
        else
        {
            cost = towerPrefabCosts[type][(int)level];
        }

        return cost;
    }
    public void StartWave()
    {
        currentWave = 0;
        currentWave++;
        topMenu.SetWaveLabel("Wave: " + currentWave);
        waveActive = true;

    }
    public void EndWave()
    {
        waveActive = false;
    }
    public void AddInGameEnemy()
    {
        enemyInGameCounter++;
    }
    public void RemoveInGameEnemy()
    {
        enemyInGameCounter--;
        if (waveActive = false && enemyInGameCounter <= 0)
        {
            if (currentWave == 1)
            {
                return;
            }
            else
            {
                topMenu.EnableWaveButton();
            }
        }
    }
}