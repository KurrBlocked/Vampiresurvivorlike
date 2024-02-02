using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStateManager : MonoBehaviour
{
    private PlayerController player;
    private bool isPaused;
    public GameObject pauseMenu;
    private CanvasGroup pauseScreen;
    private UpgradeManager upgradeManager;
    public bool isPlayable;

    public static int playerLevel;
    public static int expGained;
    public static int upgradesObtained;
    public static int minutesLasted;
    public static int killCount;
    public PlayerLevelManager level;
    private UpgradeManager upgrade;
    private TimerNumberSpawner timer;

    public bool isDead = false;


    void Start()
    {
        isDead = false;
        isPaused = false;
        player = FindAnyObjectByType<PlayerController>();
        if (isPlayable)
        {
            pauseScreen = pauseMenu.GetComponent<CanvasGroup>();
            level = FindAnyObjectByType<PlayerLevelManager>();
            upgrade = FindAnyObjectByType<UpgradeManager>();
            timer = FindAnyObjectByType<TimerNumberSpawner>();
        }
        upgradeManager = FindAnyObjectByType<UpgradeManager>();
        Time.timeScale = 1;
    }
    public void LoadStart()
    {
        SceneManager.LoadScene("StartMenu");
    }
    public void LoadTutorial()
    {
        SceneManager.LoadScene("Tutorial");
    }
    public void LoadInstruction()
    {
        SceneManager.LoadScene("Instruction");
    }
    public void LoadGame()
    {
        SceneManager.LoadScene("BossMonster");
    }
    public void QuitGame()
    {
        Application.Quit();
    }
    // Update is called once per frame
    void Update()
    {
        if (pauseMenu != null)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                isPaused = !isPaused;
                ManagePause();
            }
        }
        if (isPlayable)
        {
            //PlayerDeath
            if (isDead)//Add condition here
            {
                playerLevel = level.level;
                expGained = level.experience;
                upgradesObtained = upgrade.upgradesObtained;
                minutesLasted = timer.minutes;
                SceneManager.LoadScene("EndScreen");
            }
        }
    }

    void ManagePause()
    {
        if (isPaused)
        {
            Time.timeScale = 0;
            Cursor.visible = true;
            pauseScreen.alpha = 1;
            pauseScreen.interactable = true;
            pauseScreen.blocksRaycasts = true;

        }
        else
        {
            
            pauseScreen.alpha = 0;
            pauseScreen.interactable = false;
            pauseScreen.blocksRaycasts = false;
            if (!upgradeManager.isUpgrading)
            {
                Time.timeScale = 1;
                Cursor.visible = false;
            }
        }
        
    }
}
