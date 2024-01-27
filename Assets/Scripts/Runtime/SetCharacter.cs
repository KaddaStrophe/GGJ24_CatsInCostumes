using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CatsInCostumes {
    sealed class SetCharacter : MonoBehaviour, IScreenMessages {
        [SerializeField]
        TextMeshProUGUI speaker;
        [SerializeField]
        Image portrait;

        public void OnSetScreen(ScreenAsset screen) {
            if (screen.isNarrator) {
                speaker.gameObject.SetActive(false);
                portrait.gameObject.SetActive(false);
            } else {
                speaker.text = screen.speaker;
                speaker.gameObject.SetActive(true);

                if (screen.speakerAsset.TryGetSprite(screen.mood, out var sprite)) {
                    portrait.sprite = sprite;
                    portrait.SetNativeSize();
                    portrait.gameObject.SetActive(true);
                } else {
                    portrait.gameObject.SetActive(false);
                }
            }
        }
    }
}
