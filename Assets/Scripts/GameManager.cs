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

    [Header("Ally")]
    public GameObject[] allies;


    [Header("Enemy")]
    public GameObject[] enemies;
    public bool enemyIsAllDead;

    [Header("UI Panel")]
    public bool isLost;
    public GameObject victoryPanel;
    public GameObject pausePanel;
    public GameObject lostPanel;

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
        isLost = false;
        victoryPanel.SetActive(false);
        pausePanel.SetActive(false);
    }

    private void Update()
    {
        currentLevel = PlayerPrefs.GetInt("Level");
        CheckEnemyLife();
        CheckAlliesAndPlayerIsAlive();
        CheckLost();
    }
    public void BackToMainMenu()
    {
        isLost = false;
        lostPanel.SetActive(false);
        SceneManager.LoadScene(0);
    }

    public void CheckAlliesAndPlayerIsAlive()
    {
        allies = GameObject.FindGameObjectsWithTag("Ally");

        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            return;
        }

        if (allies.Length == 0)
        {
            if (Player.PlayerInstance.playerStats.health <= 0)
            {
                isLost = true;
            }
        }
        else
        {
            if (Player.PlayerInstance.playerStats.health <= 0
                && Ally.allyInstance.allyStats.health <= 0)
            {
                isLost = true;
            }
        }
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
        Time.timeScale = 1f;
        SceneManager.LoadScene(1);
    }

    public void CheckLost()
    {
        if (isLost)
        {
            lostPanel.SetActive(true);
        }
        else
        {
            lostPanel.SetActive(false);
        }
    }

    public void RestartGame()
    {
        lostPanel.SetActive(false);
        Time.timeScale = 1;
        SceneManager.LoadScene(1);
    }

    public void SetLevel(int level)
    {
        PlayerPrefs.SetInt("Level", level);
        PlayerPrefs.Save();
    }

    public void NextLevel()
    {
        currentLevel += 1;
        PlayerPrefs.SetInt("Level", currentLevel);
        PlayerPrefs.Save();
        SceneManager.LoadScene(1);
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
