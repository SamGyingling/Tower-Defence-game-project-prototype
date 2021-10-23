using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class Tile : MonoBehaviour
{
     GameObject towerPrefab;
    [SerializeField] bool isplaceable;
    public bool IsPlaceable{get { return isplaceable; }}
    GameManager gameManager;
    GridManager gridManager;
    PathFinder pathFinder;
    ObjectPool objectPool;
    
    Vector2Int coordinates = new Vector2Int();
    
    void Awake()
    {
        gridManager = FindObjectOfType<GridManager>();
        pathFinder = FindObjectOfType<PathFinder>();
        objectPool = FindObjectOfType<ObjectPool>();
       
    }

    void Start()
    {
        if (gridManager != null)
        {
            coordinates = gridManager.GetCoordinatesFromPosition(transform.position);
            if (!isplaceable){
               gridManager.BlockNode(coordinates);
            }
        }
        gameManager = GameManager.Instance;
    }
    private void OnMouseOver()
    {
       
    }
    
    void OnMouseDown()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {//will prevent clicking through canvas buttons 
            return;
        }
        towerPrefab = gameManager.TowerToBuild();
        if (towerPrefab == null)
        {
            return;
        }

        else if (gridManager.GetNode(coordinates).isWalkable && !pathFinder.willBlockPath(coordinates))
        {
           
            
            Debug.Log(towerPrefab);
            bool isSuccesful = Instantiate(towerPrefab, transform.position, Quaternion.identity);
            if (isSuccesful)
            {
                gridManager.BlockNode(coordinates);
                objectPool.notifyReceivers();
            }
        }
    }
}
