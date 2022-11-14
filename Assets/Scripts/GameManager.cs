using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public enum GameState
{
    GS_PAUSEMENU,
    GS_GAME,
    GS_LEVELCOMPLETED,
    GS_GAMEOVER
}
public class GameManager : MonoBehaviour
{
    public GameState currentGameState = GameState.GS_GAME;
    public static GameManager instance;
    //public Canvas menuCanvas;
    public Canvas inGameCanvas;
    public Canvas pauseMenuCanvas;
    public Canvas leveCompletedCanvas;
    public Canvas gameOverCanvas;
    public Image[] keysTab;
    public Image[] hitPointsTab;
    //public GameObject PauseImage;
    public Text helText;
    public Text enemiesDefeatedText;
    private int score = 0;
    public Text totalScore;
    public Text highscoreText;
    //public Text hitPointsText;
    private int hel = 0;
    private int keys = 0;
    private int hitPoints = 3;
    private int enemiesDefeated = 0;
    private float timer = 0;

    public int maxKeyNumber = 3;
    public bool keysCompleted = false;

    public Text timerText;

    private int maxSecsToHighscore = 120;

    void SetGameState(GameState newGameState)
    {
        if (newGameState == GameState.GS_LEVELCOMPLETED)
        {
            Scene currentScene = SceneManager.GetActiveScene();
            if(currentScene.name == "Level1")
            {
                Debug.Log("oldBest: " + PlayerPrefs.GetInt("HighscoreLevel1"));
                Debug.Log("newWynik: " + score);
                if (score > PlayerPrefs.GetInt("HighscoreLevel1"))
                {
                    Debug.Log("Podmieniam!");
                    PlayerPrefs.SetInt("HighscoreLevel1", score);
                }
                highscoreText.text = PlayerPrefs.GetInt("HighscoreLevel1").ToString();
            }
        }
        currentGameState = newGameState;
        inGameCanvas.enabled = (currentGameState == GameState.GS_GAME);
        pauseMenuCanvas.enabled = (currentGameState == GameState.GS_PAUSEMENU);
        leveCompletedCanvas.enabled = (currentGameState == GameState.GS_LEVELCOMPLETED);
        gameOverCanvas.enabled = (currentGameState == GameState.GS_GAMEOVER);
    }
    public void InGame()
    {
        SetGameState(GameState.GS_GAME);
        //PauseImage.gameObject.SetActive(false);
    }
    public void GameOver()
    {
        SetGameState(GameState.GS_GAMEOVER);
    }
    public void PauseMenu()
    {
        //PauseImage.gameObject.SetActive(true);
        SetGameState(GameState.GS_PAUSEMENU);
    }

    public void LevelCompleted()
    {
        AddScore(-((int)timer) / 20);
        AddScore(hitPoints);
        Debug.Log("Wynik koncowy: " + score);
        SetGameState(GameState.GS_LEVELCOMPLETED);
    }

    private void Awake()
    {
        if (!PlayerPrefs.HasKey("HighscoreLevel1"))
        {
            PlayerPrefs.SetInt("HighscoreLevel1", 0);
        }
        instance = this;
        InGame();
        helText.text = hel.ToString();
        enemiesDefeatedText.text = enemiesDefeated.ToString();
        totalScore.text = score.ToString();
        for(int i = 0; i < keysTab.Length; i++)
        {
            keysTab[i].color = Color.grey;
        }
        for (int i = 3; i < hitPointsTab.Length; i++)
        {
            hitPointsTab[i].gameObject.SetActive(false);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        //PauseMenu();
        InGame();
    }

    // Update is called once per frame
    void Update()
    {
 /*       if(Input.GetKeyDown(KeyCode.S) && currentGameState == GameState.GS_PAUSEMENU)
        {
            InGame();
        }*/

        if(Input.GetKeyDown(KeyCode.Escape) && currentGameState == GameState.GS_PAUSEMENU)
        {
            InGame();
        }
        else if(Input.GetKeyDown(KeyCode.Escape) && currentGameState == GameState.GS_GAME)
        {
            PauseMenu();
        }
        if(currentGameState == GameState.GS_GAME)
        timer += Time.deltaTime;
        //timerText.text = timer.ToString();//string.Format("{0:00}:{1:00}", minutes, seconds)
        int minutes = (int)(timer / 60f);
        int seconds = ((int)timer) % 60;
        //Debug.Log((timer / 60000 * 1000*60));
        //int milliseconds = (int)timer - minutes * 60000 - 1000 * seconds;
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        if(SceneManager.GetActiveScene().name == "Level2")
        if (timer > LevelGenerator.instance.maxGameTime && !LevelGenerator.instance.shouldFinish)
            LevelGenerator.instance.Finish();
        //Debug.Log(seconds.ToString());
    }


    public void OnResumeButtonClicked()
    {
        InGame();
    }

    public void OnRestartButtonClicked()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void OnExitButtonClicked()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void OnExitToDesktopButtonClicked()
    {
    #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; 
    #endif
        Application.Quit();
    }

    public void OnNextLevelButtonClicked()
    {
        SceneManager.LoadScene("Level2");
    }


    public void AddKey(int keyNumber)
    {
        for (int i = 0; i < keyNumber; i++)
        {
            keysTab[keys].color = Color.white;
            keys += 1;
        }
        if (keys == maxKeyNumber) keysCompleted = true;
    }

    public void AddEnemyDefeated(int enemyNumber)
    {
        enemiesDefeated += enemyNumber;
        enemiesDefeatedText.text = enemiesDefeated.ToString();
        AddScore(enemyNumber);
    }

    public void addHitPoints(int hitPointsNumber)
    {
        if (hitPointsNumber == 1 && hitPoints < 5)
        {
            //hitPoints += hitPointsNumber;
            //hitPointsText.text = hitPoints.ToString();
            for (int i = 0; i < hitPointsNumber; i++)
            {
                hitPointsTab[hitPoints].gameObject.SetActive(true);
                hitPoints += 1;
            }
        }
        else
        {
            hitPointsTab[hitPoints - 1].gameObject.SetActive(false);
            hitPoints += hitPointsNumber;
        }
    }
    public void AddHel(int helNumber)
    {
        hel += helNumber;
        helText.text = hel.ToString();
        AddScore(helNumber);
    }

    public void AddScore(int additionalScore)
    {
        score += additionalScore;
        totalScore.text = score.ToString();
    }
}
