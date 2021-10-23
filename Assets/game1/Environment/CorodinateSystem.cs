using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[ExecuteAlways]
[RequireComponent(typeof(TextMeshPro))]
public class CorodinateSystem : MonoBehaviour
{
    TextMeshPro label;

    Vector2Int coordinate = new Vector2Int();
   
    Color defaultColor = Color.black;
    Color offColor = Color.gray;
    Color exploredColor = Color.yellow;
    Color pathColor = Color.blue;

    GridManager gridManager;
    Tile tile;

    // Start is called before the first frame update
    void Awake()
    {
        label = GetComponent<TextMeshPro>();
        gridManager = FindObjectOfType<GridManager>();
        tile = GetComponentInParent<Tile>();

        label.enabled=true;
        DisplayCoordinate();
        SetLabelColor();
    }

    // Update is called once per frame
    void Update()
    {
        if (!Application.isPlaying)
        {
            DisplayCoordinate();
        }
        SetLabelColor();
        ToggleFloorname();
    }

    private void ToggleFloorname()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {

            label.enabled = !label.enabled;
        }
    }

    void SetLabelColor()
    {
        if (gridManager == null) { return; }

        Node node= gridManager.GetNode(coordinate);
        if(node == null)
        {
            return;
        }else if (!node.isWalkable|| !tile.IsPlaceable)
        {
            label.color = offColor;
        }
        else if(node.isPath)
        {
            label.color = pathColor;
        }
        else if (node.isExplored)
        {
            label.color = exploredColor;
        }
        else
        {
            label.color = defaultColor;
        }
    }

    void DisplayCoordinate()
    {
        if (gridManager == null) { return; }
        coordinate.x = Mathf.RoundToInt(transform.parent.position.x/gridManager.UnityGridSize);
        coordinate.y = Mathf.RoundToInt(transform.parent.position.z/gridManager.UnityGridSize);
       
        label.text = coordinate.ToString();
        //display name in hierarchy as well
        transform.parent.name = label.text;
        
    }
}
