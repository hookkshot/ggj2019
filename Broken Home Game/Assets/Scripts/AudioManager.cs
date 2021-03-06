﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Hellmade.Sound;

public class AudioManager : MonoBehaviour
{
    #region Singleton

    private static AudioManager _instance;

    public static AudioManager Instance
    {
        get {
            return _instance;
        }
    }

    #endregion

    private void Awake()
    {
        if(_instance == null)
            _instance = this;
    }

    [SerializeField]
    private AudioClip music;

    [SerializeField]
    private AudioClip[] menuSounds;

    [SerializeField]
    private AudioClip[] goodSounds;

    [SerializeField]
    private AudioClip[] badSounds;

    public void PlayMusic()
    {
        EazySoundManager.PlayMusic(music, 0.5f, true, true);
    }

    public void PlayGoodSound()
    {
        var sound = goodSounds[Random.Range(0, goodSounds.Length)];
        EazySoundManager.PlaySound(sound, 2.5f);
    }

    public void PlayBadSound()
    {
        var sound = badSounds[Random.Range(0, badSounds.Length)];
        EazySoundManager.PlaySound(sound, 2.5f);
    }

    public void PlayMenuSound()
    {
        var sound = menuSounds[Random.Range(0, menuSounds.Length)];

        EazySoundManager.PlayUISound(sound);
    }
}
