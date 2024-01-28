using UnityEngine;
using UnityEngine.EventSystems;

namespace CatsInCostumes {
    sealed class AdvanceInkOnClick : MonoBehaviour, IPointerClickHandler {
        public void OnPointerClick(PointerEventData eventData) {
            gameObject.scene.BroadcastMessage(nameof(IInkMessages.OnAdvanceInk));
        }
    }
}
