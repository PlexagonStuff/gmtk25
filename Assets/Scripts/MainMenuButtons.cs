using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuButtons : MonoBehaviour
{
    [Header("Menu GameObjects")]
    [SerializeField]
    private GameObject mainMenu;
    [SerializeField]
    private GameObject settingsMenu;
    public AudioClip robotTheme;

    public void PlayButton()
    {
        FindFirstObjectByType<AudioSource>().clip = robotTheme;
        FindFirstObjectByType<AudioSource>().Play();
        SceneManager.LoadScene("GameScene");
    }

    public void SettingsButton()
    {
        mainMenu.SetActive(false);
        settingsMenu.SetActive(true);
    }

    public void QuitButton()
    {
        Application.Quit();
    }

    public void BackButtonSettings()
    {
        mainMenu.SetActive(true);
        settingsMenu.SetActive(false);
    }

    public void MusicVolumeSlider(System.Single newVolume)
    {
        Settings.Instance.MusicVolume = newVolume;
    }

    public void SFXVolumeSlider(System.Single newVolume)
    {
        Settings.Instance.SFXVolume = newVolume;
    }
}
