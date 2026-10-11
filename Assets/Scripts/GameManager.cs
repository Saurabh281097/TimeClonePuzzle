using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Recorder recorder;
    public GameObject clonePrefab;
    public Transform player;
    public int maxClones = 5;
    private readonly List<GameObject> clones = new List<GameObject>();
    public Button rewindButton;
    private Vector3 playerStartPosition;
    private Quaternion playerStartRotation;

    private void Start()
    {
        if (player != null)
        {
            playerStartPosition = player.position;
            playerStartRotation = player.rotation;
        }
        if (rewindButton != null) rewindButton.onClick.AddListener(Rewind);
    }

    private void OnDestroy()
    {
        if (rewindButton != null) rewindButton.onClick.RemoveListener(Rewind);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R)) Rewind();
    }

    public void Rewind()
    {
        if (recorder == null || clonePrefab == null || player == null) return;
        if (clones.Count >= maxClones) return;

        List<FrameData> data = recorder.GetRecording();
        if (data.Count < 2) return;

        GameObject clone = Instantiate(clonePrefab, data[0].position, Quaternion.identity);
        ReplayController replay = clone.GetComponent<ReplayController>();
        if (replay == null)
        {
            Debug.LogError("Clone prefab needs a ReplayController", clone);
            Destroy(clone);
            return;
        }
        replay.Init(data);
        clones.Add(clone);

        // Existing clones remain active: preserving multi-clone puzzle behavior.
        player.SetPositionAndRotation(playerStartPosition, playerStartRotation);
        recorder.Clear(); // records a fresh t=0 frame at the reset position
    }

    public int CurrentCloneCount() => clones.Count;
}
