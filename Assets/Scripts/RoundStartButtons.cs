using UnityEngine;
using UnityEngine.SceneManagement;

public class RoundStartButtons : MonoBehaviour
{
    public void StartButton()
    {
        SceneManager.LoadScene("GameScene");
    }
}
