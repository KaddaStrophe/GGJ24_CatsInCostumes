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
        Color speakerColor = Color.black;
        [SerializeField]
        Image portrait;

        public void OnSetScreen(ScreenAsset screen) {
            if (screen.isNarrator || screen.isTitleCard) {
                speakerBox.SetActive(false);
                portrait.gameObject.SetActive(false);
            } else {
                speaker.text = screen.speaker;
                Color.RGBToHSV(speakerColor, out float h, out float s, out float v);
                h = screen.speaker.GetHashCode() % 360 / 360f;
                speaker.color = Color.HSVToRGB(h, s, v);
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
