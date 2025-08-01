using UnityEditor.EventSystems;
using UnityEngine;

public class Target : MonoBehaviour
{
    public bool triggered = false;
    public int id = -1;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<Bullet>() != null )
        {
            triggered = true;
            Debug.Log("Object entered trigger: " + other.gameObject.name);
            Destroy(other.gameObject);
        }
    }


}
