using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace Unity.Tutorial
{
    [RequireComponent(typeof(Image))]
    public class TutorialInteract : TutorialHandlerRect, ISubmitHandler, IPointerClickHandler
    {
        public void OnSubmit(BaseEventData eventData) => HandleInteraction();
        public void OnPointerClick(PointerEventData eventData) => HandleInteraction();
    }
}