using System;
using UnityEngine;
using UnityEngine.UI;

public class SliderSync : MonoBehaviour
{
    public enum settingsOptions
    {
        MusicVolume,
        SFXVolume,
    }

    public settingsOptions valueToSyncTo = settingsOptions.MusicVolume;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GetComponent<Slider>().value = valueToSyncTo switch
        {
            settingsOptions.MusicVolume => Settings.Instance.musicVolume,
            settingsOptions.SFXVolume => Settings.Instance.sfxVolume,
            _ => 0,
        };
    }

}
