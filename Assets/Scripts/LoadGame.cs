using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadGame : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            LoadLevel1();
        }
    }

    void LoadLevel1()
    {
        SceneManager.LoadScene("Level1");
    }
}
