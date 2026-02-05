using UnityEngine;
using Unity.Mathematics;

namespace Environment
{
    public class Parallax : GameplayListener
    {
        [SerializeField] private float _speedMultiply = 1f;
        [SerializeField] private float2 _direction = float2.zero;

        private const string _id = "_TextureOffset";

        private Material _material;
        private float2 _tileSize;

        private void Start()
        {
            SpriteRenderer render = GetComponent<SpriteRenderer>();

            float2 units = render.sprite.rect.size / render.sprite.pixelsPerUnit;
            _tileSize = _direction * 0.02f * ((float2)render.size / units);
            _material = render.material;
        }
        protected override void GameUpdate(float traveled)
        {
            Vector2 movement = traveled * _speedMultiply * _tileSize;
            _material.SetVector(_id, movement);
        }
    }
}