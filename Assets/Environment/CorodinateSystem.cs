using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[ExecuteAlways]
public class CorodinateSystem : MonoBehaviour
{
    TextMeshPro label;
    Vector2Int coordinate = new Vector2Int();
   
    Color defaultColor = Color.white;
    Color offColor = Color.gray;
    Color exploredColor = Color.yellow;
    Color pathColor = Color.blue;

    GridManager gridManager;  


    // Start is called before the first frame update
    void Awake()
    {
        label = GetComponent<TextMeshPro>();
        gridManager = FindObjectOfType<GridManager>();
        label.enabled=true;
        FindObjectOfType<GridManager>();
        DisplayCoordinate();
       
       
    }

    // Update is called once per frame
    void Update()
    {
        if (Application.isPlaying)
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
        }
        if (!node.isWalkable)
        {
            label.color = offColor;
        }
        else if(node.isPath)
        {
            label.color = pathColor;

        }else if (node.isExplored)
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
       
        coordinate.x = Mathf.RoundToInt(transform.parent.position.x/UnityEditor.EditorSnapSettings.move.x);
        coordinate.y = Mathf.RoundToInt(transform.parent.position.z/UnityEditor.EditorSnapSettings.move.z);
       
        label.text = coordinate.ToString();
        //display name in hierarchy as well
        transform.parent.name = label.text;
        
    }
}
