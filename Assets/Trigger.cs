using Unity.VisualScripting;
using UnityEngine;
using System.Collections.Generic;
//using UnityEditor.Build.Content;

public class Trigger : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public int id = -1;
    private List<Target> targets = new List<Target>();
    public bool triggered = false;
    void Start()
    {
        Target[] allTargets = Object.FindObjectsByType<Target>(FindObjectsSortMode.None);
        for (int i = 0; i < allTargets.Length; i++)
        {
            if (allTargets[i].id == id)
            {
                targets.Add(allTargets[i]);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < targets.Count || !triggered; i++)
        {
            if (targets[i].triggered)
            {
                triggered = true;
            }
        }
        if (triggered)
        {
            if (gameObject.transform.position.x > -20)
            {
                gameObject.transform.position = gameObject.transform.position + new Vector3(-0.05f, 0, 0);
            }
        }
    }
}
