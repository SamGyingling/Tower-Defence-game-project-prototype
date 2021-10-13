using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [SerializeField] GameObject[] objectPool;
    [SerializeField] GameObject enemy;
    [SerializeField]int poolsize=5;
    [SerializeField] float waitTime;

    void Awake()
    {
       GeneratePool();
       StartCoroutine(PlacingObjectsInScene());
    }
    void GeneratePool()
    {
        objectPool = new GameObject[poolsize];
        for(int i = 0; i < poolsize; i++)
        {
            objectPool[i] = Instantiate(enemy,transform);
            objectPool[i].SetActive(false);
        }
    }
    void EnableObjectsInPool()
    {
        for (int i = 0; i < objectPool.Length; i++)
        {
            if (!objectPool[i].activeInHierarchy)
            {
                objectPool[i].SetActive(true);
                return;
            }
        }
    }
    IEnumerator PlacingObjectsInScene()
    {
        while (true)
        {
            EnableObjectsInPool();
            yield return new WaitForSeconds(waitTime);
        }
    }
}
