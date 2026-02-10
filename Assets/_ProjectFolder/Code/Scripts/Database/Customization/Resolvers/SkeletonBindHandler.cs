using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D.Animation;

namespace Unity.Customization
{
    public class SkeletonBindHandler : MonoBehaviour
    {
        [SerializeField] private SkeletonAsset _skeleton;
        [SerializeField] private Transform _root;
        [SerializeField] private int _pixelPerUnit = 1024;

        [ContextMenu("Bind Skeleton")]
        private void BindSkeleton()
        {
            Dictionary<string, Transform> transforms = new();
            foreach (Transform transform in _root.GetComponentsInChildren<Transform>())
            {
                if (!transforms.ContainsKey(transform.name))
                    transforms.Add(transform.name, transform);
            }
            
            foreach (var boneData in _skeleton.GetSpriteBones())
            {
                if (string.Equals(_root.name, boneData.name)) continue;
                if (!transforms.TryGetValue(boneData.name, out Transform target))
                    continue;

                target.localPosition = boneData.position / _pixelPerUnit;
                target.localRotation = boneData.rotation;
            }
        }
    }
}