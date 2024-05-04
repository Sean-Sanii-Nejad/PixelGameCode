using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VFXController : MonoBehaviour
{
    private ParticleSystem particleSystem;
    private ParticleSystem particleAttack;

    void Awake()
    {
        particleSystem = GameObject.Find("Player/AuraSlow").GetComponent<ParticleSystem>();
        particleAttack = GameObject.Find("EnemyBattleStation/Attack").GetComponent<ParticleSystem>();
    }

    public void PlayParticleAttack()
    {
        particleAttack.Play();
    }

    public void PlayParticleSystem()
    {
        particleSystem.Play();
    }

    public void StopParticleSystem()
    {
        particleSystem.Stop();
    }
}
