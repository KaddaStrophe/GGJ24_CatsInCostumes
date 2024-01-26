using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace CatsInCostumes {
    sealed class InputHandler : MonoBehaviour {
        [SerializeField]
        InputActionReference advanceAction;

        [SerializeField]
        InputActionReference[] reactions = Array.Empty<InputActionReference>();

        void OnEnable() {
            if (advanceAction.action is { } action) {
                action.performed += HandleAdvance;
                action.Enable();
            }

            for (int i = 0; i < reactions.Length; i++) {
                if (reactions[i].action is { } reaction) {
                    reaction.performed += HandleReaction;
                    reaction.Enable();
                }
            }
        }

        void OnDisable() {
            if (advanceAction.action is { } action) {
                action.performed -= HandleAdvance;
                action.Disable();
            }

            for (int i = 0; i < reactions.Length; i++) {
                if (reactions[i].action is { } reaction) {
                    reaction.performed += HandleReaction;
                    reaction.Disable();
                }
            }
        }

        void HandleAdvance(InputAction.CallbackContext context) {
            gameObject.BroadcastMessage(nameof(IInkMessages.OnAdvanceInk), SendMessageOptions.DontRequireReceiver);
        }

        void HandleReaction(InputAction.CallbackContext context) {
            string reaction = context.action.name.ToLower();
            gameObject.BroadcastMessage(nameof(IInkMessages.OnReact), reaction, SendMessageOptions.DontRequireReceiver);
        }
    }
}
