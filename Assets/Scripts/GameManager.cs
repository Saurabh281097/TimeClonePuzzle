using UnityEngine;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public Recorder recorder;
    public GameObject clonePrefab;
    public Transform player;
    public int maxClones = 5;
    private List<GameObject> clones = new List<GameObject>();

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            Rewind();
        }
    }

   void Rewind()
{
    List<FrameData> data = recorder.GetRecording();

    // ❌ Prevent empty clones
    if (data.Count == 0)
        return;

    // ❌ Limit number of clones
    if (clones.Count >= maxClones)
    {
        // Destroy(clones[0]);
        // clones.RemoveAt(0);
        return;
    }

    // ✅ Spawn clone at correct start position
    GameObject clone = Instantiate(clonePrefab, data[0].position, Quaternion.identity);

    ReplayController replay = clone.GetComponent<ReplayController>();
    replay.Init(data);

    clones.Add(clone);

    // ✅ Reset player
    player.position = Vector3.zero;

    // ✅ Clear recording
    recorder.Clear();
}

public int CurrentCloneCount()
{
    return clones.Count;
}
}
