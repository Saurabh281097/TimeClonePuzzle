using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class InstructionUI : MonoBehaviour
{
     void Start()
    {
        var root = GetComponent<UIDocument>().rootVisualElement;

        Button startButton = root.Q<Button>("Start");

        startButton.clicked += OnStartClicked;
    }

    void OnStartClicked()
    {
        SceneManager.LoadScene("Level1");
    }
}