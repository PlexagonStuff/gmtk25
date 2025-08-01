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
}
