using TMPro;
using UnityEngine;

namespace CatsInCostumes {
    sealed class SetTitleCard : MonoBehaviour, IScreenMessages {
        [SerializeField]
        TextMeshProUGUI title;
        [SerializeField]
        TextMeshProUGUI subtitle;

        public void OnSetScreen(ScreenAsset screen) {
            if (screen.isTitleCard) {
                title.text = screen.title;
                subtitle.text = screen.speech;
            } else {
                title.text = "";
                subtitle.text = "";
            }
        }
    }
}
