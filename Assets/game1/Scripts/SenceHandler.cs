using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class SenceHandler : MonoBehaviour
{
    GameManager gameManager;
    private void Awake()
    {
        
    }
    private void Start()
    {
        gameManager = GameManager.Instance;
    }
    public void LoadMenu()
    {
        
    }
    public void ReLoadLevel()
    {
        int currentScene = SceneManager.GetActiveScene().buildIndex;
        int nextScene = currentScene + 1;
        int totalSceneNumber = SceneManager.sceneCountInBuildSettings;
        if (nextScene == totalSceneNumber)
        {
            SceneManager.LoadScene(0);
        }
        else
        {
            SceneManager.LoadScene(nextScene);
        }
    }
    public void QuitGame()
    {
        gameManager.gameOver = true;
        Application.Quit();
    }
    public void StartGame()
    {
        gameManager.gameOver = false;
        Time.timeScale = 1;
        gameManager.gameFinshCamvas.SetActive(false);
    }
}
