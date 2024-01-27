﻿using System.Linq;
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

        void OnEnable() {
            button.onClick.AddListener(Invoke);
        }

        void OnDisable() {
            button.onClick.RemoveListener(Invoke);
        }

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
        }

        public void OnSetAction(InputAction action) {
            this.action = action;
            UpdateButton();
        }
    }
}
