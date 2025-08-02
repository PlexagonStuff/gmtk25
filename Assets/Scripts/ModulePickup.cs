using UnityEngine;

public class ModulePickup : MonoBehaviour
{
    public enum moduleType
    {
        wheel, 
        hover
    }

    public moduleType t;

    private InfoManager manager;

    private void Start()
    {
        manager = GameObject.Find("Manager").GetComponent<InfoManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("collided with Module");
        if (collision.gameObject.tag == "Player")
        {
            RobotController rc = collision.gameObject.GetComponent<RobotController>();
            if (t == moduleType.wheel)
            {
                manager.collectedWheel = true;
            } else if (t == moduleType.hover)
            {
                manager.collectedhover = true;
            }
            Destroy(gameObject);
        }
    }
}
