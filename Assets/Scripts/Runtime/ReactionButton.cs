using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace CatsInCostumes {
    sealed class ReactionButton : MonoBehaviour, IActionMessage {
        [SerializeField]
        TextMeshProUGUI binding;
        [SerializeField]
        Button button;

        InputAction action;

        void UpdateButton() {
            binding.text = action
                .bindings
                .DefaultIfEmpty(new InputBinding("?"))
                .FirstOrDefault()
                .path
                .Split('/')[^1];
        }

        public void OnSetAction(InputAction action) {
            this.action = action;
            UpdateButton();
        }
    }
}
