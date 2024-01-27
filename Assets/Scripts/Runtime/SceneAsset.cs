using UnityEngine;
using UnityEngine.Serialization;

namespace CatsInCostumes {
    [CreateAssetMenu]
    sealed class ScreenAsset : ScriptableObject {
        [SerializeField]
        internal string background;

        [SerializeField, FormerlySerializedAs(nameof(speaker))]
        string speakerId;
        [SerializeField]
        CharacterAsset speakerAsset;

        internal string speaker {
            get => speakerId;
            set {
                if (speakerId != value) {
                    speakerId = value;
                    speakerAsset = CharacterAsset.GetAssetById(value);
                }
            }
        }

        [SerializeField]
        internal Mood mood;

        [SerializeField]
        internal string speech;

        internal bool isNarrator => CharacterAsset.IsNarrator(speaker);

        internal bool TryGetSpeakerPortrait(out Sprite sprite) {
            if (speakerAsset) {
                return speakerAsset.TryGetSprite(mood, out sprite);
            }

            sprite = default;
            return false;
        }
    }
}
