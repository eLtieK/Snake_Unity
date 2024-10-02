using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TwoPlayerLogic : MonoBehaviour
{
    public bool snake1IsAlive = true;
    public bool snake2IsAlive = true;
    public bool donePlay = false;
    public int player1Score = 0;
    public int player2Score = 0;
    public Text score1Text;
    public Text score2Text;
    public int player1Win = 0;
    public int player2Win = 0;
    public Text win1Text;
    public Text win2Text;
    public int player1Streak = 0;
    public int player2Streak = 0;
    public Text streak1Text;
    public Text streak2Text;
    public GameObject player1Screen;
    public GameObject player2Screen;
    public GameObject drawScreen;
    public AudioControl sound;
    // Start is called before the first frame update
    void Start()
    {
        Load();
        snake1IsAlive = true;
        snake2IsAlive = true;
        donePlay = false;
        sound = GameObject.FindGameObjectWithTag("AudioControl").GetComponent<AudioControl>();
    }
    public void Snake1Die() { snake1IsAlive = false; }
    public void Snake2Die() { snake2IsAlive = false; }
    public void WinOrDraw()
    {
        if(!snake1IsAlive && !snake2IsAlive) 
        {
            if (player1Score > player2Score) 
            {
                player1Screen.SetActive(true);
                AddWinSnake1(1);
                AddStreakSnake1(1);
                PlayerPrefs.SetInt("Streak2", 0);
                streak2Text.text = 0.ToString();
            }
            else if (player1Score < player2Score) 
            { 
                player2Screen.SetActive(true);
                AddWinSnake2(1);
                AddStreakSnake2(1);
                PlayerPrefs.SetInt("Streak1", 0);
                streak1Text.text = 0.ToString();
            }
            else { drawScreen.SetActive(true); }
            donePlay = true;
        }
        else if(!snake1IsAlive) 
        { 
            player2Screen.SetActive(true);
            AddWinSnake2(1);
            AddStreakSnake2(1);
            PlayerPrefs.SetInt("Streak1", 0);
            streak1Text.text = 0.ToString();
            donePlay = true;
        }
        else if (!snake2IsAlive) 
        {
            player1Screen.SetActive(true);
            AddWinSnake1(1);
            AddStreakSnake1(1);
            PlayerPrefs.SetInt("Streak2", 0);
            streak2Text.text = 0.ToString();
            donePlay = true;
        }
    }
    public bool isLose()
    {
        if (drawScreen.activeSelf || player2Screen.activeSelf || player1Screen.activeSelf) { return true; }
        else { return false; }
    }
    public void AddScoreSnake1(int scoreToAdd)
    {
        player1Score += scoreToAdd;
        score1Text.text = player1Score.ToString();
    }
    public void AddWinSnake1(int winToAdd)
    {
        player1Win += winToAdd;
        win1Text.text = player1Win.ToString();
        PlayerPrefs.SetInt("Win1", player1Win);
    }
    public void AddStreakSnake1(int streakToAdd)
    {
        player1Streak += streakToAdd;
        streak1Text.text = player1Streak.ToString();
        PlayerPrefs.SetInt("Streak1", player1Streak);
    }
    public void AddScoreSnake2(int scoreToAdd)
    {
        player2Score += scoreToAdd;
        score2Text.text = player2Score.ToString();
    }
    public void AddWinSnake2(int winToAdd)
    {
        player2Win += winToAdd;
        win2Text.text = player2Win.ToString();
        PlayerPrefs.SetInt("Win2", player2Win);
    }
    public void AddStreakSnake2(int streakToAdd)
    {
        player2Streak += streakToAdd;
        streak2Text.text = player2Streak.ToString();
        PlayerPrefs.SetInt("Streak2", player2Streak);
    }
    // Update is called once per frame
    void Update()
    {
        if (!donePlay) { WinOrDraw(); }
        if (SceneManager.GetActiveScene().name == "2PlayerScreen" || SceneManager.GetActiveScene().name == "DuelScreen")
        {
            if (Input.GetKey(KeyCode.Space) && isLose()) { SceneManager.LoadScene(SceneManager.GetActiveScene().name); }
        }
    }
    public void Load()
    {
        player1Win = PlayerPrefs.GetInt("Win1");
        win1Text.text = player1Win.ToString();
        player2Win = PlayerPrefs.GetInt("Win2");
        win2Text.text = player2Win.ToString();

        player1Streak = PlayerPrefs.GetInt("Streak1");
        streak1Text.text = player1Streak.ToString();
        player2Streak = PlayerPrefs.GetInt("Streak2");
        streak2Text.text = player2Streak.ToString();
    }
    public void Reset()
    {
        PlayerPrefs.SetInt("Win1", 0);
        PlayerPrefs.SetInt("Win2", 0);
        PlayerPrefs.SetInt("Streak1", 0);
        PlayerPrefs.SetInt("Streak2", 0);
    }
}
