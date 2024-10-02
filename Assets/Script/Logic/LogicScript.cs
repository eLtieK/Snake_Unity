using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LogicScript : MonoBehaviour
{
    public bool snakeIsAlive = true;
    public int playerScore = 0;
    public Text scoreText;
    public Text highestText;
    public int playerHighestScore = 0;
    public GameObject gameOverScreen;
    public AudioControl sound;
    public bool havePassRecord = false;
    public GameObject Level1;
    public GameObject Level2;
    public GameObject Level3;
    // Start is called before the first frame update
    public bool NotPlay()
    {
        if (SceneManager.GetActiveScene().name == "StartScreen" || SceneManager.GetActiveScene().name == "Level") { return true; }
        else { return false; }
    }
    public void CheckLevelSelected()
    {
        if(PlayerPrefs.GetInt("Level") == 1) 
        {
            Level1.SetActive(true);
            Level2.SetActive(false);
            Level3.SetActive(false);
        }
        else if (PlayerPrefs.GetInt("Level") == 2)
        {
            Level2.SetActive(true);
            Level1.SetActive(false);
            Level3.SetActive(false);
        }
        else if (PlayerPrefs.GetInt("Level") == 3)
        {
            Level3.SetActive(true);
            Level1.SetActive(false);
            Level2.SetActive(false);
        }
        else if(PlayerPrefs.GetInt("Level") == 0) 
        { 
            Level1.SetActive(false);
            Level2.SetActive(false);
            Level3.SetActive(false);
        }
    }
    public int LoadLevelHighestScore()
    {
        switch (PlayerPrefs.GetInt("Level")) {
            case 0:
                return PlayerPrefs.GetInt("Level0Score");
            case 1:
                return PlayerPrefs.GetInt("Level1Score");
            case 2:
                return PlayerPrefs.GetInt("Level2Score");
            case 3:
                return PlayerPrefs.GetInt("Level3Score");
        }
        return 0;
    }
    public void SetLevelHighestScore(int score)
    {
        switch (PlayerPrefs.GetInt("Level"))
        {
            case 0:
                PlayerPrefs.SetInt("Level0Score", score);
                break;
            case 1:
                PlayerPrefs.SetInt("Level1Score" ,score);
                break;
            case 2:
                PlayerPrefs.SetInt("Level2Score", score);
                break;
            case 3:
                PlayerPrefs.SetInt("Level3Score", score);
                break;
        }
    }
    void Start()
    {
        havePassRecord = false;
        snakeIsAlive = true;
        if (!NotPlay()) { CheckLevelSelected(); }
        if (SceneManager.GetActiveScene().name == "PlayScreen")
        {
            highestText.text = LoadLevelHighestScore().ToString();
            playerHighestScore = LoadLevelHighestScore();
            sound = GameObject.FindGameObjectWithTag("AudioControl").GetComponent<AudioControl>();
        }
    }
    public void Die() { snakeIsAlive = false; }
    public void Over() 
    {
        if(!snakeIsAlive) { gameOverScreen.SetActive(true); } 
    }
    public void AddScore(int scoreToAdd) 
    {
        playerScore += scoreToAdd;
        scoreText.text = playerScore.ToString();
        UpdateHighestScore(playerScore);
    }
    public void UpdateHighestScore(int score)
    {
        if (score > playerHighestScore)
        {
            playerHighestScore = score;
            if (SceneManager.GetActiveScene().name == "PlayScreen") { SetLevelHighestScore(score); }
            if (!havePassRecord) { sound.NewRecord(); havePassRecord = true; }
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (SceneManager.GetActiveScene().name == "PlayScreen")
        {
            highestText.text = LoadLevelHighestScore().ToString();
            if (Input.GetKey(KeyCode.Space) && gameOverScreen.activeSelf) { SceneManager.LoadScene(SceneManager.GetActiveScene().name); }
        }
        Over();
    }
}