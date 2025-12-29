using System.Threading.Tasks;

namespace UnityEngine.UI
{
    public class FitterVerticalLayoutGroup : MonoBehaviour
    {
        //[SerializeField] private TweenPreferredSize _preferredSize;
        [SerializeField] private VerticalLayoutGroup _layout;
        private RectTransform _transform;

        private void Awake() => _transform = transform as RectTransform;
        private void Reset() => _layout = GetComponent<VerticalLayoutGroup>();

        private async void OnTransformChildrenChanged()
        {
            float height = 0;
            _layout.enabled = true;
            await Task.Yield();

            for (int i = 0; i < _transform.childCount; i++)
                height += _transform.GetChild(i).GetComponent<RectTransform>().rect.height;

            await Task.Yield();
            _layout.enabled = false;
            //_preferredSize.ReplacePreferredHeight = height;
        }
    }
}