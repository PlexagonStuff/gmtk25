using UnityEngine;

public class Settings : MonoBehaviour
{
    public static Settings Instance { get; private set; }

    public float musicVolume = 0.5f;
    public float sfxVolume = 0.5f;

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
