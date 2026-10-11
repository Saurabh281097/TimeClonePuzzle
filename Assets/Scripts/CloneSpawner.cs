using UnityEngine;
using System.Collections;

public class CloneSpawner : MonoBehaviour
{
    public GameObject clonePrefab;
    public Transform spawnPoint;

    public int maxClones = 3;
    private int currentClones = 0;

    public void SpawnClone()
    {
        Debug.Log("Attempting to spawn clone. Current clones: " + currentClones);
         Debug.Log("SpawnClone called");
        if (currentClones >= maxClones)
        {
            Debug.Log("No clones left");
            return;
        }

        GameObject clone = Instantiate(clonePrefab, spawnPoint.position, Quaternion.identity);
        Collider cloneCol = clone.GetComponent<Collider>();
        if(cloneCol != null)
        {
            Debug.Log("Collider found on clone");
        }
        else
        {
            Debug.LogError("No collider found on clone prefab!");
        }
        cloneCol.enabled = false;
        StartCoroutine(EnableCollisionAfterDelay(cloneCol, 30.0f));
        currentClones++;
    }

    IEnumerator EnableCollisionAfterDelay(Collider col, float delay)
    {
        yield return new WaitForSeconds(delay);
        col.enabled = true;
    }
}