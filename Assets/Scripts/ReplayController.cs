using System.Collections.Generic;
using UnityEngine;

public class ReplayController : MonoBehaviour
{
    private List<FrameData> replayData;
    private int currentFrame;
    private float playbackTime;

    public bool IsPlaybackComplete { get; private set; }

    public void Init(List<FrameData> data)
    {
        replayData = data == null ? null : new List<FrameData>(data);
        currentFrame = 0;
        playbackTime = 0f;
        IsPlaybackComplete = replayData == null || replayData.Count == 0;
        if (!IsPlaybackComplete) transform.position = replayData[0].position;
    }

    private void Update() => AdvancePlayback(Time.deltaTime);

    // Plays according to recorded timestamps, independent of display frame rate.
    public void AdvancePlayback(float deltaTime)
    {
        if (IsPlaybackComplete || replayData == null || deltaTime <= 0f) return;
        playbackTime += deltaTime;
        while (currentFrame + 1 < replayData.Count &&
               replayData[currentFrame + 1].time <= playbackTime)
            currentFrame++;

        if (currentFrame >= replayData.Count - 1)
        {
            transform.position = replayData[replayData.Count - 1].position;
            IsPlaybackComplete = true;
            return;
        }

        FrameData a = replayData[currentFrame];
        FrameData b = replayData[currentFrame + 1];
        float span = b.time - a.time;
        float alpha = span > 0.00001f ? Mathf.Clamp01((playbackTime - a.time) / span) : 1f;
        transform.position = Vector3.LerpUnclamped(a.position, b.position, alpha);
    }
}
