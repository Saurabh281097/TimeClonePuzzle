
using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

[RequireComponent(typeof(UIDocument))]
public class LevelSelectController : MonoBehaviour
{
    [SerializeField] private GameObject mainMenuUI;

    private Button[] levelButtons;
    private Action[] levelActions;
    private Button backButton;

    private void OnEnable()
    {
        var root = GetComponent<UIDocument>().rootVisualElement;

        levelButtons = new Button[LevelProgress.MaxLevels];
        levelActions = new Action[LevelProgress.MaxLevels];

        for (int i = 0; i < LevelProgress.MaxLevels; i++)
        {
            int level = i + 1;
            Button button = root.Q<Button>($"Level{level}Button");

            levelButtons[i] = button;

            if (button == null)
            {
                Debug.LogWarning($"Level{level}Button missing.");
                continue;
            }

            bool unlocked = LevelProgress.IsUnlocked(level);

            button.SetEnabled(unlocked);
            button.text = unlocked
                ? $"CHAMBER {level:00}"
                : $"LOCKED {level:00}";

            int selectedLevel = level;
            levelActions[i] = () => LoadLevel(selectedLevel);
            button.clicked += levelActions[i];
        }

        backButton = root.Q<Button>("BackButton");

        if (backButton != null)
            backButton.clicked += OnBack;
    }

    private void OnDisable()
    {
        if (levelButtons != null && levelActions != null)
        {
            for (int i = 0; i < levelButtons.Length; i++)
            {
                if (levelButtons[i] != null &&
                    levelActions[i] != null)
                {
                    levelButtons[i].clicked -= levelActions[i];
                }
            }
        }

        if (backButton != null)
            backButton.clicked -= OnBack;
    }

    private void LoadLevel(int level)
    {
        if (!LevelProgress.IsUnlocked(level))
            return;

        string sceneName = $"Level{level}";

        if (!Application.CanStreamedLevelBeLoaded(sceneName))
        {
            Debug.LogError($"Scene missing: {sceneName}");
            return;
        }

        Time.timeScale = 1f;
        SceneManager.LoadScene(sceneName);
    }

    private void Start()
{
    var document = GetComponent<UIDocument>();

    Debug.Log("LevelSelect UIDocument: " +
              (document != null));

    if (document == null)
        return;

    var root = document.rootVisualElement;

    Debug.Log("LevelSelect children: " +
              root.childCount);

    Debug.Log("Level1Button found: " +
              (root.Q<UnityEngine.UIElements.Button>(
                  "Level1Button") != null));
}

    private void OnBack()
    {
        if (mainMenuUI == null)
        {
            Debug.LogError("MainMenuUI reference missing.");
            return;
        }

        mainMenuUI.SetActive(true);
        gameObject.SetActive(false);
    }
}
