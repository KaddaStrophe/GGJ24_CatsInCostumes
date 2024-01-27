using Slothsoft.UnityExtensions;
using TMPro;
using UnityEngine;

namespace CatsInCostumes {
    sealed class SetDialogText : MonoBehaviour, IScreenMessages {
        [SerializeField, Expandable]
        ScreenAsset screen;
        [SerializeField]
        TextMeshProUGUI speech;

        void Start() {
            UpdateScreen();
        }

        void UpdateScreen() {
            if (screen) {
                speech.text = screen.isNarrator
                    ? screen.speech
                    : $"\"{screen.speech}\"";
                speech.gameObject.SetActive(!string.IsNullOrEmpty(screen.speech));
            }
        }

        public void OnSetScreen(ScreenAsset screen) {
            this.screen = screen;
            UpdateScreen();
        }
    }
}
