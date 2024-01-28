using UnityEngine;

namespace CatsInCostumes {
    sealed class HideInWebGL : MonoBehaviour {
#if UNITY_WEBGL
        void Awake() {
            gameObject.SetActive(false);
        }
#endif
    }
}
