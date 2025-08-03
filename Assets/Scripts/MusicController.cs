using UnityEngine;
public class MusicController : MonoBehaviour

{
    public AudioClip robotTheme;

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
        Settings.Instance.OnMusicVolumeChanged.AddListener(SetVolume);
        GetComponent<AudioSource>().clip = robotTheme;
        GetComponent<AudioSource>().Play();
        GetComponent<AudioSource>().volume = 0.5f;
    }

    private void SetVolume()
    {
        GetComponent<AudioSource>().volume = Settings.Instance.MusicVolume;
    }
}
