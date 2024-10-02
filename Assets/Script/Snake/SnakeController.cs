using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SnakeController : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject FloatingPrefab;
    public AudioControl sound;
    public Vector2 direction;
    public bool left = false;
    public bool right = true;
    public bool up = true;
    public bool down = true;
    public GameObject segmentPrefab;
    private List<GameObject> segments = new List<GameObject>();
    private bool startGame = false;
    public float timer;
    public int autoDirectionTime;
    public int autoDirection;
    public LogicScript logic;
    public TwoPlayerLogic twoPlayer;
    public Paused pausedGame;
    public bool outStartPosition;
    public bool isReverse;
    void Start()
    {
        isReverse = false;
        outStartPosition = false;
        startGame = false;
        timer = 0;
        autoDirectionTime = 2;  
        autoDirection = 3;
        if (!NotPlay())
        {
            pausedGame = GameObject.FindGameObjectWithTag("Canvas").GetComponent<Paused>();
            sound = GameObject.FindGameObjectWithTag("AudioControl").GetComponent<AudioControl>();
        }
        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicScript>();
        if (SceneManager.GetActiveScene().name == "2PlayerScreen") 
        {
            twoPlayer = GameObject.FindGameObjectWithTag("Logic").GetComponent<TwoPlayerLogic>(); 
        }
        Reset();
        ResetSegment();
    }
    public bool TwoPlayerMode()
    {
        if (SceneManager.GetActiveScene().name == "2PlayerScreen" || SceneManager.GetActiveScene().name == "DuelScreen") { return true; }
        else { return false; }
    }
    public bool StartPosition()
    {
        if ((transform.position.x == 2.5f || transform.position.x == -2.5f) && transform.position.y == 0.5f && !outStartPosition)
        {
            return true;
        }
        else { outStartPosition = true; return false; }
    }
    public bool NotPlay() 
    {
        if (SceneManager.GetActiveScene().name == "StartScreen" || SceneManager.GetActiveScene().name == "Level") { return true; }
        else { return false; }
    }
    public void AutoMove()
    {
        if (timer < autoDirectionTime) { timer += Time.deltaTime * 10; }
        else
        {
            timer = 0;
            autoDirectionTime = Random.Range(1, 3);
            int randomNum = Random.Range(0, 3);
            while (autoDirection == randomNum) { randomNum = Random.Range(0, 3); }
            autoDirection = randomNum;
        }
    }
    void ShowFloatingText()
    {
        Instantiate(FloatingPrefab, transform.position, Quaternion.identity, transform);
    }
    // Update is called once per frame
    void Update()
    {
        if (NotPlay())
        {
            AutoMove();
            AutoController();
        }
        else
        {
            if (gameObject.tag == "Snake") { Controller(); }
            if (gameObject.tag == "Player2") { Controller2(); }
        }
    }
    void FixedUpdate()
    {
        if (SceneManager.GetActiveScene().name == "PlayScreen")
        {
            if (logic.snakeIsAlive && !pausedGame.isPaused)
            {
                SnakeMove();
                MoveSegment();
            }
        }
        else if (NotPlay())
        {
            SnakeMove();
            MoveSegment();
        }
        else if (TwoPlayerMode())
        {
            if(twoPlayer.snake1IsAlive && twoPlayer.snake2IsAlive && !pausedGame.isPaused)
            {
                SnakeMove();
                MoveSegment();
                if (SceneManager.GetActiveScene().name == "DuelScreen")
                {
                    if ((transform.position.x % 1 == 0.5f || transform.position.y % 1 == 0.5f)
                        || (transform.position.x % 1 == -0.5f || transform.position.y % 1 == -0.5f)
                        && !pausedGame.isPaused) 
                    {  
                        CreateSegment(); 
                    }
                }
            }
        }
    }
    public void CreateSegment()
    {
        GameObject newSegment = Instantiate(segmentPrefab);
        newSegment.transform.position = segments[segments.Count - 1].transform.position;
        segments.Add(newSegment);
    }
    public void CreateWhenStart()
    {
        if (NotPlay()) 
        {
            for (int i = 0; i < 20; i++) { CreateSegment(); }
        }
        else { CreateSegment(); }
    }
    public void ResetSegment()
    {
        for (int i = 1; i < segments.Count; i++) { Destroy(segments[i]); }
        segments.Clear();
        segments.Add(gameObject);
        CreateWhenStart();
    }
    public void Reset()
    {
        if (gameObject.tag == "Snake") { direction = Vector2.left; }
        else if (gameObject.tag == "Player2") { direction = Vector2.right; }
        Time.timeScale = 0.1f;
        left = false;
        right = true;
        up = true;
        down = true;
    }
    public void MoveSegment()
    {
        if (!isReverse)
        {
            for (int i = segments.Count - 1; i > 0; i--) { segments[i].transform.position = segments[i - 1].transform.position; }
        }
    }
    public void ReverseSnake()
    {
        Vector2[] box = new Vector2[segments.Count];
        for (int i = 0; i < segments.Count; i++)
        {
            box[i] = segments[i].transform.position;
        }
        for(int i = 0; i < segments.Count/2; i++)
        {
            segments[i].transform.position = box[segments.Count - 1 - i];
            segments[segments.Count - 1 - i].transform.position = box[i];
        }
        if (segments[1].transform.position.x - segments[2].transform.position.x < 0)
        {
            direction = Vector2.left;
            left = true;
            right = false;
            up = true;
            down = true;
            Debug.Log("Left");
        }
        else if (segments[1].transform.position.x - segments[2].transform.position.x > 0)
        {
            direction = Vector2.right;
            left = false;
            right = true;
            up = true;
            down = true;
            Debug.Log("Right");
        }
        else if (segments[1].transform.position.y - segments[2].transform.position.y < 0)
        {
            direction = Vector2.down;
            left = true;
            right = true;
            up = false;
            down = true;
            Debug.Log("Down");
        }
        else if (segments[1].transform.position.y  - segments[2].transform.position.y > 0)
        {
            direction = Vector2.up;
            left = true;
            right = true;
            up = true;
            down = false;
            Debug.Log("Up");
        }
    }
    public void AutoController()
    {
        if (autoDirection == 0 && up)
        {
            direction = Vector2.up;
            left = true;
            right = true;
            up = true;
            down = false;
        }
        else if (autoDirection == 1 && down)
        {
            direction = Vector2.down;
            left = true;
            right = true;
            up = false;
            down = true;
        }
        else if (autoDirection == 2 && left)
        {
            direction = Vector2.left;
            left = true;
            right = false;
            up = true;
            down = true;
        }
        else if ( autoDirection == 3 && right )
        {
            direction = Vector2.right;
            left = false;
            right = true;
            up = true;
            down = true;
        }
    }
    public void Controller()
    {
        if ((Input.GetKey(KeyCode.W) || (Input.GetKey(KeyCode.UpArrow) && SceneManager.GetActiveScene().name == "PlayScreen")) && up)
        {
            direction = Vector2.up;
            left = true;
            right = true;
            up = true;
            down = false;
        }
        else if ((Input.GetKey(KeyCode.S) || (Input.GetKey(KeyCode.DownArrow) && SceneManager.GetActiveScene().name == "PlayScreen")) && down)
        {
            direction = Vector2.down;
            left = true;
            right = true;
            up = false;
            down = true;
        }
        else if ((Input.GetKey(KeyCode.A) || (Input.GetKey(KeyCode.LeftArrow) && SceneManager.GetActiveScene().name == "PlayScreen")) && left)
        {
            direction = Vector2.left;
            left = true;
            right = false;
            up = true;
            down = true;
        }
        else if ((Input.GetKey(KeyCode.D) || (Input.GetKey(KeyCode.RightArrow) && SceneManager.GetActiveScene().name == "PlayScreen")) && right)
        {
            direction = Vector2.right;
            left = false;
            right = true;
            up = true;
            down = true;
        }
    }
    public void Controller2()
    {
        if (Input.GetKey(KeyCode.UpArrow) && up)
        {
            direction = Vector2.up;
            left = true;
            right = true;
            up = true;
            down = false;
        }
        else if (Input.GetKey(KeyCode.DownArrow) && down)
        {
            direction = Vector2.down;
            left = true;
            right = true;
            up = false;
            down = true;
        }
        else if (Input.GetKey(KeyCode.LeftArrow) && left) 
        {
            direction = Vector2.left;
            left = true;
            right = false;
            up = true;
            down = true;
        }
        else if (Input.GetKey(KeyCode.RightArrow) && right) 
        {
            direction = Vector2.right;
            left = false;
            right = true;
            up = true;
            down = true;
        }
    }
    public void SnakeMove()
    {
        float x, y;
        x = transform.position.x + direction.x;
        y = transform.position.y + direction.y;
        transform.position = new Vector2(x, y);
        if (transform.position.x > 19.5f) { transform.position = new Vector2(-19.5f, y); }
        if (transform.position.x < -19.5f) { transform.position = new Vector2(19.5f, y); }
        if (transform.position.y > 19.5f) { transform.position = new Vector2(x, -19.5f); }
        if (transform.position.y < -19.5f) { transform.position = new Vector2(x, 19.5f); }
    }
    private void OnTriggerEnter2D(Collider2D collision) 
    {
        if (SceneManager.GetActiveScene().name == "PlayScreen")
        {
            if (collision.gameObject.tag == "Wall" && startGame) { Debug.Log("Die"); logic.Die(); ShowFloatingText(); sound.Die(); }
            else { startGame = true; }
        }
        else if (TwoPlayerMode())
        {
            if (gameObject.tag == "Snake")
            {
                if (collision.gameObject.tag == "Wall" && startGame) { Debug.Log("Snake1Die"); twoPlayer.Snake1Die(); ShowFloatingText(); sound.Die(); }
                else { startGame = true; }
            }
            else if (gameObject.tag == "Player2")
            {
                if (collision.gameObject.tag == "Wall" && startGame) { Debug.Log("Snake2Die"); twoPlayer.Snake2Die(); ShowFloatingText(); sound.Die(); }
                else { startGame = true; }
            }
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (SceneManager.GetActiveScene().name == "PlayScreen")
        {
            if (collision.gameObject.tag == "Segment" && startGame) { Debug.Log("Die"); logic.Die(); ShowFloatingText(); sound.Die(); }
            else { startGame = true; }
        }
        else if (TwoPlayerMode())
        {
            if (gameObject.tag == "Snake")
            {
                if (collision.gameObject.tag == "Segment" && startGame) { Debug.Log("Snake1Die"); twoPlayer.Snake1Die(); ShowFloatingText(); sound.Die(); }
                else if (!StartPosition()) { startGame = true; }
                else if (SceneManager.GetActiveScene().name == "2PlayerScreen") { startGame = true; }
            }
            else if(gameObject.tag == "Player2")
            {
                if (collision.gameObject.tag == "Segment" && startGame) { Debug.Log("Snake2Die"); twoPlayer.Snake2Die(); ShowFloatingText(); sound.Die(); }
                else if (!StartPosition()) { startGame = true; }
                else if (SceneManager.GetActiveScene().name == "2PlayerScreen") { startGame = true; }
            }
        }
    }
}
