using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct FrameData
{
    public float time;
    public Vector3 position;

    public FrameData(float time, Vector3 position)
    {
        this.time = time;
        this.position = position;
    }
}

public class Recorder : MonoBehaviour
{
    [Min(1f)] public float samplesPerSecond = 20f;
    [Min(0.1f)] public float maxDuration = 10f;
    public Transform player;

    // Preserves the existing public timeline contract used elsewhere in the project.
    public List<FrameData> timeline = new List<FrameData>();

    private float elapsed;
    private float nextSampleTime;
    private Vector3 previousPosition;
    private bool initialized;
    private bool recording = true;

    private void Start()
    {
        if (player == null) { Debug.LogError("Recorder: player reference missing", this); enabled = false; return; }
        StartRecording();
    }

    private void Update()
    {
        TickRecording(Time.deltaTime);
    }

    public void StartRecording()
    {
        if (player == null) return;
        timeline.Clear();
        elapsed = 0f;
        nextSampleTime = 1f / Mathf.Max(1f, samplesPerSecond);
        previousPosition = player.position;
        timeline.Add(new FrameData(0f, previousPosition));
        initialized = true;
        recording = true;
    }

    public void StopRecording() => recording = false;

    // Public to make timeline sampling deterministic and testable without rendering frames.
    public void TickRecording(float deltaTime)
    {
        if (!recording || !initialized || player == null || deltaTime <= 0f) return;

        float startTime = elapsed;
        float endTime = Mathf.Min(elapsed + deltaTime, maxDuration);
        Vector3 currentPosition = player.position;
        float interval = 1f / Mathf.Max(1f, samplesPerSecond);
        while (nextSampleTime <= endTime + 0.00001f)
        {
            float fraction = endTime > startTime ?
                Mathf.Clamp01((nextSampleTime - startTime) / (endTime - startTime)) : 1f;
            timeline.Add(new FrameData(nextSampleTime, Vector3.LerpUnclamped(previousPosition, currentPosition, fraction)));
            nextSampleTime += interval;
        }
        elapsed = endTime;
        previousPosition = currentPosition;
        if (elapsed >= maxDuration) recording = false;
    }

    public List<FrameData> GetRecording() => new List<FrameData>(timeline);

    // Keep Clear() for other existing callers; it now resets the recording clock too.
    public void Clear() => StartRecording();
}
