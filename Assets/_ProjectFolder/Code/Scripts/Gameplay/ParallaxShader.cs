using UnityEngine;

public class ParallaxShader : MonoBehaviour
{
    public float speedMultiply = 1;
    public Vector2 direction = Vector2.right;

    private string id = "_TextureOffset";

    private Material material;
    private Vector2 delta;

    private void Start() => material = GetComponent<SpriteRenderer>().material;
    private void OnEnable() => delta = Time.fixedDeltaTime * speedMultiply * 0.1f * direction;
    private void FixedUpdate() => material.SetVector(id, (Vector2)material.GetVector(id) + delta);
}