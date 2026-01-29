namespace UnityEngine.Events
{
    public class PositionConstrainUI : MonoBehaviour
    {
        [SerializeField] private Camera _camera;
        [SerializeField] private RectTransform _target;

        private void Start() => transform.position = _camera.ScreenToWorldPoint(_target.position);
    }
}