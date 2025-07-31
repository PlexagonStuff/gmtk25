using UnityEngine;

public class MusicController : MonoBehaviour
{
    private void Start()
    {
        DontDestroyOnLoad(gameObject);
        Settings.Instance.OnMusicVolumeChanged.AddListener(SetVolume);
    }

    private void SetVolume()
    {
        GetComponent<AudioSource>().volume = Settings.Instance.MusicVolume;
    }
}
