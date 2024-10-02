using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountdownTimer : MonoBehaviour
{
    public int countdownTime;
    public Text countdownText;
    public AudioControl sound;
    public Paused pausedGame;
    private void Start()
    {
        StartCoroutine(CountDownToStart());
        pausedGame = GameObject.FindGameObjectWithTag("Canvas").GetComponent<Paused>();
        pausedGame.isPaused = true;
        sound = GameObject.FindGameObjectWithTag("AudioControl").GetComponent<AudioControl>();
    }

    public IEnumerator CountDownToStart()
    {
        pausedGame.isCount = true;
        countdownTime = 3;
        countdownText.gameObject.SetActive(true);
        while (countdownTime > 0)
        {
            countdownText.text = countdownTime.ToString();
            if(countdownTime <= 2) { sound.Count(); }
            yield return new WaitForSeconds(0.1f);

            countdownTime--;
        }

        countdownText.text = "GO!";
        sound.Go();

        yield return new WaitForSeconds(0.1f);

        countdownText.gameObject.SetActive(false);
        pausedGame.isPaused = false;
        pausedGame.isCount = false;
    }
}
