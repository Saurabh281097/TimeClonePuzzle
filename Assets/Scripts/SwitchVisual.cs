
using UnityEngine;

public class SwitchVisual : MonoBehaviour
{
    [SerializeField] private Switch pressureSwitch;
    [SerializeField] private Renderer statusLight;

    [SerializeField] private Color inactiveColor =
        new Color(0.937f, 0.349f, 0.4f);

    [SerializeField] private Color activeColor =
        new Color(0.322f, 0.878f, 0.635f);

    [SerializeField] private float glowIntensity = 1.5f;

    private MaterialPropertyBlock propertyBlock;
    private bool? previousState;

    private void Awake()
    {
        propertyBlock = new MaterialPropertyBlock();

        if (pressureSwitch == null)
            pressureSwitch = GetComponentInParent<Switch>();
    }

    private void Update()
    {
        if (pressureSwitch == null || statusLight == null)
            return;

        bool active = pressureSwitch.isPressed;

        if (previousState == active)
            return;

        previousState = active;

        Color color = active ? activeColor : inactiveColor;

        statusLight.GetPropertyBlock(propertyBlock);
        propertyBlock.SetColor("_BaseColor", color);
        propertyBlock.SetColor(
            "_EmissionColor",
            color * glowIntensity
        );
        statusLight.SetPropertyBlock(propertyBlock);
    }
}
