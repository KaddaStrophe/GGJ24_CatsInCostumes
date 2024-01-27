using System.Collections;
using Slothsoft.UnityExtensions;
using UnityEngine;
using UnityEngine.UI;

namespace CatsInCostumes {
    sealed class SetBackgroundImage : MonoBehaviour, IScreenMessages {
        [SerializeField, Expandable]
        ScreenAsset screen;
        [SerializeField]
        string label;
        [SerializeField]
        Image image;

        IEnumerator Start() {
            yield return GameManager.waitUntilReady;

            UpdateScreen();
        }

        void UpdateScreen() {
            if (screen) {
                SetImage(screen.background);
            }
        }

        void SetImage(string background) {
            if (GameManager.TryGetBackground(background, out var sprite)) {
                image.sprite = sprite;
                image.enabled = true;
            } else {
                image.enabled = false;
            }
        }

        public void OnSetScreen(ScreenAsset screen) {
            this.screen = screen;
            UpdateScreen();
        }
    }
}
