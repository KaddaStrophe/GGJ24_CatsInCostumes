using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CatsInCostumes {
    sealed class Toolbar : MonoBehaviour {
        [SerializeField]
        GameObject buttonPrefab;

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
    }
}
