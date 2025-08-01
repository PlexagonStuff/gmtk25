using UnityEngine;

public class ThowableObjectBehavior : MonoBehaviour
{
    public bool lerping = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.parent != null && !lerping)
        {
            transform.position = transform.parent.position;
            transform.rotation = transform.parent.rotation;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject go = collision.gameObject;
        if (go.tag == "Player")
        {
            RobotController rc = go.GetComponent<RobotController>();
            if (!rc.isHoldingInteractable)
            {
                rc.interactableObject = gameObject;
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        GameObject go = collision.gameObject;
        if (go.tag == "Player")
        {
            RobotController rc = go.GetComponent<RobotController>();
            if (rc.interactableObject == gameObject)
            {
                rc.interactableObject = null;
            }
        }
    }
}
