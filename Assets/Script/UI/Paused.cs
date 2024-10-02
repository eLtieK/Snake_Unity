using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Paused : MonoBehaviour
{
    public bool isPaused = false;
    public CountdownTimer countdown;
    public AudioControl sound;
    public GameObject pauseMenu;
    public LogicScript onePlayer;
    public TwoPlayerLogic twoPlayer;
    public bool isCount;
    // Update is called once per frame
    void Start()
    {
        countdown = GameObject.FindGameObjectWithTag("Logic").GetComponent<CountdownTimer>();
        isCount = true;
        sound = GameObject.FindGameObjectWithTag("AudioControl").GetComponent<AudioControl>();
        if (SceneManager.GetActiveScene().name == "PlayScreen")
        {
            onePlayer = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicScript>();
        }
        if(SceneManager.GetActiveScene().name == "2PlayerScreen")
        {
            twoPlayer = GameObject.FindGameObjectWithTag("Logic").GetComponent<TwoPlayerLogic>();
        }   
    }
    bool CheckScreen()
    {
        if (SceneManager.GetActiveScene().name == "PlayScreen" && onePlayer.snakeIsAlive) { return true; }
        else if(SceneManager.GetActiveScene().name == "2PlayerScreen" || SceneManager.GetActiveScene().name == "DuelScreen" && twoPlayer.snake1IsAlive && twoPlayer.snake2IsAlive) { return true; }
        else { return false; }
    }
    void Update()
    {
        if(CheckScreen())
        {
            if (Input.GetKeyDown(KeyCode.Escape) && !isCount)
            {
                if (isPaused) { Resume(); }
                else { Pause(); }
            }
            if (Input.GetKeyDown(KeyCode.Space) && isPaused == true && !isCount) { Resume(); }
        }
    }

    public void Pause()
    {
        pauseMenu.SetActive(true);
        isPaused = true;
        sound.tracks.volume = 0;
    }
    public void Resume()
    {
        StartCoroutine(countdown.CountDownToStart());
        pauseMenu.SetActive(false);
        sound.tracks.volume = 0.25f;
    }
    public void Again()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        pauseMenu.SetActive(false);
        isPaused = false;
        sound.tracks.volume = 0.25f;
    }
    public void Menu()
    {
        pauseMenu.SetActive(false);
        isPaused = false;
        SceneManager.LoadScene("StartScreen");
        sound.tracks.volume = 0.25f;
    }
}
