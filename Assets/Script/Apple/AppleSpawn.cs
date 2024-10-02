using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AppleSpawn : MonoBehaviour
{
    // Start is called before the first frame update
    public SnakeController snake;
    public SnakeController player2;
    public TwoPlayerLogic twoPlyer;
    public AudioControl sound;
    public LogicScript logic;
    void Start()
    {
        Spawn();
        sound = GameObject.FindGameObjectWithTag("AudioControl").GetComponent<AudioControl>();
        snake = GameObject.FindGameObjectWithTag("Snake").GetComponent<SnakeController>();
        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicScript>();
        if (SceneManager.GetActiveScene().name == "2PlayerScreen") 
        {
            twoPlyer = GameObject.FindGameObjectWithTag("Logic").GetComponent<TwoPlayerLogic>();
            player2 = GameObject.FindGameObjectWithTag("Player2").GetComponent<SnakeController>();
        }
    }

    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Snake")
        { 
            if(logic.playerScore <= logic.playerHighestScore) { sound.Eat(); }
            else if(logic.havePassRecord) { sound.Eat(); }
            snake.CreateSegment();
            if (PlayerPrefs.GetInt("Twin") == 1) { snake.ReverseSnake(); }
            Spawn();
            if (SceneManager.GetActiveScene().name == "PlayScreen") { logic.AddScore(100); }
            else if (SceneManager.GetActiveScene().name == "2PlayerScreen") { twoPlyer.AddScoreSnake1(100); }
        }
        if (collision.gameObject.tag == "Player2")
        {
            if (logic.playerScore <= logic.playerHighestScore) { sound.Eat(); }
            else if (logic.havePassRecord) { sound.Eat(); }
            player2.CreateSegment();
            if (PlayerPrefs.GetInt("Twin") == 1) { player2.ReverseSnake(); }
            if (SceneManager.GetActiveScene().name == "2PlayerScreen") { twoPlyer.AddScoreSnake2(100); }
            Spawn();
        }
        if (collision.gameObject.tag == "Segment") { Spawn(); }
        if (collision.gameObject.tag == "Wall") { Spawn(); Debug.Log("Wall"); }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Wall") { Spawn(); Debug.Log("Wall"); }
    }
    public void Spawn()
    {
        float x = 0, y = 0;
        do
        {
            x = Random.Range(-20, 19) + 0.5f;
            y = Random.Range(-20, 19) + 0.5f;
        } while ((x == -2.5f && y == 0.5f) 
              || (x == 2.5f && y == 0.5f)
              || (x == 0.5f && y == 0.5f)
              || (x < -19.5f && x > 20.5f)
              || (y < -19.5F && y > 20.5f));
        transform.position = new Vector2(x, y);
    }
}
