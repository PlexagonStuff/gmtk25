using UnityEngine;
using UnityEngine.Events;

public class Settings : MonoBehaviour
{
    public static Settings Instance { get; private set; }

    public UnityEvent OnMusicVolumeChanged;
    private float _musicVolume = 0.5f;
    public float MusicVolume
    {
        get
        {
            return _musicVolume;
        }
        set
        {
            if (_musicVolume != value)
            {
                _musicVolume = value;
                OnMusicVolumeChanged.Invoke();
            }
        }
    }
    public UnityEvent OnSFXVolumeChanged;
    private float _sfxVolume = 0.5f;
    public float SFXVolume
    {
        get
        {
            return _sfxVolume;
        }
        set
        {
            if (_sfxVolume != value)
            {
                _sfxVolume = value;
                OnSFXVolumeChanged.Invoke();
            }
        }
    }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
}
