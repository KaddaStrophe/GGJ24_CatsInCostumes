using UnityEngine;

namespace CatsInCostumes {
    [CreateAssetMenu]
    sealed class ScreenAsset : ScriptableObject {
        [SerializeField]
        internal string background;

        [SerializeField]
        internal string speaker;

        [SerializeField]
        internal string mood;

        [SerializeField]
        internal string speech;
    }
}
