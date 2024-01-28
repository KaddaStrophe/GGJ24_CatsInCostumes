using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace CatsInCostumes {
    sealed class EscapeHandler : MonoBehaviour, IInkMessages {
        [SerializeField]
        InputActionReference escapeAction;

        public void OnSetInk(TextAsset ink) { }
        public void OnAdvanceInk() { }
        public void OnReact(string reaction) {
            if (!escapeAction.action.name.Equals(reaction, StringComparison.OrdinalIgnoreCase)) {
                return;
            }

            if (GameManager.gameState is GameState.MainMenu or GameState.Exit) {
                GameManager.gameState = GameState.Exit;
                return;
            }

            GameManager.gameState = GameState.MainMenu;
        }
    }
}
