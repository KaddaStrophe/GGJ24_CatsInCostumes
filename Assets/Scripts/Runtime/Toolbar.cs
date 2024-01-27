using System.Collections;
using System.Collections.Generic;
using Slothsoft.UnityExtensions;
using UnityEngine;

namespace CatsInCostumes {
    sealed class Toolbar : MonoBehaviour, IGameMessages {
        [SerializeField]
        GameObject buttonPrefab;

        [Space]
        [SerializeField]
        CanvasGroup group;
        [SerializeField]
        SerializableKeyValuePairs<GameState, float> alphaByState = new();

        readonly List<GameObject> buttonInstances = new();

        IEnumerator Start() {
            yield return GameManager.waitUntilReady;

            foreach (var action in InputHandler.reactionActions) {
                var button = Instantiate(buttonPrefab, transform);
                button.BroadcastMessage(nameof(IActionMessage.OnSetAction), action, SendMessageOptions.DontRequireReceiver);
                buttonInstances.Add(button);
            }
        }

        void OnDisable() {
            foreach (var button in buttonInstances) {
                Destroy(button);
            }

            buttonInstances.Clear();
        }

        public void OnSetState(GameState state) {
            group.alpha = alphaByState[state];
        }
        public void OnLoadScene(string scene) {
        }
    }
}
