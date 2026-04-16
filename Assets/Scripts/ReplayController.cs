using System.Collections.Generic;
using UnityEngine;

public class ReplayController : MonoBehaviour
{
    private List<FrameData> replayData;
    private int currentFrame = 0;

    void Start()
    {
        //GetComponent<Renderer>().material.color = Color.magenta;
    }

    public void Init(List<FrameData> data)
    {
        replayData = data;
        currentFrame = 0;
    }

    void Update()
    {
        if (replayData == null || currentFrame >= replayData.Count)
            return;

        transform.position = replayData[currentFrame].position;
        currentFrame++;
    }
}