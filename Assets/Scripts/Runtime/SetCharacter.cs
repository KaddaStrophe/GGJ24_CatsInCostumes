using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CatsInCostumes {
    sealed class SetCharacter : MonoBehaviour, IScreenMessages {
        [SerializeField]
        GameObject speakerBox;
        [SerializeField]
        TextMeshProUGUI speaker;
        [SerializeField]
        Image portrait;

        public void OnSetScreen(ScreenAsset screen) {
            if (screen.isNarrator) {
                speakerBox.SetActive(false);
                portrait.gameObject.SetActive(false);
            } else {
                speaker.text = screen.speaker;
                speakerBox.SetActive(true);
                speakerBox.BroadcastMessage(nameof(ContentSizeFitter.SetLayoutHorizontal), SendMessageOptions.DontRequireReceiver);

                if (screen.TryGetSpeakerPortrait(out var sprite)) {
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
