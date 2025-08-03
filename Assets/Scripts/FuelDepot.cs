using UnityEngine;

public class FuelDepot : MonoBehaviour
{
    GameObject player;

    GameObject spawnLocation;

    private void Start()
    {
        spawnLocation = GameObject.Find("StartPos");
    }

    private void FixedUpdate()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject == player)
        {
            spawnLocation.transform.position = transform.position + Vector3.up * 3;
        }
    }
}
