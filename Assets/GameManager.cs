using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    private GameObject towerToBuild;

    void Awake()
    {
        //singleton
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public GameObject TowerToBuild()
    {
        return towerToBuild;
    }
    public void SetTowerToBuild( GameObject gameObject)
    {
        this.towerToBuild = gameObject;
    }
}
