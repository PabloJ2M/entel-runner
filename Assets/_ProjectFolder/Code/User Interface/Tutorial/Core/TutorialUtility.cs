using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Unity.Tutorial
{
    [Flags] public enum TutorialType
    {
        None = 0,
        DisplayArrow = 1,
        ScreenFocusObject = 2,
        WaitForInteraction = 4
    }

    public interface IElement
    {
        Sprite Shape { get; }
        Rect Rect { get; }
        Vector2 Position { get; }
        Vector2 Pivot { get; }
    }

    [Serializable] public struct RectElement : IElement
    {
        [SerializeField] private RectTransform _rectTransform;
        [SerializeField] private Sprite _shape;

        public RectElement(Transform transform, Image image)
        {
            _rectTransform = transform as RectTransform;
            _shape = image?.sprite;
        }

        public Sprite Shape => _shape;
        public Rect Rect => _rectTransform.rect;
        public Vector2 Position => _rectTransform.position;
        public Vector2 Pivot => _rectTransform.pivot;
    }
    [Serializable] public struct Object2DElement : IElement
    {
        [SerializeField] private Camera _camera;
        [SerializeField] private Transform _transform;
        [SerializeField] private Sprite _shape;

        public Object2DElement(Camera camera, Transform transform, SpriteRenderer render)
        {
            _camera = camera;
            _transform = transform;
            _shape = render.sprite;
        }

        public Sprite Shape => _shape;
        public Rect Rect => _shape.rect;
        public Vector2 Position => _camera.WorldToScreenPoint(_transform.position);
        public Vector2 Pivot => _shape.pivot;
    }
    [Serializable] public struct Object3DElement : IElement
    {
        [SerializeField] private Camera _camera;
        [SerializeField] private Transform _transform;
        [SerializeField] private Sprite _shape;

        public Object3DElement(Camera camera, Transform transform, Sprite render)
        {
            _camera = camera;
            _transform = transform;
            _shape = render;
        }

        public Sprite Shape => _shape;
        public Rect Rect => _shape.rect;
        public Vector2 Position => _camera.WorldToScreenPoint(_transform.position);
        public Vector2 Pivot => _shape.pivot;
    }

    [Serializable] public struct Dialogue
    {
        public Sprite image;
        public string text;
    }
    [Serializable] public struct DialogueUI
    {
        public Image image;
        public TextMeshProUGUI text;
    }
}