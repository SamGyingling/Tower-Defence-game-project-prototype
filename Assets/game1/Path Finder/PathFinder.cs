using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFinder : MonoBehaviour
{
     
    [SerializeField] Vector2Int startCoordinates;
    public Vector2Int StartCoordinates { get { return startCoordinates; } }
    [SerializeField] Vector2Int destinationCoordinates;
    public Vector2Int DestinationCoordinates { get { return destinationCoordinates; } }

    Node currentSearchNode;
    Node startNode;
    Node destinationNode;

    Queue<Node> frontier = new Queue<Node>();
    Dictionary<Vector2Int, Node> reachedNode = new Dictionary<Vector2Int, Node>();

    GridManager gridManager;
    Dictionary<Vector2Int, Node> grid = new Dictionary<Vector2Int, Node>();
    Vector2Int[] directions = { Vector2Int.left, Vector2Int.up, Vector2Int.right, Vector2Int.down };

    void Awake()
    {
        gridManager = FindObjectOfType<GridManager>();
        if (gridManager != null)
        {
            grid = gridManager.Grid;
            startNode = grid[startCoordinates];
            destinationNode = grid[destinationCoordinates];
        }
    }
    void Start()
    {
        GetNewPath();
    }
    public List<Node> GetNewPath()
    {
        return GetNewPath(startCoordinates);
    }
    public List<Node> GetNewPath(Vector2Int coordinates)
    {
        gridManager.ResetNodes();
        BreadthFirstSearch(coordinates);
        return BuildPath();
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
            if (!reachedNode.ContainsKey(neighboor.coordinates)&& neighboor.isWalkable)
            {
                frontier.Enqueue(neighboor);
                reachedNode.Add(neighboor.coordinates, neighboor);
                neighboor.connectedTo = currentSearchNode;
            }
        }
    }
    void BreadthFirstSearch(Vector2Int coordinates)
    {
        startNode.isWalkable = true;
        destinationNode.isWalkable = true;
       
        frontier.Clear();
        reachedNode.Clear();
       
        
        frontier.Enqueue(grid[coordinates]);
        reachedNode.Add(coordinates, grid[coordinates]);
        
        while (frontier.Count > 0)
        {
            currentSearchNode = frontier.Dequeue();
            currentSearchNode.isExplored = true;
            ExploreNeighbors();
             
            if (currentSearchNode == destinationNode)
            {
               
            }
        }
    }
    List<Node> BuildPath()
    {
        List<Node> path = new List<Node>();
        Node currentNode = destinationNode;
        path.Add(currentNode);
        currentNode.isPath = true;
        while (currentNode.connectedTo != null)
        {
            currentNode = currentNode.connectedTo;
            path.Add(currentNode);
            currentNode.isPath = true;
        }
        path.Reverse();

        return path;
    }
    public bool willBlockPath(Vector2Int coordinates)
    {

        if (grid.ContainsKey(coordinates))
        { 
            bool prevoiusState = grid[coordinates].isWalkable;
            grid[coordinates].isWalkable = false;
            List<Node> newPath = GetNewPath();
            grid[coordinates].isWalkable = prevoiusState;
            if (newPath.Count<=1)
            {
                GetNewPath();
                return true;
            }
        }
        return false;
    }
   
}
