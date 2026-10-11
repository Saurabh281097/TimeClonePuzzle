using System.Collections.Generic;
using UnityEngine;

public class DoorVisual : MonoBehaviour
{
    [SerializeField] private Door door;
    [SerializeField] private Renderer statusLightRenderer;

    [SerializeField] private List<Switch> switches;

    [SerializeField] private Color lockedColor = new Color(0.937f, 0.349f, 0.4f);
    [SerializeField] private Color unlockedColor = new Color(0.322f, 0.878f, 0.635f);
    [SerializeField] private float emissionIntensity = 1.5f;

    private MaterialPropertyBlock block;

    private void Awake()
    {
        block = new MaterialPropertyBlock();
    }

    private void Update()
    {
        if (statusLightRenderer == null || switches == null || switches.Count == 0)
            return;

        bool allPressed = true;

        foreach (var sw in switches)
        {
            if (sw == null || !sw.isPressed)
            {
                allPressed = false;
                break;
            }
        }

        Color c = allPressed ? unlockedColor : lockedColor;

        statusLightRenderer.GetPropertyBlock(block);
        block.SetColor("_BaseColor", c);
        block.SetColor("_EmissionColor", c * emissionIntensity);
        statusLightRenderer.SetPropertyBlock(block);
    }
}