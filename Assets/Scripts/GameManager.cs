using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Recorder recorder;
    public GameObject clonePrefab;
    public Transform player;
    public int maxClones = 5;
    private List<GameObject> clones = new List<GameObject>();
    public Button rewindButton;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            Rewind();
        }
    }

    void Start()
    {
        rewindButton.onClick.AddListener(Rewind);
    }

   public void Rewind()
{
    List<FrameData> data = recorder.GetRecording();

    // Prevent empty clones
    if (data.Count == 0)
        return;

    // Limit number of clones
    if (clones.Count >= maxClones)
    {
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
