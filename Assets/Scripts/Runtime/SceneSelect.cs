using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CatsInCostumes {
    sealed class SceneSelect : MonoBehaviour {
        [SerializeField]
        GameObject buttonPrefab;

        readonly List<GameObject> buttonInstances = new();

        void OnEnable() {
            StartCoroutine(Start_Co());
        }

        IEnumerator Start_Co() {
            yield return GameManager.waitUntilReady;

            foreach (var action in InputHandler.sceneActions) {
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
