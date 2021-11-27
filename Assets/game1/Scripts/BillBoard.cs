using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BillBoard : MonoBehaviour
{

    Cam cam;
    
    
    private void Start()
    {
        cam = FindObjectOfType<Cam>();
        
    }
    private void LateUpdate()
    {
       transform.LookAt(transform.position + cam.transform.forward);
    }
    
}
