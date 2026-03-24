using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class LoadNextLevel : MonoBehaviour
{
public Button nextButton;

void Start()
{
   var root = GetComponent<UIDocument>().rootVisualElement;

    nextButton = root.Q<Button>("NextLevel");

    nextButton.clicked += OnNext;
}

void OnNext()
{
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
}
