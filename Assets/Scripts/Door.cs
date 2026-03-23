using UnityEngine;
using System.Collections.Generic;

public class Door : MonoBehaviour
{
    public List<Switch> switches;

    public Vector3 openPosition;
    public Vector3 closedPosition;
    public float speed = 2f;

    void Update()
    {
        bool allPressed = true;

        foreach (var sw in switches)
        {
            if (!sw.isPressed)
            {
                allPressed = false;
                break;
            }
        }

        Vector3 targetPos = allPressed ? openPosition : closedPosition;
        transform.position = Vector3.Lerp(transform.position, targetPos, Time.deltaTime * speed);
    }
}