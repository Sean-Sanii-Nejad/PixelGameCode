using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    private AudioSource audioMusic;
    [SerializeField]
    private AudioSource audioEffects;

    // System Controllers
    private CollisionController collisionController;
    
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
        // Audio
        //audioEffects = GameObject.Find("Entities/Door").GetComponent<AudioSource>();
        audioMusic = GetComponent<AudioSource>();
        
        audioMusic.loop = true;
        audioMusic.Play();
        
        // System Controllers
        collisionController = GameObject.Find("Player").GetComponent<CollisionController>();
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
