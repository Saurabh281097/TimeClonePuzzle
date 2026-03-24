using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitZone : MonoBehaviour
{
    public GameObject LevelCompletePanel;
    public GameObject LevelUI;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            LevelCompletePanel.SetActive(true);
            LevelUI.SetActive(false); 
        }
    }
}