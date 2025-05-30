using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager GMInstance;

    public int numberOfEnemies;
    public int currentLevel;


    [Header("Enemy")]
    public GameObject[] enemies;
    public bool enemyIsAllDead;

    [Header("UI Panel")]
    public GameObject victoryPanel;
    public GameObject pausePanel;

    private void Awake()
    {
        if (GMInstance == null)
        {
            GMInstance = this;
        }
    }

    private void Start()
    {
        enemyIsAllDead = false;
        victoryPanel.SetActive(false);
        pausePanel.SetActive(false);
    }

    private void Update()
    {
        CheckEnemyLife();
    }
    public void BackToMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void SetEnemyAlive()
    {
        enemyIsAllDead = false;
    }

    public void CheckEnemyLife()
    {
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        if (Player.PlayerInstance == null)
        {
            victoryPanel.SetActive(false);
            return;
        }
        else if (enemies.Length == 0)
        {
            Debug.Log("Khong con enemy nao.");
            enemyIsAllDead = true;
            victoryPanel.SetActive(true);
        }
        else
        {
            Debug.Log("Con " + enemies.Length + " enemy");
        }

    }

    public IEnumerator WaitToSpawnPanel()
    {
        yield return new WaitForSeconds(5);
    }

    public void SelectAmountOfEnemies(int countEnemy)
    {
        PlayerPrefs.SetInt("AmountOfEnemies", countEnemy);
        PlayerPrefs.Save();
    }

    public void SelectAmountOfAllies(int countAlly)
    {
        PlayerPrefs.SetInt("AmountOfAllies", countAlly);
        PlayerPrefs.Save();
    }

    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void SetLevel(int level)
    {
        PlayerPrefs.SetInt("Level", level);
        PlayerPrefs.Save();
        currentLevel = level;
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
        pausePanel.SetActive(true);
    }

    public void ContinueGame()
    {
        Time.timeScale = 1f;
        pausePanel.SetActive(false);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
