using UnityEngine;

public class CloneSpawner : MonoBehaviour
{
    public GameObject clonePrefab;
    public Transform spawnPoint;

    public int maxClones = 3;
    private int currentClones = 0;

    public void SpawnClone()
    {
         Debug.Log("SpawnClone called");
        if (currentClones >= maxClones)
        {
            Debug.Log("No clones left");
            return;
        }

        Instantiate(clonePrefab, spawnPoint.position, Quaternion.identity);
        currentClones++;
    }
}