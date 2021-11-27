using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyMover : MonoBehaviour
{
    [SerializeField] [Range(0f,5f)]float baseSpeed = 1.0f;
    List<Node> path = new List<Node>();
    float speed;
    public bool isAlive;
    Enemy enemy;
    GridManager gridManager;
    PathFinder pathFinder;
    GameManager gameManager;
    
    void OnEnable()
    {
        ResetSpeed();
        ReturnToStartPosition();
        RecalculatePath(true);
    }
    void Awake()
    {
        enemy = GetComponent<Enemy>();
        gridManager = FindObjectOfType<GridManager>();
        pathFinder = FindObjectOfType<PathFinder>();
        gameManager = GameManager.Instance;
    }
    private void Update()
    {

       
    }
    void ReturnToStartPosition()
    {
        transform.position = gridManager.GetPositionFromCoordinates(pathFinder.StartCoordinates);
    }
    public void Slow(float percent)
    {
        speed = baseSpeed * (1f - percent);
    }
    public void ResetSpeed()
    {
        speed = baseSpeed;
    }
    public void RecalculatePath(bool resetPath)
    {
        Vector2Int coordinates = new Vector2Int();
        if (resetPath)
        {
            coordinates = pathFinder.StartCoordinates;
        }
        else
        {
            coordinates = gridManager.GetCoordinatesFromPosition(transform.position);
        }
        StopAllCoroutines();
        path.Clear();
        path = pathFinder.GetNewPath(coordinates);
        //StartCoroutine(FollowPath());
    }
    void FollowPath()
    {
        
        for (int i=1; i<path.Count;i++)
        {
            if (!isAlive)
            {
                return;
            }


            Vector3 startPosition = transform.position;
            Vector3 endPosition = gridManager.GetPositionFromCoordinates(path[i].coordinates);
            float travelPercentage = 0f;

            transform.LookAt(endPosition);
            
            while (travelPercentage < 1)
            {
                    travelPercentage += Time.deltaTime * speed;
                    transform.position = Vector3.Lerp(startPosition, endPosition, travelPercentage);
                    ResetSpeed();
                // yield return new WaitForEndOfFrame();
                
            }

        }
        gameObject.SetActive(false);
        enemy.isAlive = false;
        gameManager.playerHealth -= 30;
        gameManager.enemyCount--;
    }

}
