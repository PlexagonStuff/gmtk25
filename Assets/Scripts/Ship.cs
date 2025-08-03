using UnityEngine;
using UnityEngine.SceneManagement;
public class Ship : MonoBehaviour
{
    GameObject player;

    GameObject spawnLocation;

    private bool used = false;
    private void Start()
    {
        
    }

    private void FixedUpdate()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject == player)
        {
            SceneManager.LoadScene("MainMenu");
        }
    }
}
