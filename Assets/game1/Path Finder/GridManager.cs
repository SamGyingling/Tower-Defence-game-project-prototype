using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    [SerializeField] Vector2Int gridSize;
    [Tooltip("world grid size - should match unityEditor snap setting.")]
    [SerializeField] int unityGridSize = 10;
    public int UnityGridSize { get { return unityGridSize; } }

    Dictionary<Vector2Int, Node> grid = new Dictionary<Vector2Int, Node>();
    public Dictionary<Vector2Int,Node> Grid{  get { return grid; }}

    void Awake()
    {
        CreateGrid();
    }
    public Node GetNode(Vector2Int coordinates)
    {
        if (grid.ContainsKey(coordinates))
        {
            return grid[coordinates];
        }
        return null;
       
    }
    public void BlockNode(Vector2Int coordinates)
    {
        if (grid.ContainsKey(coordinates))
        {
            grid[coordinates].isWalkable = false;
        }
    }
    public void ResetNodes()
    {
        foreach(KeyValuePair<Vector2Int,Node> entry in grid)
        {
            entry.Value.connectedTo = null;
            entry.Value.isExplored = false;
            entry.Value.isPath = false;
        }
    }
    public Vector2Int GetCoordinatesFromPosition(Vector3 postion)
    {
        Vector2Int coordinates = new Vector2Int();
        coordinates.x = Mathf.RoundToInt( postion.x / unityGridSize);
        coordinates.y = Mathf.RoundToInt( postion.z / unityGridSize);
        return coordinates;
    }
    public Vector3 GetPositionFromCoordinates(Vector2Int coordinates)
    {
        Vector3 position = new Vector3();
        position.x = Mathf.RoundToInt( coordinates.x * unityGridSize);
        position.z = Mathf.RoundToInt(coordinates.y* unityGridSize);
        return position;
    }
    void CreateGrid()
    {
        for(int i = 0; i < gridSize.x; i++)
        {
            for(int j = 0; j < gridSize.y; j++)
            {
                Vector2Int coordinates = new Vector2Int(i, j);
                grid.Add(coordinates, new Node(coordinates, true));
            }
        }
      
    }
}
