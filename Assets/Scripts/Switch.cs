using UnityEngine;

public class Switch : MonoBehaviour
{
    public bool isPressed = false;

    private int objectsOnSwitch = 0;

    void Update()
    {
       GetComponent<Renderer>().material.color = isPressed ? Color.green : Color.red;
       Vector3 targetScale = isPressed ? new Vector3(1, 0.8f, 1) : new Vector3(1, 0.5f, 1);
        transform.localScale = Vector3.Lerp(transform.localScale, targetScale, Time.deltaTime * 8f);
        transform.Rotate(Vector3.up * 50 * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Entered by: " + other.name);
        if (other.CompareTag("Player") || other.CompareTag("Clone"))
        {
            objectsOnSwitch++;
            isPressed = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") || other.CompareTag("Clone"))
        {
            objectsOnSwitch--;

            if (objectsOnSwitch <= 0)
            {
                isPressed = false;
                objectsOnSwitch = 0;
            }
        }
    }
}