using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFinder : MonoBehaviour
{
     
    [SerializeField] Vector2Int startCoordinates;
    [SerializeField] Vector2Int destinationCoordinates;

    Node currentSearchNode;
    Node startNode;
    Node destinationNode;

    Queue<Node> frontier = new Queue<Node>();
    Dictionary<Vector2Int, Node> reached = new Dictionary<Vector2Int, Node>();

    GridManager gridManager;
    Dictionary<Vector2Int, Node> grid = new Dictionary<Vector2Int, Node>();
    Vector2Int[] directions = { Vector2Int.left, Vector2Int.up, Vector2Int.right, Vector2Int.down };

    void Awake()
    {
        gridManager = FindObjectOfType<GridManager>();
        grid = gridManager.Grid;
        startNode = new Node(startCoordinates,true);
        destinationNode = new Node(destinationCoordinates,true);
    }
    void Start()
    {
        BreadthFirstSearch();
    }
    void ExploreNeighbors()
    {
        List<Node> neighbors = new List<Node>();

        foreach(Vector2Int dirction in directions)
        {
            Vector2Int neighborCoor = currentSearchNode.coordinates + dirction;
            
            if (grid.ContainsKey(neighborCoor))
            {
                neighbors.Add(grid[neighborCoor]);
            }    
        }
        foreach(Node neighboor in neighbors)
        {
            if (!reached.ContainsKey(neighboor.coordinates)&& neighboor.isWalkable)
            {
                frontier.Enqueue(neighboor);
                reached.Add(neighboor.coordinates, neighboor);
            }
        }
    }
    void BreadthFirstSearch()
    {
        bool isRunning = true;
        frontier.Enqueue(startNode);
        reached.Add(startCoordinates, startNode);
        
        while (frontier.Count > 0)
        {
            currentSearchNode = frontier.Dequeue();
            currentSearchNode.isExplored = true;
            ExploreNeighbors();
            if (currentSearchNode == destinationNode)
            {
                isRunning = false;
            }
        }
    }
}
