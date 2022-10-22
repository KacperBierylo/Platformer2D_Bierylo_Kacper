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
    public GameState currentGameState = GameState.GS_PAUSEMENU;
    public static GameManager instance;
    //public Canvas menuCanvas;
    public Canvas inGameCanvas;
    public Canvas pauseMenuCanvas;
    public Image[] keysTab;
    public Image[] hitPointsTab;
    //public GameObject PauseImage;
    public Text helText;
    public Text enemiesDefeatedText;
    //public Text hitPointsText;
    private int hel = 0;
    private int keys = 0;
    private int hitPoints = 3;
    private int enemiesDefeated = 0;
    private float timer = 0;
    public Text timerText;
    void SetGameState(GameState newGameState)
    {
        currentGameState = newGameState;
        inGameCanvas.enabled = (currentGameState == GameState.GS_GAME);
        pauseMenuCanvas.enabled = (currentGameState == GameState.GS_PAUSEMENU);

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

    public void LevelCOmpleted()
    {
        SetGameState(GameState.GS_LEVELCOMPLETED);
    }

    private void Awake()
    {
        instance = this;
        InGame();
        helText.text = hel.ToString();
        enemiesDefeatedText.text = enemiesDefeated.ToString();
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
        PauseMenu();
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

    public void AddKey(int keyNumber)
    {
        for (int i = 0; i < keyNumber; i++)
        {
            keysTab[keys].color = Color.white;
            keys += 1;
        }
    }

    public void AddEnemyDefeated(int enemyNumber)
    {
        enemiesDefeated += enemyNumber;
        enemiesDefeatedText.text = enemiesDefeated.ToString();
    }

    public void addHitPoints(int hitPointsNumber)
    {
        if (hitPointsNumber > 0)
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
    }
}
