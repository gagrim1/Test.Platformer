using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioSource recover;
    public AudioSource attack;
    public AudioSource attackEnemy;
    public AudioSource run;
    public AudioSource jump;
    public AudioSource wallJump;
    public AudioSource death;

    public void playAttack()
    {
        attack.Play();
    }

    public void playJump()
    {
        jump.Play();
    }

    public void playWallJump()
    {
        wallJump.Play();
    }

    public void playDeath()
    {
        death.Play();
    }

    public void startRun()
    {
        if (!run.isPlaying)
        {
            run.Play();
        }
    }

    public void stopRun()
    {
        if (run.isPlaying)
        {
            //Debug.Log("run");
            run.Stop();
        }
    }

    public void playRecover()
    {
        recover.Play();
    }
    public void playAtackEnemy()
    {
        attack.Stop();
        attackEnemy.Play();
    }
}
