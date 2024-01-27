using FMODUnity;
using UnityEngine;

namespace CatsInCostumes {
    sealed class PlayFMOD : MonoBehaviour, IInkMessages {
        public void OnSetInk(TextAsset ink) {
        }
        public void OnAdvanceInk() {
        }
        public void OnReact(string reaction) {
            var eve = EventReference.Find($"event:/reaction/{reaction}");

            RuntimeManager.PlayOneShot(eve);
        }
    }
}
