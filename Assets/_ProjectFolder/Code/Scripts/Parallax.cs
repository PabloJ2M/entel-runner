using Unity.Mathematics;
using UnityEngine;
//using System;

namespace Environment
{
    public class Parallax : MonoBehaviour
    {
        //public enum AdOrientation { Horizontal, Vertical }

        //[Serializable]
        //public class AdChildData
        //{
        //    public SpriteRenderer adRenderer;
        //    public AdOrientation orientation;
        //}

        [SerializeField] private float _speedMultiply = 1f;
        [SerializeField] private float2 _direction = float2.zero;

        //[Header("Anuncios")]
        //[SerializeField] private AdChildData[] _adChildren;
        //[SerializeField] private Sprite[] _horizontalOptions;
        //[SerializeField] private Sprite[] _verticalOptions;

        private const string _id = "_TextureOffset";

        private Material _material;
        private float2 _tileSize;
        //private float[] _initialLocalPositionsX;
        //private Vector2[] _targetWorldSizes;
        //private float _initialOffsetX;
        //private float _spriteWidth;

        private void Start()
        {
            SpriteRenderer render = GetComponent<SpriteRenderer>();

            //_spriteWidth = render.sprite.rect.width / render.sprite.pixelsPerUnit;

            float2 units = render.sprite.rect.size / render.sprite.pixelsPerUnit;
            _tileSize = _direction * 0.02f * ((float2)render.size / units);
            _material = render.material;

            //if (_adChildren == null || _adChildren.Length == 0) return;

            //_initialLocalPositionsX = new float[_adChildren.Length];
            //_targetWorldSizes = new Vector2[_adChildren.Length];

            //for (int i = 0; i < _adChildren.Length; i++)
            //{
            //    if (_adChildren[i].adRenderer == null) continue;

            //    Transform t = _adChildren[i].adRenderer.transform;

            //    _initialLocalPositionsX[i] = t.localPosition.x;
            //    _targetWorldSizes[i] =
            //        (Vector2)_adChildren[i].adRenderer.sprite.bounds.size *
            //        (Vector2)t.localScale;

            //    MatchAdScale(i);
            //}

            //_initialOffsetX = _material.GetVector(OffsetId).x;
        }

        private void OnEnable() => GameplayManager.Instance.onDinstanceTraveled += AddSpeed;
        private void OnDisable() => GameplayManager.Instance.onDinstanceTraveled -= AddSpeed;
        private void AddSpeedConstant(float amount) => AddSpeed(amount * Time.deltaTime);

        public void AddSpeed(float amount)
        {
            Vector2 movement = amount * _speedMultiply * _tileSize;
            //if (movement == Vector2.zero) return;

            //Vector2 currentOffset = (Vector2)_material.GetVector(_id) + movement;
            _material.SetVector(_id, movement);

            //float deltaOffset = currentOffset.x - _initialOffsetX;
            //float unitsMoved = deltaOffset * _spriteWidth;

            //for (int i = 0; i < _adChildren.Length; i++)
            //{
            //    SpriteRenderer sr = _adChildren[i].adRenderer;
            //    if (sr == null) continue;

            //    float wrappedX =
            //        Mathf.Repeat(unitsMoved + (_spriteWidth * 0.5f), _spriteWidth) -
            //        (_spriteWidth * 0.5f);

            //    float targetX = _initialLocalPositionsX[i] - wrappedX;
            //    Vector3 localPos = sr.transform.localPosition;

            //    if (Mathf.Abs(targetX - localPos.x) > _spriteWidth * 0.5f)
            //    {
            //        UpdateAdSprite(i);
            //    }

            //    sr.transform.localPosition =
            //        new Vector3(targetX, localPos.y, localPos.z);
            //}
        }

        //private void UpdateAdSprite(int index)
        //{
        //    Sprite[] pool =
        //        _adChildren[index].orientation == AdOrientation.Vertical
        //        ? _verticalOptions
        //        : _horizontalOptions;

        //    if (pool == null || pool.Length == 0) return;

        //    _adChildren[index].adRenderer.sprite =
        //        pool[UnityEngine.Random.Range(0, pool.Length)];

        //    MatchAdScale(index);
        //}

        //private void MatchAdScale(int index)
        //{
        //    SpriteRenderer sr = _adChildren[index].adRenderer;
        //    if (sr.sprite == null) return;

        //    Vector2 spriteSize = sr.sprite.bounds.size;

        //    sr.transform.localScale = new Vector3(
        //        _targetWorldSizes[index].x / spriteSize.x,
        //        _targetWorldSizes[index].y / spriteSize.y,
        //        1f
        //    );
        //}
    }
}
