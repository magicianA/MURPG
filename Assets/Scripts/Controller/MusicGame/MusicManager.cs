using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    private AudioSource audioSource;
    public float delaytime = 400f / 70f;
    public AudioClip[] audioClips;
    void Start()
    {
        audioSource =  GetComponent<AudioSource>();
        Invoke("play",delaytime);
    } 
    void Update()
    {
    }
    public void play(){
        audioSource.Play();
    }
    public void pause(){
        audioSource.Pause();
    }
}
