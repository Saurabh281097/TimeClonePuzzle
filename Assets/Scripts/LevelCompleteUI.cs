using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class LevelCompleteUI : MonoBehaviour
{
    public GameObject panel;

    private bool isComplete = false;

    public void ShowLevelComplete()
    {
        panel.SetActive(true);
        Time.timeScale = 0f; // pause game
        isComplete = true;
    }

    void Update()
    {
        if (!isComplete) return;

        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            LoadNextLevel();
        }
    }

    public void LoadNextLevel()
    {
       Time.timeScale = 1f;

    int currentIndex = SceneManager.GetActiveScene().buildIndex;
    int nextIndex = currentIndex + 1;

    if (nextIndex < SceneManager.sceneCountInBuildSettings)
    {
        SceneManager.LoadScene(nextIndex);
    }
    else
    {
        // fallback to start scene
        SceneManager.LoadScene("StartScene");
    }
    }

    public void RestartLevel()
    {
        Time.timeScale = 1f;
        
        // Get current scene info
        Scene currentScene = SceneManager.GetActiveScene();
        
        // Option 1: Load by scene name (more reliable)
        SceneManager.LoadScene(currentScene.name);
        
        // Option 2: Load by build index (alternative if needed)
        // SceneManager.LoadScene(currentScene.buildIndex);
    }


}