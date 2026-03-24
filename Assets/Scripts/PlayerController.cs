using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Joystick joystick;
    public float moveSpeed = 5f;
    float h = 0;
    float moveX = 0;
    float moveY = 0;
    bool actionPressed = false;
    bool bButtonPressed = false;

    void Start()
    {
        GetComponent<Renderer>().material.color = Color.yellow;
    }

    void Update()
    {
        if(!bButtonPressed)
            h = Input.GetAxis("Horizontal");
        else
            moveSpeed = 60;
    
        #if UNITY_ANDROID || UNITY_IOS
        moveX = 0;
        moveY = 0;
        actionPressed = false;
        HandleTouchInput();
        h = moveX;
        #endif
        Vector3 move = new Vector3(h, 0, 0);
        transform.position += move * moveSpeed * Time.deltaTime;
        bButtonPressed = false;
        moveSpeed = 5f;
    }

    void HandleTouchInput()
    {
        if (Input.touchCount == 0) return;

        Touch touch = Input.GetTouch(0);

        // Simple logic: left/right side movement
        if (touch.position.x < Screen.width / 2)
        {
            moveX = -1; // Left
        }
        else
        {
            moveX = 1; // Right
        }

        if (touch.phase == TouchPhase.Began)
        {
            actionPressed = true;
        }
    }

    public void move(int input)
    {
       h = input;
       Debug.Log("Move called with input: " + input);
       bButtonPressed = true;
    }
}
