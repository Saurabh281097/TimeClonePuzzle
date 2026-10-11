using System;
using System.Collections;
using System.Reflection;
using NUnit.Framework;
using UnityEngine;

// Reflection allows tests to exercise the project's predefined Assembly-CSharp
// without relocating gameplay scripts into a custom assembly definition.
public class TimeRecordingTests
{
    private static Type Script(string name) => Type.GetType(name + ", Assembly-CSharp", true);
    private static Component Add(GameObject go, string type) => go.AddComponent(Script(type));
    private static void Set(Component c, string field, object value) => c.GetType().GetField(field).SetValue(c, value);
    private static object Call(Component c, string method, params object[] args) => c.GetType().GetMethod(method).Invoke(c, args);

    private static IList Samples(params (float t, float x)[] positions)
    {
        Type frame = Script("FrameData");
        Type listType = typeof(System.Collections.Generic.List<>).MakeGenericType(frame);
        IList list = (IList)Activator.CreateInstance(listType);
        ConstructorInfo ctor = frame.GetConstructor(new[] { typeof(float), typeof(Vector3) });
        foreach (var p in positions) list.Add(ctor.Invoke(new object[] { p.t, new Vector3(p.x, 0f, 0f) }));
        return list;
    }

    [Test]
    public void ReplayIsIndependentOfDisplayFrameRate()
    {
        var slowGo = new GameObject("slow"); var fastGo = new GameObject("fast");
        try
        {
            var slow = Add(slowGo, "ReplayController"); var fast = Add(fastGo, "ReplayController");
            Call(slow, "Init", Samples((0f, 0f), (0.5f, 5f), (1f, 10f)));
            Call(fast, "Init", Samples((0f, 0f), (0.5f, 5f), (1f, 10f)));
            for (int i = 0; i < 15; i++) Call(slow, "AdvancePlayback", 1f / 30f);
            for (int i = 0; i < 30; i++) Call(fast, "AdvancePlayback", 1f / 60f);
            Assert.That(slowGo.transform.position.x, Is.EqualTo(5f).Within(0.001f));
            Assert.That(fastGo.transform.position.x, Is.EqualTo(5f).Within(0.001f));
        }
        finally { UnityEngine.Object.DestroyImmediate(slowGo); UnityEngine.Object.DestroyImmediate(fastGo); }
    }

    [Test]
    public void ReplayEndsAtFinalPosition()
    {
        var go = new GameObject("clone");
        try
        {
            var replay = Add(go, "ReplayController");
            Call(replay, "Init", Samples((0f, 0f), (1f, 10f)));
            Call(replay, "AdvancePlayback", 2f);
            Assert.That(go.transform.position.x, Is.EqualTo(10f).Within(0.001f));
            Assert.IsTrue((bool)replay.GetType().GetProperty("IsPlaybackComplete").GetValue(replay));
        }
        finally { UnityEngine.Object.DestroyImmediate(go); }
    }

    [Test]
    public void RecordingIsTimeSampledAndDurationLimited()
    {
        var player = new GameObject("player"); var go = new GameObject("recorder");
        try
        {
            var recorder = Add(go, "Recorder");
            Set(recorder, "player", player.transform);
            Set(recorder, "samplesPerSecond", 20f);
            Set(recorder, "maxDuration", 1f);
            Call(recorder, "StartRecording");
            for (int i = 1; i <= 10; i++)
            {
                player.transform.position = new Vector3(i, 0f, 0f);
                Call(recorder, "TickRecording", 0.1f);
            }
            IList samples = (IList)Call(recorder, "GetRecording");
            Assert.AreEqual(21, samples.Count);
            object sample = samples[10];
            Assert.That((float)sample.GetType().GetField("time").GetValue(sample), Is.EqualTo(0.5f).Within(0.0001f));
            Assert.That(((Vector3)sample.GetType().GetField("position").GetValue(sample)).x, Is.EqualTo(5f).Within(0.001f));
            Call(recorder, "TickRecording", 2f);
            Assert.AreEqual(21, ((IList)Call(recorder, "GetRecording")).Count);
        }
        finally { UnityEngine.Object.DestroyImmediate(go); UnityEngine.Object.DestroyImmediate(player); }
    }

    [Test]
    public void ClearStartsNewRecordingAtCurrentPlayerPosition()
    {
        var player = new GameObject("player"); var go = new GameObject("recorder");
        try
        {
            var recorder = Add(go, "Recorder");
            Set(recorder, "player", player.transform);
            Call(recorder, "StartRecording");
            player.transform.position = new Vector3(7f, 0f, 0f);
            Call(recorder, "Clear");
            IList samples = (IList)Call(recorder, "GetRecording");
            Assert.AreEqual(1, samples.Count);
            object sample = samples[0];
            Assert.AreEqual(0f, (float)sample.GetType().GetField("time").GetValue(sample));
            Assert.AreEqual(7f, ((Vector3)sample.GetType().GetField("position").GetValue(sample)).x);
        }
        finally { UnityEngine.Object.DestroyImmediate(go); UnityEngine.Object.DestroyImmediate(player); }
    }
}
