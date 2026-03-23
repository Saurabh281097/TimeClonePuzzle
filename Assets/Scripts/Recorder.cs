using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public struct FrameData
{
    public Vector3 position;
}

public class Recorder : MonoBehaviour
{
    public List<FrameData> timeline = new List<FrameData>();
    public Transform player;

    void Update()
    {
        FrameData frame = new FrameData
        {
            position = player.position
        };

        timeline.Add(frame);
    }

    public List<FrameData> GetRecording()
    {
        return new List<FrameData>(timeline);
    }

    public void Clear()
    {
        timeline.Clear();
    }
}