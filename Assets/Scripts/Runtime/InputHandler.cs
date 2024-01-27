using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

namespace CatsInCostumes {
    sealed class InputHandler : MonoBehaviour {

        internal static InputHandler instance { get; private set; }

        [SerializeField]
        InputActionReference escapeAction;
        [SerializeField]
        InputActionReference advanceAction;

        [SerializeField]
        internal InputActionReference[] reactions = Array.Empty<InputActionReference>();

        internal static IEnumerable<InputAction> reactionActions => instance
            ? instance.reactions.Select(r => r.action)
            : Enumerable.Empty<InputAction>();

        [SerializeField]
        internal InputActionReference[] scenes = Array.Empty<InputActionReference>();

        internal static IEnumerable<InputAction> sceneActions => instance
            ? instance.scenes.Select(r => r.action).Where(a => GameManager.TryGetStory(a.name, out _))
            : Enumerable.Empty<InputAction>();

        void Awake() {
            instance = this;
        }

        void OnEnable() {
            escapeAction.action.performed += HandleEscape;
            escapeAction.action.Enable();

            advanceAction.action.performed += HandleAdvance;
            advanceAction.action.Enable();

            for (int i = 0; i < reactions.Length; i++) {
                if (reactions[i].action is { } reaction) {
                    reaction.performed += HandleReaction;
                    reaction.Enable();
                }
            }

            for (int i = 0; i < scenes.Length; i++) {
                if (scenes[i].action is { } scene) {
                    scene.performed += HandleScene;
                    scene.Enable();
                }
            }
        }

        void OnDisable() {
            escapeAction.action.performed += HandleEscape;
            escapeAction.action.Disable();

            advanceAction.action.performed -= HandleAdvance;
            advanceAction.action.Disable();

            for (int i = 0; i < reactions.Length; i++) {
                if (reactions[i].action is { } reaction) {
                    reaction.performed += HandleReaction;
                    reaction.Disable();
                }
            }

            for (int i = 0; i < scenes.Length; i++) {
                if (scenes[i].action is { } scene) {
                    scene.performed += HandleScene;
                    scene.Disable();
                }
            }
        }

        void HandleEscape(InputAction.CallbackContext context) {
            if (GameManager.gameState == GameState.MainMenu) {
                GameManager.Quit();
                return;
            }

            GameManager.gameState = GameState.MainMenu;
        }

        void HandleAdvance(InputAction.CallbackContext context) {
            gameObject.scene.BroadcastMessage(nameof(IInkMessages.OnAdvanceInk));
        }

        void HandleReaction(InputAction.CallbackContext context) {
            string reaction = context.action.name;
            gameObject.scene.BroadcastMessage(nameof(IInkMessages.OnReact), reaction);
        }

        void HandleScene(InputAction.CallbackContext context) {
            string scene = context.action.name;
            GameManager.LoadScene(scene);
        }
    }
}
