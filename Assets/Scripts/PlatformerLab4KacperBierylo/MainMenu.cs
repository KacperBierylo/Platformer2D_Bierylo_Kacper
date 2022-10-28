using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenu : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
/*        if (Input.GetKeyDown(KeyCode.S))
        {
            OnLevel1ButtonPressed();
        }*/
    }
    private IEnumerator StartGame(string levelName)
    {
        yield return new WaitForSeconds(0.1f);
        SceneManager.LoadScene(levelName);
    }
    public void OnLevel1ButtonPressed()
    {
        StartCoroutine(StartGame("Level1"));
    }

    public void OnLevel2ButtonPressed()
    {
        StartCoroutine(StartGame("Level2"));
    }

    public void OnExitToDekstopButtonPressed()
    {
    #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
    #endif
        Application.Quit();
    }
}