using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] Text moneyText;
    [SerializeField] Text roundsText;
    [SerializeField] float startMoney;
    [SerializeField] GameObject pauseMenu;
    [SerializeField] Slider health;
    public GameObject gameFinshCamvas;
    public static GameManager Instance;

    public int enemyCount=0;
    public int rounds;
    public float money=0;
    public float playerHealth=0;
    public bool gameOver=true;


    private Tower towerToBuild;

    private void Awake()
    {
        //singleton
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        InitializeGame();
    }
    private void InitializeGame()
    {
        money = startMoney;
        enemyCount = 0;
        playerHealth = 100;
        health.maxValue = playerHealth;
        rounds = 0;
        gameFinshCamvas.SetActive(false);
        Time.timeScale = 1;
        gameOver = false;
    }

    private void Update()
    {
        UpdateUserInterface();
        health.value = playerHealth;
        if (playerHealth <= 0)
        {
            Time.timeScale = 0;
            gameFinshCamvas.SetActive(true);
            roundsText.text = rounds.ToString();
        }
        if (Input.GetKeyDown(KeyCode.Escape) && gameOver == false)
        {
            Toggle();
        }
    }
    private void UpdateUserInterface()
    {
        moneyText.text = "$ " + money+"\n"+"Round:"+ rounds;
    }
    public void SetTowerToBuild( Tower tower)
    {
        this.towerToBuild = tower;
    }
    public void SetMoney(float _money)
    {
        this.money += _money;
    }
    public void Toggle()
    {
        pauseMenu.SetActive(!pauseMenu.activeSelf);
        if (pauseMenu.activeSelf)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
    }
    public Tower TowerToBuild()
    {
        return towerToBuild;
    }
}
