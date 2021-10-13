using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayPoint : MonoBehaviour
{
    [SerializeField] GameObject towerPrefab;
    [SerializeField] bool isplaceable;
    public bool IsPlaceable{
        get { return isplaceable; }
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    
    }
    private void OnMouseDown()
    {
        if (isplaceable)
        {
            Instantiate(towerPrefab, transform.position,Quaternion.identity);
            isplaceable = false;
        }
       
    }
}
