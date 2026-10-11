
using UnityEngine;
using System.Collections.Generic;

public class Door : MonoBehaviour
{
    public List<Switch> switches;

    public Vector3 openPosition;
    public Vector3 closedPosition;
    public float speed = 2f;

    [Header("Door Sound")]
    [SerializeField] private AudioSource doorAudioSource;
    [SerializeField] private AudioClip doorOpenSound;

    private bool wasOpen = false;

    void Start()
    {
        // Prevent sound playing immediately
        // if the door starts in an open state.
        wasOpen = AreAllSwitchesPressed();
    }

    void Update()
    {
        // Step 1: Check all connected switches
        bool allPressed = AreAllSwitchesPressed();

        // Step 2: Play sound only when
        // door changes from closed to opening
        if (allPressed && !wasOpen)
        {
            PlayDoorSound();
        }

        // Step 3: Remember previous state
        wasOpen = allPressed;

        // Step 4: Move the door
        Vector3 targetPos =
            allPressed ? openPosition : closedPosition;

        transform.position = Vector3.Lerp(
            transform.position,
            targetPos,
            Time.deltaTime * speed
        );
    }

    private bool AreAllSwitchesPressed()
    {
        if (switches == null || switches.Count == 0)
            return false;

        foreach (var sw in switches)
        {
            if (sw == null || !sw.isPressed)
                return false;
        }

        return true;
    }

    private void PlayDoorSound()
    {
        if (doorAudioSource != null &&
            doorOpenSound != null)
        {
            doorAudioSource.PlayOneShot(doorOpenSound);
        }
    }
}
