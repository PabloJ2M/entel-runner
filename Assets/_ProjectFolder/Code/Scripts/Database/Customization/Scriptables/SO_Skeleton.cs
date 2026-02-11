using System;
using UnityEngine;
using UnityEngine.Rendering;

namespace Unity.Customization
{
    [Serializable]
    public struct BoneData
    {
        public Vector2 position;
        public Quaternion rotation;

        public BoneData(Vector2 position, Quaternion rotation)
        {
            this.position = position;
            this.rotation = rotation;
        }
    }

    [CreateAssetMenu(fileName = "skeleton", menuName = "system/customization/skeleton")]
    public class SO_Skeleton : ScriptableObject
    {
        [SerializeField] private SerializedDictionary<string, BoneData> _bones;

        public void SetBonesPositionAndRotation(Transform[] bones)
        {
            foreach (var bone in bones) {
                if (_bones.TryGetValue(bone.name, out BoneData data)) {
                    bone.localPosition = data.position;
                    bone.localRotation = data.rotation;
                }
            }
        }
        public void GetBonesData(Transform[] bones)
        {
            foreach (var bone in bones)
                _bones[bone.name] = new(bone.localPosition, bone.localRotation);
        }
    }
}