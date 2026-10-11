
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class LoadNextLevel : MonoBehaviour
{
    private Button nextButton;
    private Button retryButton;
    private Label completedLevelText;

    private void OnEnable()
    {
        var document = GetComponent<UIDocument>();

        if (document == null)
        {
            Debug.LogError("UIDocument missing.");
            return;
        }

        var root = document.rootVisualElement;

        nextButton = root.Q<Button>("NextLevel");
        retryButton = root.Q<Button>("RetryLevel");
        completedLevelText =
            root.Q<Label>("CompletedLevelText");

        int level = SceneManager.GetActiveScene().buildIndex;

        if (completedLevelText != null)
            completedLevelText.text = $"LEVEL {level:00}";

        if (nextButton != null)
            nextButton.clicked += OnNext;

        if (nextButton != null &&
    SceneManager.GetActiveScene().name == "Level3")
{
    nextButton.text = "BACK TO MENU";
}

        if (retryButton != null)
            retryButton.clicked += OnRetry;
    }

    private void OnDisable()
    {
        if (nextButton != null)
            nextButton.clicked -= OnNext;

        if (retryButton != null)
            retryButton.clicked -= OnRetry;
    }

    private void OnNext()
    {
    Time.timeScale = 1f;

    int currentIndex =
        SceneManager.GetActiveScene().buildIndex;

    if (currentIndex >= 3)
    {
        SceneManager.LoadScene("StartScene");
        return;
    }

    SceneManager.LoadScene($"Level{currentIndex + 1}");
    }

    private void OnRetry()
    {
        Time.timeScale = 1f;

        SceneManager.LoadScene(
            SceneManager.GetActiveScene().buildIndex
        );
    }
}
