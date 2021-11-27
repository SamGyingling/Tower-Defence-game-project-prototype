using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Shop : MonoBehaviour
{
    public Tower tower1;
    public Tower tower2;
    public Tower tower3;
    [SerializeField] Text tower1CostText;
    [SerializeField] Text tower2CostText;
    [SerializeField] Text tower3CostText;
    GameManager gameManager;
    private void Start()
    {
        gameManager = GameManager.Instance;
        tower1CostText.text = "$ " + tower1.cost;
        tower2CostText.text = "$ " + tower2.cost;
        tower3CostText.text = "$ " + tower3.cost;
    }
    public void SelectTower1()
    {
        gameManager.SetTowerToBuild(tower1);
    }
    public void SelectTower2()
    {
        gameManager.SetTowerToBuild(tower2);
    }
    public void SelectTower3()
    {
        gameManager.SetTowerToBuild(tower3);
    }

}
