using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SfxManager : MonoBehaviour
{
    [SerializeField] AudioSource button, jump, hit, rocks, lives;

    public void jumpAudio()
    {
        jump.Play();
    }

    public void buttoPress()
    {
        button.Play();
    }

    public void hitAudio()
    {
        hit.Play();
    }

    public void rocksAudio()
    {
        rocks.Play();
    }
    public void livesAudio()
    {
        lives.Play();
    }
}
