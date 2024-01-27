using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

namespace CatsInCostumes {
    sealed class InputHandler : MonoBehaviour {
        [SerializeField]
        InputActionReference advanceAction;

        [SerializeField]
        internal InputActionReference[] reactions = Array.Empty<InputActionReference>();

        internal static InputHandler instance { get; private set; }

        internal static IEnumerable<InputAction> reactionActions => instance
            ? instance.reactions.Select(r => r.action)
            : Enumerable.Empty<InputAction>();

        void Awake() {
            instance = this;
        }

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
            gameObject.scene.BroadcastMessage(nameof(IInkMessages.OnAdvanceInk));
        }

        void HandleReaction(InputAction.CallbackContext context) {
            string reaction = context.action.name;
            gameObject.scene.BroadcastMessage(nameof(IInkMessages.OnReact), reaction);
        }
    }
}
