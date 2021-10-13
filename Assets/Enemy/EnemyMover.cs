using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMover : MonoBehaviour
{
    [SerializeField] List<WayPoint> path = new List<WayPoint>();
    [SerializeField] [Range(0f,5f)]float speed = 1.0f;

    // Start is called before the first frame update
    private void OnEnable()
    { 
        FindPath();
        ReturnToStartPosition();
        StartCoroutine(FollowPath());
    }

    private void ReturnToStartPosition()
    {
        transform.position = path[0].transform.position;
    }

    private void FindPath()
    {
        path.Clear();
        GameObject parent = GameObject.FindGameObjectWithTag("Path");
        foreach (Transform child in parent.transform)
        {
            path.Add(child.GetComponent<WayPoint>());
        }
    }

    IEnumerator FollowPath()
    {
        foreach (WayPoint wayPoint in path)
        {
            Vector3 startPosition = transform.position;
            float travelPercentage = 0;
            transform.LookAt(wayPoint.transform.position);
            while (travelPercentage < 1)
            {
                travelPercentage += Time.deltaTime* speed;
                transform.position = Vector3.Lerp(startPosition, wayPoint.transform.position, travelPercentage);          
                yield return new WaitForEndOfFrame();
            }
        }
        gameObject.SetActive(false);
    }

}
