using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitZone : MonoBehaviour
{
    public LevelCompleteUI levelCompleteUI;
    public GameObject gamePanel;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            gamePanel.SetActive(false);
            levelCompleteUI.ShowLevelComplete();
        }
    }
}