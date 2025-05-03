using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VictorySound : MonoBehaviour
{
    public static VictorySound Instance;
    public AudioClip endGameLevel;

    public AudioSource AudioSource;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    public void PlayEndGameMusic()
    { 
        AudioSource.clip = endGameLevel;
        AudioSource.Play();
    }
}
