using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI cloneText;
    public GameManager gameManager;

    void Update()
    {
        int left = gameManager.maxClones - gameManager.CurrentCloneCount();
        cloneText.text = "Clones Left: " + left;
    }
}