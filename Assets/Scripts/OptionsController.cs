using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class OptionsController : MonoBehaviour
{
    public GameObject optionsScreen;
    public AudioMixer audioMixer;
    public AudioSource audioSource;

    bool isPaused = false;
    bool musicPaused = false;

    public AudioClip[] audioClips;
    public float TimeTilNextSong = 120;
    float timeTilnextSong;
    int index = 0;

    private void Awake()
    {
        DontDestroyOnLoad(this);
        timeTilnextSong = TimeTilNextSong;
    }

    private void Update()
    {
        if(!musicPaused)
        {
            timeTilnextSong -= Time.deltaTime;
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            OnOptionsScreen();
        }
        if(timeTilnextSong <= -1.5f)
        {
            timeTilnextSong = TimeTilNextSong;
            playNext();
        }
    }

    public void OnOptionsScreen()
    {
        isPaused = isPaused ? false : true;
        optionsScreen.SetActive(isPaused);
    }

    public void OnMasterVolume(float level)
    {
        audioMixer.SetFloat("MasterVolume", level);
    }

    public void OnMusicVolume(float level)
    {
        audioMixer.SetFloat("MusicVolume", level);
    }

    public void OnSFXVolume(float level)
    {
        audioMixer.SetFloat("SFXVolume", level);
    }

    public void playNext()
    {
        index++;
        if (index >= audioClips.Length - 1) index = 0;
        audioSource.clip = audioClips[index];
        audioSource.PlayDelayed(2.5f);
    }

    public void playPrev()
    {
        index--;
        if (index < 0) index =  audioClips.Length -1;
        audioSource.clip = audioClips[index];
        audioSource.PlayDelayed(2.5f);
    }

    public void pauseMusic()
    {
        musicPaused = true;
        audioSource.Pause();
    }

    public void playMusic()
    {
        musicPaused = false;
        audioSource.Play();
    }
}
