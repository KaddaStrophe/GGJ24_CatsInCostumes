using FMODUnity;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace CatsInCostumes {
    static class Extensions {
        internal static void BroadcastMessage(this Scene scene, string message) {
            var objects = scene.GetRootGameObjects();
            for (int i = 0; i < objects.Length; i++) {
                objects[i].BroadcastMessage(message, SendMessageOptions.DontRequireReceiver);
            }
        }

        internal static void BroadcastMessage(this Scene scene, string message, object context) {
            var objects = scene.GetRootGameObjects();
            for (int i = 0; i < objects.Length; i++) {
                objects[i].BroadcastMessage(message, context, SendMessageOptions.DontRequireReceiver);
            }
        }

        internal static void SelectFirstSelectable(this GameObject obj) {
            var selectable = obj.GetComponentInChildren<Selectable>();
            if (selectable) {
                selectable.Select();
            }
        }

        internal static void PlayOneShot(this in EventReference reference) {
            if (!reference.IsNull) {
                RuntimeManager.PlayOneShot(reference);
            }
        }
    }
}
