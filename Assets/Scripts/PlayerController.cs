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

    public float doubleTapTime = 1.0f;
    public float tapDelay = 0.1f;

    private float lastTapTime = 0f;
    private bool waitingForSecondTap = false;

    private Vector2 currentInputPos;
    private bool isHolding = false;

    void Start()
    {
        GetComponent<Renderer>().material.color = Color.yellow;
    }

    void Update()
    {
        #if UNITY_EDITOR
            HandleMouseInput();   // simulate touch
        #elif UNITY_WEBGL
            HandleMouseInput();   // important: browsers use mouse
            HandleTouchInput();
        #else
            HandleTouchInput();   // real touch
        #endif
    
        HandleMovement();
    }

    void HandleTouchInput()
{
    if (Input.touchCount == 0) return;

    Touch touch = Input.GetTouch(0);

    if (touch.phase == TouchPhase.Began)
    {
        currentInputPos = touch.position;

        if (touch.tapCount == 2)
        {
            // DOUBLE TAP → clone only
            SpawnClone();
            isHolding = false;
            waitingForSecondTap = false;
            return;
        }

        // single tap → prepare for hold movement
        waitingForSecondTap = true;
        Invoke(nameof(EnableHold), doubleTapTime);
    }

    if (touch.phase == TouchPhase.Moved || touch.phase == TouchPhase.Stationary)
    {
        isHolding = true;
        currentInputPos = touch.position;
    }

    if (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled)
    {
        isHolding = false;
    }
}

void HandleMouseInput()
{
 if (Input.GetMouseButtonDown(0))
        {
            currentInputPos = Input.mousePosition;

            if (Time.time - lastTapTime <= doubleTapTime)
            {
                CancelInvoke(nameof(EnableHold));
                SpawnClone();

                waitingForSecondTap = false;
                isHolding = false;
                lastTapTime = 0f;
                return;
            }
            else
            {
                waitingForSecondTap = true;
                lastTapTime = Time.time;
                Invoke(nameof(EnableHold), tapDelay);
            }
        }

        if (Input.GetMouseButton(0))
        {
            currentInputPos = Input.mousePosition;
            isHolding = true;
        }

        if (Input.GetMouseButtonUp(0))
        {
            isHolding = false;
        }
}

void ProcessTouch(Vector2 pos, TouchPhase phase)
{
    if (Input.touchCount == 0) return;

        Touch touch = Input.GetTouch(0);

        if (touch.phase == TouchPhase.Began)
        {
            currentInputPos = touch.position;

            if (Time.time - lastTapTime <= doubleTapTime)
            {
                CancelInvoke(nameof(EnableHold));
                SpawnClone();

                waitingForSecondTap = false;
                isHolding = false;
                lastTapTime = 0f;
                return;
            }
            else
            {
                waitingForSecondTap = true;
                lastTapTime = Time.time;
                Invoke(nameof(EnableHold), tapDelay);
            }
        }

        if (touch.phase == TouchPhase.Moved || touch.phase == TouchPhase.Stationary)
        {
            currentInputPos = touch.position;
            isHolding = true;
        }

        if (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled)
        {
            isHolding = false;
        }
}

 void EnableHold()
    {
        waitingForSecondTap = false;
    }

     void HandleMovement()
    {
        if (waitingForSecondTap) return;
        if (!isHolding) return;

        if (currentInputPos.x < Screen.width / 2f)
            MoveLeft();
        else
            MoveRight();
    }


void DetectDoubleTap(Vector2 pos)
{
    if (Time.time - lastTapTime <= doubleTapTime)
    {
        SpawnClone();
    }
    lastTapTime = Time.time;
}

void SpawnClone()
{
    Debug.Log("Spawn Clone");
    GameObject gmObj = GameObject.FindGameObjectWithTag("GameManager");

    if (gmObj != null)
        {
            GameManager gm = gmObj.GetComponent<GameManager>();
            
            if (gm != null)
            {
                gm.Rewind();
            }
        }
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
