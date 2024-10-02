using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MoveScreen : MonoBehaviour
{
    public GameObject twoPlayerMode;
    public GameObject onePlayerMode;
    public TwoPlayerLogic logic;
    void Start() 
    {
        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<TwoPlayerLogic>();
    }
    public void Twin(){ PlayerPrefs.SetInt("Twin", 1); }
    public void NotTwin() { PlayerPrefs.SetInt("Twin", 0); }
    public void TwoPlayerReset()
    {
        if(SceneManager.GetActiveScene().name == "2PlayerScreen" || SceneManager.GetActiveScene().name == "DuelScreen")
        {
            Debug.Log("Reset");
            logic.Reset();
        }
    }
    public void PlayScreen() { PlayerPrefs.SetInt("GameMode", 1); SceneManager.LoadScene("Level"); }
    public void TwoPlayerScreen() { PlayerPrefs.SetInt("GameMode", 2); SceneManager.LoadScene("Level"); }
    public void DuelScreen() { PlayerPrefs.SetInt("GameMode", 3); SceneManager.LoadScene("Level"); }
    public void LoadScreen()
    {
        if (PlayerPrefs.GetInt("GameMode") == 1) { SceneManager.LoadScene("PlayScreen"); }
        else if (PlayerPrefs.GetInt("GameMode") == 2) { SceneManager.LoadScene("2PlayerScreen"); }
        else { SceneManager.LoadScene("DuelScreen"); }
    }
    public void TwoPlayerModeAppear()
    {
        twoPlayerMode.SetActive(true);
    }
    public void TwoPlayerModeDisappear()
    {
        twoPlayerMode.SetActive(false);
    }
    public void OnePlayerModeAppear()
    {
        onePlayerMode.SetActive(true);
    }
    public void OnePlayerModeDisappear()
    {
        onePlayerMode.SetActive(false);
    }
    public void SelectedLevel1()
    {
        PlayerPrefs.SetInt("Level", 1);
        LoadScreen();
    }
    public void SelectedLevel2()
    {
        PlayerPrefs.SetInt("Level", 2);
        LoadScreen();
    }
    public void SelectedLevel3()
    {
        PlayerPrefs.SetInt("Level", 3);
        LoadScreen();
    }
    public void ChangeLevel()
    {
        SceneManager.LoadScene("Level");
    }
    public void Endless()
    {
        PlayerPrefs.SetInt("Level", 0);
        LoadScreen();
    }
    public void MainMenu() { SceneManager.LoadScene("StartScreen"); TwoPlayerReset(); }
    public void ExitGame() { Application.Quit(); TwoPlayerReset(); }
    public void Agian() { SceneManager.LoadScene(SceneManager.GetActiveScene().name); }
}
