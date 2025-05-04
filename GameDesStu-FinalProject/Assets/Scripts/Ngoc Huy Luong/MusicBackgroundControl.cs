using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicBackgroundControl : MonoBehaviour
{
    public static MusicBackgroundControl Instance;
    public AudioClip normalMusic;
    public AudioClip bossMusic;

    public AudioSource AudioSource;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }
    public void PlayNormalMusic()
    {
        AudioSource.clip = normalMusic;
        AudioSource.Play();
    }

    public void PlayBossMusic()
    {
        AudioSource.clip = bossMusic;
        AudioSource.Play();
    }

}
