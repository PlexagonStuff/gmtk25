using UnityEngine;

public class Pickupdetector : MonoBehaviour
{
    private RobotController playerRC;
    void Start()
    {
        playerRC = transform.parent.GetComponent<RobotController>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject collisionGO = collision.gameObject;
        if (collisionGO.tag == "Throwable") {
            playerRC.addCloseInteractable(collisionGO.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        GameObject collisionGO = collision.gameObject;
        if (collisionGO.tag == "Throwable")
        {
            playerRC.removeCloseInteractable(collisionGO.gameObject);
        }
    }
}
