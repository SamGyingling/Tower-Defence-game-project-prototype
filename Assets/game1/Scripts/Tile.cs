using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class Tile : MonoBehaviour
{
  
    [SerializeField] bool isplaceable;
    public bool IsPlaceable{get { return isplaceable; }}

    GameManager gameManager;
    GridManager gridManager;
    PathFinder pathFinder;
    ObjectPool objectPool;
    Tower towerToBuild;

    Vector2Int coordinates = new Vector2Int();

    private void Awake()
    {
        gridManager = FindObjectOfType<GridManager>();
        pathFinder = FindObjectOfType<PathFinder>();
        objectPool = FindObjectOfType<ObjectPool>();
        gameManager = GameManager.Instance;
    }
    private void Start()
    {
        if (gridManager != null)
        {
            coordinates = gridManager.GetCoordinatesFromPosition(transform.position);
            if (!isplaceable){
               gridManager.BlockNode(coordinates);
            }
        }
        
    }

    private void OnMouseDown()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {//will prevent clicking through canvas buttons 
            return;
        }
        //build tower
        towerToBuild = gameManager.TowerToBuild();
        if (towerToBuild == null|| gameManager.money < towerToBuild.cost)
        {
            //visual feedback if can not build on this tile
            GetComponentInChildren<Renderer>().material.SetColor("_Color", Color.red);
            return;
        }
        else if (gridManager.GetNode(coordinates).isWalkable && !pathFinder.willBlockPath(coordinates)&&isplaceable)
        {
            GameObject tower = (GameObject)Instantiate(towerToBuild.towerPrefab, transform.position, Quaternion.identity);
            if (tower!=null)
            {
                gameManager.SetMoney(-towerToBuild.cost); //decrese money for buying tower and
                gridManager.BlockNode(coordinates);
                objectPool.notifyReceivers();
                isplaceable = false;
            }
        }
    }
    private void OnMouseEnter()
    {
        //change tile color
        if (isplaceable)
        {
            GetComponentInChildren<Renderer>().material.SetColor("_Color", Color.green);
        }
        else
        {
            GetComponentInChildren<Renderer>().material.SetColor("_Color", Color.red);
        }
    }
    private void OnMouseExit()
    {
        GetComponentInChildren<Renderer>().material.SetColor("_Color", Color.white);
    }
}
