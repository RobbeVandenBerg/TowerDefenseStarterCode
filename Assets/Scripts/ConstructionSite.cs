using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using static Enums;

public class ConstructionSite
{

    public Vector3Int TilePosition { get; private set; }
    public Vector3 WorldPosition { get; private set; }
    public SiteLevel Level { get; private set; }
    public TowerType TowerType { get; private set; }
    private GameObject tower;

    public ConstructionSite(Vector3Int tilePosition, Vector3 worldPosition)
    {
        TilePosition = tilePosition;
        WorldPosition = new Vector3(worldPosition.x, worldPosition.y + 0.5f, worldPosition.z);

        tower = null;
    }

    public void SetTower(GameObject newTower, SiteLevel newLevel, TowerType newType)
    {
        //Check if a tower is already present
        if (tower != null)
        {
            //Destroy extant tower
            GameObject.Destroy(tower);
        }
        //New properties
        tower = newTower;
        Level = newLevel;
        TowerType = newType;
    }
    public Vector3 GetBuildPosition()
    {
        return WorldPosition;
    }
}