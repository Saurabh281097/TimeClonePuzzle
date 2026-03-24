using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class GameUIController : MonoBehaviour
{
    private Label levelText;
    private Label timerText;
    private Label cloneText;

    private Button replayButton;
    private Button resetButton;
    private Button startButton;
    private Button leftButton;
    private Button rightButton;

    private float timer = 0f;
    private int cloneCount = 0;
    private int levelNumber = 1;
    

    void Start()
    {
        var root = GetComponent<UIDocument>().rootVisualElement;

        // ===== Get References =====
        levelText = root.Q<Label>("LevelText");
        timerText = root.Q<Label>("TimerText");
        cloneText = root.Q<Label>("CloneText");

        replayButton = root.Q<Button>("Replay");
        resetButton = root.Q<Button>("Restart");
        startButton = root.Q<Button>("StartButton"); // optional
        leftButton = root.Q<Button>("Left"); 
        rightButton = root.Q<Button>("Right"); 

        // ===== Safety Checks =====
        if (replayButton == null) Debug.LogError("ReplayButton not found");
        if (resetButton == null) Debug.LogError("ResetButton not found");
        if (cloneText == null) Debug.LogError("CloneText not found");

        // ===== Bind Events =====
        replayButton.clicked += OnReplayClicked;
        resetButton.clicked += OnResetClicked;
        leftButton.clicked += OnLeftClicked;
        rightButton.clicked += OnRightClicked;

        if (startButton != null)
            startButton.clicked += OnStartClicked;

        // ===== Initialize UI =====
        levelNumber = SceneManager.GetActiveScene().buildIndex; 
        Debug.Log("Current Level: " + levelNumber);
        UpdateLevel(levelNumber);
         UpdateCloneCount();
        UpdateTimer(0);
    }

    void OnLeftClicked()
    {
        // Handle left button click
        Debug.Log("Left Button Clicked");
        GameObject gmObj = GameObject.FindGameObjectWithTag("Player");

        if (gmObj != null)
        {
            PlayerController pc = gmObj.GetComponent<PlayerController>();
            
            if (pc != null)
            {
                pc.move(-1);
            }
        }
    }

    void OnRightClicked()
    {
        // Handle right button click
        Debug.Log("Right Button Clicked");
        GameObject gmObj = GameObject.FindGameObjectWithTag("Player");

        if (gmObj != null)
        {
            Debug.Log("Right Button Clicked 11");
            PlayerController pc = gmObj.GetComponent<PlayerController>();
            
            if (pc != null)
            {
                pc.move(1);
            }
        }
    }

    void Update()
    {
        // Timer update
        timer += Time.deltaTime;
        UpdateTimer(timer);
        UpdateCloneCount();
    }

    // ===== Button Handlers =====

    void OnReplayClicked()
    {
        Debug.Log("Replay Clicked");

        timer = 0;
        // TODO: trigger your clone replay system
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

    void OnResetClicked()
    {
        Debug.Log("Reset Clicked");

        // TODO: reset player + clones
         SceneManager.LoadScene("Level1");
       
    }

    void OnStartClicked()
    {
        SceneManager.LoadScene("Level1");
    }

    // ===== UI Update Functions =====

    public void UpdateLevel(int level)
    {
        levelNumber = level;
        if (levelText != null)
            levelText.text = "Level " + level;
    }

    public void UpdateCloneCount()
    {
       GameObject gmObj = GameObject.FindGameObjectWithTag("GameManager");

        if (gmObj != null)
        {
            GameManager gm = gmObj.GetComponent<GameManager>();
            
            if (gm != null)
            {
                 if (cloneText != null)
                    cloneText.text = "Clones: " + (gm.maxClones - gm.CurrentCloneCount());
            }
        }
    }

    public void UpdateTimer(float time)
    {
        if (timerText != null)
            timerText.text = time.ToString("F1");
    }
}