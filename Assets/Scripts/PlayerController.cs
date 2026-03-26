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
        
        #if UNITY_EDITOR
            HandleMouseInput();   // simulate touch
        #elif UNITY_WEBGL
            HandleMouseInput();   // important: browsers use mouse
            HandleTouchInput();
        #else
            HandleTouchInput();   // real touch
        #endif
    
        Vector3 move = new Vector3(h, 0, 0);
        transform.position += move * moveSpeed * Time.deltaTime;
        bButtonPressed = false;
        moveSpeed = 5f;
    }

    void HandleTouchInput()
{
    if (Input.touchCount > 0)
    {
        Touch touch = Input.GetTouch(0);
        ProcessTouch(touch.position, touch.phase);
    }
}

void HandleMouseInput()
{
    if (Input.GetMouseButtonDown(0))
        ProcessTouch(Input.mousePosition, TouchPhase.Began);

    if (Input.GetMouseButton(0))
        ProcessTouch(Input.mousePosition, TouchPhase.Moved);

    if (Input.GetMouseButtonUp(0))
        ProcessTouch(Input.mousePosition, TouchPhase.Ended);
}

void ProcessTouch(Vector2 pos, TouchPhase phase)
{
    if (pos.x < Screen.width / 2)
        MoveLeft();
    else
        MoveRight();
}

void MoveLeft()
{
    h = -1;
    Debug.Log("Move Left");
    Vector3 move = new Vector3(h, 0, 0);
    transform.position += move * moveSpeed * Time.deltaTime;
}

void MoveRight()
{
    h = 1;
    Debug.Log("Move Right");
    Vector3 move = new Vector3(h, 0, 0);
    transform.position += move * moveSpeed * Time.deltaTime;
}



public void move(int input)
{
       h = input;
       Debug.Log("Move called with input: " + input);
       bButtonPressed = true;
}
}
