using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioControl : MonoBehaviour
{
    public AudioSource tracks;
    public AudioSource appleEatten;
    public AudioSource newRecord;
    public AudioSource count;
    public AudioSource go;
    public AudioSource die;
    public AudioSource click;

    void Start() { tracks.Play(); tracks.volume = 0.2f; }
    public void Eat() { appleEatten.Play(); appleEatten.volume = 0.5f; }
    public void NewRecord() { newRecord.Play(); }
    public void Count() { count.Play(); }
    public void Go() { go.Play(); }
    public void Die() { die.Play(); }
    public void Click() { click.Play(); }
}
