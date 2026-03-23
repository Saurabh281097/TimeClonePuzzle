using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Joystick joystick;
    public float moveSpeed = 5f;
    float h = 0;

    void Start()
    {
        GetComponent<Renderer>().material.color = Color.yellow;
    }

    void Update()
    {
        if(Input.GetAxis("Horizontal") > 0 || joystick.Horizontal > 0)
              h = Mathf.Max(Input.GetAxis("Horizontal"), joystick.Horizontal);
        else if(Input.GetAxis("Horizontal") < 0 || joystick.Horizontal < 0)
                h = Mathf.Min(Input.GetAxis("Horizontal"), joystick.Horizontal);
            else
                h = 0;

        Vector3 move = new Vector3(h, 0, 0);
        transform.position += move * moveSpeed * Time.deltaTime;
    }
}
