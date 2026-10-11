
using UnityEngine;

public class ExitZone : MonoBehaviour
{
    public GameObject LevelCompletePanel;
    public GameObject LevelUI;

    [SerializeField] private int levelNumber = 1;

    private bool completed = false;

    private void OnTriggerEnter(Collider other)
    {
        if (completed || !other.CompareTag("Player"))
            return;

        completed = true;

        // Unlock next chamber.
        LevelProgress.UnlockNextLevel(levelNumber);

        if (LevelUI != null)
            LevelUI.SetActive(false);

        if (LevelCompletePanel != null)
        {
            LevelCompletePanel.SetActive(true);
            Time.timeScale = 0f;
        }
        else
        {
            Debug.LogError("LevelCompletePanel not assigned!");
        }
    }
}
