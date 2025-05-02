using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    // Singleton set up
    static SoundManager _instance;
    public static SoundManager Instance { get { return _instance; } }

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this);
        }
        else
        {
            _instance = this;
        }
        DontDestroyOnLoad(gameObject);
    }

    // Audio mixer
    [SerializeField] AudioMixer audioMixer;
    // audio source
    [SerializeField] AudioSource bgMusicMain;
    [SerializeField] AudioSource bgMusicW3;
    [SerializeField] AudioSource vfx;
    [SerializeField] AudioSource vfxLoop;
    [SerializeField] AudioSource vfxBtn;

    // for button click
    [SerializeField] AudioClip btnSound;
    [SerializeField] AudioClip enterLevelSound;

    //// volumn slider
    //[SerializeField] Slider bgMusicSlider;
    //[SerializeField] Slider vfxSlider;
    //[SerializeField] Slider masterSlider;


    //// Script to adjust volumn using slider. Attach to slider
    //// Link this to slider controlling the background sound. 
    //public void AdjustMusicVolumn()
    //{
    //    float volumn = Mathf.Log10(bgMusicSlider.value) * 20;
    //    audioMixer.SetFloat("Music", volumn);
    //}

    //// Link this to slider controlling the VFX sound. 
    //public void AdjustVfxVolumn()
    //{
    //    float volumn = Mathf.Log10(vfxSlider.value) * 20;
    //    audioMixer.SetFloat("VFX", volumn);
    //}

    //// Link this to slider controlling the master sound. 
    //public void AdjustMasterVolumn()
    //{
    //    float volumn = Mathf.Log10(masterSlider.value) * 20;
    //    audioMixer.SetFloat("MasterSound", volumn);
    //}

    // Script to play background music for the main menu
    public void playBackgroundMain()
    {
        if (bgMusicW3.isPlaying)
        {
            bgMusicW3.Stop();
        }
        bgMusicMain.loop = true;
        //AdjustMusicVolumn();
        bgMusicMain.Play();
    }

    // Script to play background music for 3rd world
    public void playBackgroundW3()
    {
        if (bgMusicMain.isPlaying)
        {
            bgMusicMain.Stop();
        }
        bgMusicW3.loop = true;
        //AdjustMusicVolumn();
        bgMusicW3.Play();
    }

    // Script to stop all background music
    public void stopBackgroundMusic()
    {
        if (bgMusicMain.isPlaying)
        {
            bgMusicMain.Stop();
        }
        if (bgMusicW3.isPlaying)
        {
            bgMusicW3.Stop();
        }
    }

    // This is to play VFX. call this function whenever a new VFX is played. 
    // This create a new object, and destroy it after the sound is done. This is so multiple VFX can be played at the same time.
    public void playVFX(AudioClip audioClip, Transform spamTrans)
    {
        //AdjustVfxVolumn();
        vfx.PlayOneShot(audioClip);
    }
    public void playVFXLoop(AudioClip audioClip, Transform spamTrans)
    {
        //AdjustVfxVolumn();
        vfxLoop.clip = audioClip;
        vfxLoop.Play();
    }
    public void StopVFXLoop()
    {
        vfxLoop.Stop();
    }

    public void PlayBtnSound(Transform trans) { 
        vfxBtn.PlayOneShot(btnSound);
    }
    public void PlayEnterLevel(Transform trans) { 
        vfxBtn.PlayOneShot(enterLevelSound);
    }

    // when load level play different sound
    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoad;
    }

    private void OnSceneLoad(Scene scene, LoadSceneMode mode)
    {
        if (scene.buildIndex == 0 || scene.buildIndex == 1)
        {
            if (!bgMusicMain.isPlaying)
            {
                playBackgroundMain();
            }
        }
        else if (scene.buildIndex == 5)
        {
            if (!bgMusicW3.isPlaying)
            {
                playBackgroundW3();
            }
        }
        else
        {
            stopBackgroundMusic();
        }
        Debug.Log(mode);
    }

}
