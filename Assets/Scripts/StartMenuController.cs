
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class StartMenuController : MonoBehaviour
{
    [SerializeField] private GameObject levelSelectUI;

    private Button playButton;
    private Button selectLevelButton;
    private Button quitButton;

    private void OnEnable()
    {
        var root = GetComponent<UIDocument>().rootVisualElement;

        playButton = root.Q<Button>("PlayButton");
        selectLevelButton = root.Q<Button>("SelectLevelButton");
        quitButton = root.Q<Button>("QuitButton");

        if (playButton != null)
            playButton.clicked += OnPlay;

        if (selectLevelButton != null)
            selectLevelButton.clicked += OnSelectLevel;

        if (quitButton != null)
            quitButton.clicked += OnQuit;
    }

    private void OnDisable()
    {
        if (playButton != null)
            playButton.clicked -= OnPlay;

        if (selectLevelButton != null)
            selectLevelButton.clicked -= OnSelectLevel;

        if (quitButton != null)
            quitButton.clicked -= OnQuit;
    }

    private void OnPlay()
    {
           int level = LevelProgress.HighestUnlockedLevel;
    string sceneName = $"Level{level}";

    if (!Application.CanStreamedLevelBeLoaded(sceneName))
    {
        Debug.LogError($"Scene not found: {sceneName}");
        return;
    }

    Time.timeScale = 1f;
    SceneManager.LoadScene(sceneName);
    }

    private void OnSelectLevel()
    {
Debug.Log("SELECT CHAMBER clicked");

    if (levelSelectUI == null)
    {
        Debug.LogError("LevelSelectUI reference missing!");
        return;
    }

    levelSelectUI.SetActive(true);

    Debug.Log(
        "LevelSelectUI active: " +
        levelSelectUI.activeInHierarchy
    );

    gameObject.SetActive(false);
    }

    private void OnQuit()
    {
        Application.Quit();
    }
}
