using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [SerializeField] float waitTime=1;
    

    // Start is called before the first fra m e update
    void Start()
    {
        
        StartCoroutine(BuldBody());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator BuldBody()
    {
        foreach(Transform child in transform)
        {
            child.gameObject.SetActive(false);
            foreach(Transform grandChild in child)
            {
                grandChild.gameObject.SetActive(false);
            }
        }
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(true);
            yield return new WaitForSeconds(waitTime);
            foreach (Transform grandChild in child)
            {
                
                grandChild.gameObject.SetActive(true);
            }
        }
       
    }
}
