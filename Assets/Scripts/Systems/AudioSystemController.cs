using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSystemController : MonoBehaviour
{
    private AudioSource audioMusic;
    private AudioSource audioEffects;
    private CollisionController collisionController;
    
    void Start()
    {
        collisionController = GameObject.Find("Entities/Player").GetComponent<CollisionController>();
        audioEffects = GameObject.Find("Entities/Door").GetComponent<AudioSource>();
        audioMusic = GetComponent<AudioSource>();  
        audioMusic.Play();
        audioMusic.loop = true;
    }

    public void SetAudioEffect(AudioSource audioSource)
    {
        audioEffects = audioSource;
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

    public void StopAudio()
    {
        audioEffects.Stop();
    }

    public void PlayerMusic()
    {
        audioMusic.Play();
    }
}
