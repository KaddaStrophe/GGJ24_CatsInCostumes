using UnityEngine;
using UnityEngine.SceneManagement;

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
    }
}
