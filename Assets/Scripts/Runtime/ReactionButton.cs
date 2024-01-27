using System;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace CatsInCostumes {
    sealed class ReactionButton : MonoBehaviour, IActionMessage, IInkMessages, IPointerClickHandler {
        [SerializeField]
        TextMeshProUGUI binding;
        [SerializeField]
        Image buttonImage;
        [SerializeField]
        Image iconImage;

        InputAction action;

        void Invoke() {
            string reaction = action.name;
            gameObject.scene.BroadcastMessage(nameof(IInkMessages.OnReact), reaction);
        }

        void UpdateButton() {
            binding.text = action
                .bindings
                .DefaultIfEmpty(new InputBinding("?"))
                .FirstOrDefault()
                .path
                .Split('/')[^1];

            if (GameManager.TryGetIcon(action.name, out var sprite)) {
                iconImage.sprite = sprite;
                iconImage.SetNativeSize();
                iconImage.enabled = true;
            } else {
                iconImage.enabled = false;
            }
        }

        public void OnSetAction(InputAction action) {
            this.action = action;
            UpdateButton();
        }

        public void OnSetInk(TextAsset ink) { }
        public void OnAdvanceInk() { }
        public void OnReact(string reaction) {
            if (action.name.Equals(reaction, StringComparison.OrdinalIgnoreCase)) {
                buttonImage.color = highlightColor;
                Invoke(nameof(ClearColor), colorDelay);
            }
        }

        [SerializeField]
        float colorDelay = 1;
        [SerializeField]
        Color highlightColor = Color.green;

        void ClearColor() {
            buttonImage.color = Color.white;
        }

        public void OnPointerClick(PointerEventData eventData) {
            Invoke();
        }
    }
}
