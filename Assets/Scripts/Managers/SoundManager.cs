using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioSource recover;
    public AudioSource attack;
    public AudioSource hurt;
    public AudioSource getCoin;
    public AudioSource airJump;
    public AudioSource run;
    public AudioSource jump;
    public AudioSource wallJump;
    public AudioSource death;

    public void PlayAttack()
    {
        attack.Play();
    }

    public void PlayJump()
    {
        jump.Play();
    }

    public void PlayWallJump()
    {
        wallJump.Play();
    }

    public void PlayDeath()
    {
        death.Play();
    }

    public void StartRun()
    {
        if (!run.isPlaying)
        {
            run.Play();
        }
    }

    public void StopRun()
    {
        if (run.isPlaying)
        {
            run.Stop();
        }
    }

    public void PlayRecover()
    {
        recover.Play();
    }

    public void PlayHurt()
    {
        hurt.Play();
    }

    public void PlayGetCoin() 
    {
        if (getCoin.isPlaying)
            getCoin.pitch += 0.1f;
        else
            getCoin.pitch = 1f;
        getCoin.PlayOneShot(getCoin.clip);
    }
    
    public void PlayAirJump() 
    {
        airJump.Play();
    }
}
