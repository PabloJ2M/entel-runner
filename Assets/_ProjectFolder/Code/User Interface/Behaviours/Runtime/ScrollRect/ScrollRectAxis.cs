using System;
using Unity.Mathematics;
using UnityEngine.EventSystems;

namespace UnityEngine.UI
{
    public class ScrollRectAxis : ScrollRect
    {
        private bool _routeToParent;

        private bool IsMovementOnThisAxis(PointerEventData eventData)
        {
            float2 delta = eventData.delta;

            if (horizontal && vertical) return false;
            else if (horizontal) return math.abs(delta.y) > math.abs(delta.x);
            else if (vertical) return math.abs(delta.x) > math.abs(delta.y);
            return false;
        }
        private void DoForParents<T>(Action<T> action) where T : IEventSystemHandler
        {
            Transform parent = transform.parent;
            
            while (parent != null)
            {
                foreach (var component in parent.GetComponents<Component>())
                    if (component is T handler) action(handler);

                parent = parent.parent;
            }
        }

        public override void OnInitializePotentialDrag(PointerEventData eventData)
        {
            _routeToParent = false;
            base.OnInitializePotentialDrag(eventData);
        }
        public override void OnBeginDrag(PointerEventData eventData)
        {
            if (!IsMovementOnThisAxis(eventData)) { base.OnBeginDrag(eventData); return; }

            _routeToParent = true;
            DoForParents<IInitializePotentialDragHandler>((parent) => { parent.OnInitializePotentialDrag(eventData); });
            DoForParents<IBeginDragHandler>((parent) => { parent.OnBeginDrag(eventData); });
        }
        public override void OnDrag(PointerEventData eventData)
        {
            if (!_routeToParent) base.OnDrag(eventData);
            else DoForParents<IDragHandler>((parent) => { parent.OnDrag(eventData); });
        }
        public override void OnEndDrag(PointerEventData eventData)
        {
            if (!_routeToParent) base.OnEndDrag(eventData);
            else DoForParents<IEndDragHandler>((parent) => { parent.OnEndDrag(eventData); });

            _routeToParent = false;
        }
    }
}