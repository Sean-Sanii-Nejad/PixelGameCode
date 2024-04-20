using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSystemController : MonoBehaviour
{
    private AudioSource audioMusic;
    private AudioSource audioEffects;
    
    void Start()
    {
        audioMusic = GetComponent<AudioSource>();
        audioEffects = GameObject.Find("Entities/Door").GetComponent<AudioSource>();
        audioMusic.Play();
        audioMusic.loop = true;
    }

    public AudioSource GetAudioEffects()
    {
        return audioEffects;
    }

    public AudioSource GetMusicEffects()
    {
        return audioMusic;
    }

    public void PlayAudio()
    {
        audioEffects.Play();
    }

    public void PlayerMusic()
    {
        audioMusic.Play();
    }
}
