using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VFXController : MonoBehaviour
{
    private ParticleSystem particleSystem;

    void Awake()
    {
        particleSystem = GameObject.Find("Player/BrokenHeart(Slow)").GetComponent<ParticleSystem>();
    }

    public void PlayParticleSystem()
    {
        particleSystem.Play();
    }
}
