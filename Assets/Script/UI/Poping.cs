using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Poping : MonoBehaviour
{
    public CanvasGroup myUI;
    private int haveFade = 1;
    public float timer = 0;
    void Update()
    {
        Fade();
    }
    // Update is called once per frame
    public void Fade()
    {
        if (haveFade == 0) 
        {
            if (timer < 0.8f) { timer += Time.timeScale * 0.2f; myUI.alpha -= Time.timeScale * 0.2f; }
            else { timer = 0; haveFade = 1; }
        }
        else
        {
            if(timer < 0.8f) { timer += Time.timeScale * 0.2f; myUI.alpha += Time.timeScale * 0.2f; }
            else { timer = 0; haveFade = 0; }
        }
    }
}
