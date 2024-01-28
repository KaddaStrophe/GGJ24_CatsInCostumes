using UnityEngine;
using UnityEngine.Serialization;

namespace CatsInCostumes {
    [CreateAssetMenu]
    sealed class ScreenAsset : ScriptableObject {
        static ScreenAsset m_empty;
        internal static ScreenAsset empty {
            get {
                if (!m_empty) {
                    m_empty = CreateInstance<ScreenAsset>();
                }

                return m_empty;
            }
        }

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

        [SerializeField]
        internal string nextScene;
        [SerializeField]
        internal bool isMeowing;
        [SerializeField]
        internal string title;

        internal bool isTitleCard => !string.IsNullOrEmpty(title);

        internal bool TryGetNextScene(out TextAsset scene) {
            scene = default;
            return !string.IsNullOrEmpty(nextScene)
                && GameManager.TryGetStory(nextScene, out scene);
        }
    }
}
