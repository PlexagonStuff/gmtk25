using UnityEngine;

public class ThowableObjectBehavior : MonoBehaviour
{
    private bool lerping = false;
    
    public void setLerping(bool _lerping)
    {
        lerping = _lerping;
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
            if (!rc.GetIsHoldingInteractable())
            {
                rc.SetInteractableObject(gameObject);
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        GameObject go = collision.gameObject;
        if (go.tag == "Player")
        {
            RobotController rc = go.GetComponent<RobotController>();
            if (rc.GetInteractableObject() == gameObject)
            {
                rc.SetInteractableObject(null);
            }
        }
    }
}
