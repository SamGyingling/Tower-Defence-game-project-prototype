using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    [SerializeField] GameObject tower1;
    [SerializeField] GameObject tower2;

    private void Awake()
    {
        
    }

    public void PurchaseBallista()
    {
        //Debug.Log("you have purchased one Ballista tower");
        GameManager.Instance.SetTowerToBuild(tower1);
    }
    public void PurchaseBallista2()
    {
        //Debug.Log("you have purchased one Ballista2 tower");
        GameManager.Instance.SetTowerToBuild(tower2);
    }
    
}
