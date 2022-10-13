using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
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
    public Canvas menuCanvas;
    public Canvas inGameCanvas;
    public Image[] keysTab;
    public Image[] hitPointsTab;
    public Text helText;
    public Text enemiesDefeatedText;
    public Text hitPointsText;
    private int hel = 0;
    private int keys = 0;
    private int hitPoints = 3;
    private int enemiesDefeated = 0;
    private float timer = 0;
    public Text timerText;
    void SetGameState(GameState newGameState)
    {
        currentGameState = newGameState;
        if(newGameState == GameState.GS_GAME)
        {
            inGameCanvas.enabled = true;
        }
        else if (newGameState == GameState.GS_PAUSEMENU)
        {
            inGameCanvas.enabled = false;
        }
    }
    public void InGame()
    {
        SetGameState(GameState.GS_GAME);
    }
    public void GameOver()
    {
        SetGameState(GameState.GS_GAMEOVER);
    }
    public void PauseMenu()
    {
        SetGameState(GameState.GS_PAUSEMENU);
    }

    public void LevelCOmpleted()
    {
        SetGameState(GameState.GS_LEVELCOMPLETED);
    }

    private void Awake()
    {
        instance = this;
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
        if(Input.GetKeyDown(KeyCode.S) && currentGameState == GameState.GS_PAUSEMENU)
        {
            InGame();
        }
        timer += Time.deltaTime;
        //timerText.text = timer.ToString();//string.Format("{0:00}:{1:00}", minutes, seconds)
        int minutes = (int)(timer / 60000*1000);
        int seconds = (int)(timer / (1000 - 60 * minutes)*1000);
        //Debug.Log((timer / 60000 * 1000*60));
        int milliseconds = (int)timer - minutes * 60000 - 1000 * seconds;
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        //Debug.Log(seconds.ToString());
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
            //Debug.Log("dupa");
            //hitPoints += hitPointsNumber;
            //hitPointsText.text = hitPoints.ToString();
            //Debug.Log("dupa2");
            for (int i = 0; i < hitPointsNumber; i++)
            {
                // Debug.Log("dupaLoop");
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
