using System;
using System.Linq;
using FMODUnity;
using UnityEngine;

namespace CatsInCostumes {
    sealed class PlayFMOD : MonoBehaviour, IInkMessages {
        [SerializeField]
        EventReference[] reactionEvents = Array.Empty<EventReference>();

        public void OnSetInk(TextAsset ink) {
        }
        public void OnAdvanceInk() {
        }
        public void OnReact(string reaction) {
            var reference = reactionEvents
                .FirstOrDefault(r => r.Path.Split('/')[^1].Equals(reaction, StringComparison.OrdinalIgnoreCase));

            if (reference.IsNull) {
                Debug.LogWarning($"Failed to find event for reaction '{reaction}'!");
                return;
            }

            RuntimeManager.PlayOneShot(reference);
        }
    }
}
