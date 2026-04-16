using UnityEngine;

public class PlayerMaterial : MonoBehaviour
{
   Renderer r;

void Start()
{
    r = GetComponent<Renderer>();
}

void Update()
{
    Vector2 offset = r.material.mainTextureOffset;
    offset.y += Time.deltaTime * 0.5f;
    r.material.mainTextureOffset = offset;
}
}
