using UnityEngine;

public class FuelDepot : MonoBehaviour
{
    GameObject player;

    GameObject spawnLocation;

    private bool used = false;
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
            RobotController robot = player.GetComponent<RobotController>();
            if (used == false)
            {
                robot.Fuel = 50;
                used = true;
            }
            spawnLocation.transform.position = transform.position + Vector3.up * 3;
        }
    }
}
