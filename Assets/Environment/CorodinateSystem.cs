using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[ExecuteAlways]
public class CorodinateSystem : MonoBehaviour
{
    TextMeshPro floorName;
    Vector2Int coordinate = new Vector2Int();
    WayPoint wayPoint;
    Color defaultColor = Color.white;
    Color offColor = Color.gray;
    
    
    // Start is called before the first frame update
    void Awake()
    {
        floorName = GetComponent<TextMeshPro>();
        floorName.enabled=false;
        DisplayCoordinate();
        wayPoint = GetComponentInParent<WayPoint>();
       
    }

    // Update is called once per frame
    void Update()
    {
        if (Application.isPlaying)
        {
            DisplayCoordinate();
        }
        SetFloorNameColor();
        ToggleFloorname();

    }

    private void ToggleFloorname()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {

            floorName.enabled = !floorName.enabled;
        }
    }

    void SetFloorNameColor()
    {
        if (wayPoint.IsPlaceable)
        {
            floorName.color = defaultColor;
        }
        else
        {
            floorName.color = offColor;
        }
    }

    void DisplayCoordinate()
    {
       
        coordinate.x = Mathf.RoundToInt(transform.parent.position.x/UnityEditor.EditorSnapSettings.move.x);
        coordinate.y = Mathf.RoundToInt(transform.parent.position.z/UnityEditor.EditorSnapSettings.move.z);
       
        floorName.text = coordinate.ToString();
        //display name in hierarchy as well
        transform.parent.name = floorName.text;
        
    }
}
