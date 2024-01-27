using System;
using FMODUnity;
using UnityEngine;
using UnityEngine.InputSystem;

namespace CatsInCostumes {
    sealed class PlayFMOD : MonoBehaviour, IInkMessages {
        [Serializable]
        struct ReactionReference {
            [SerializeField]
            InputActionReference action;
            [SerializeField]
            EventReference reference;

            public void Deconstruct(out InputActionReference action, out EventReference reference) {
                action = this.action;
                reference = this.reference;
            }
        }

        [SerializeField]
        EventReference wrongStateEvent = new();

        [SerializeField]
        ReactionReference[] reactionEvents = Array.Empty<ReactionReference>();

        bool TryGetReference(string reaction, out EventReference reference) {
            foreach (var (key, value) in reactionEvents) {
                if (key.action.name.Equals(reaction, StringComparison.OrdinalIgnoreCase)) {
                    reference = value;
                    return !reference.IsNull;
                }
            }

            reference = default;
            return false;
        }

        public void OnSetInk(TextAsset ink) {
        }
        public void OnAdvanceInk() {
        }
        public void OnReact(string reaction) {
            if (!TryGetReference(reaction, out var reference)) {
                Debug.LogWarning($"Failed to find event for reaction '{reaction}'!");
                return;
            }

            RuntimeManager.PlayOneShot(reference);

            if (GameManager.gameState == GameState.PlayingDialog) {
                RuntimeManager.PlayOneShot(wrongStateEvent);
            }
        }
    }
}
