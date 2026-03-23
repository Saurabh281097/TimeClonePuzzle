using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;

    void Start()
    {
        GetComponent<Renderer>().material.color = Color.yellow;
    }

    void Update()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        Vector3 move = new Vector3(h, 0, 0);
        transform.position += move * moveSpeed * Time.deltaTime;
    }
}
