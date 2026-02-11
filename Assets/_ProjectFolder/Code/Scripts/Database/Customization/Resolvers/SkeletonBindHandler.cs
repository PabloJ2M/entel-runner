using UnityEngine;

namespace Unity.Customization
{
    public class SkeletonBindHandler : MonoBehaviour
    {
        //[SerializeField] private SO_LibraryReference_List _reference;
        [SerializeField] private SO_Skeleton _selected;

        [Header("Bones")]
        [SerializeField] private Transform _root;
        [SerializeField] private Transform[] _bones;

        private void Awake() => GetAllBones();
        //private void OnEnable() => _reference.onPreviewUpdated += PerformeLibrary;
        //private void OnDisable() => _reference.onPreviewUpdated -= PerformeLibrary;

        //private void PerformeLibrary(SO_LibraryReference reference)
        //{
        //    _selected = reference.Skeleton;
        //    SetSkeleton();
        //}

        [ContextMenu("Get Bones")]
        private void GetAllBones() => _bones = _root.GetComponentsInChildren<Transform>();

        [ContextMenu("Set Skeleton")]
        private void SetSkeleton() => _selected.SetBonesPositionAndRotation(_bones);

        [ContextMenu("Get Skeleton Data")]
        private void GetSkeletonData() => _selected.GetBonesData(_bones);
    }
}