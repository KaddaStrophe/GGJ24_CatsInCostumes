using System.Collections;
using Slothsoft.UnityExtensions;
using TMPro;
using UnityEngine;

namespace CatsInCostumes {
    sealed class SetDialogText : MonoBehaviour, IScreenMessages {
        [SerializeField, Expandable]
        ScreenAsset screen;
        [SerializeField]
        GameObject speechBox;
        [SerializeField]
        TextMeshProUGUI speech;

        void Start() {
            UpdateScreen();
        }

        Coroutine textRoutine;

        void UpdateScreen() {
            if (screen) {
                if (textRoutine is not null) {
                    StopCoroutine(textRoutine);
                    textRoutine = null;
                }

                speech.text = screen.isNarrator
                    ? screen.speech
                    : $"\"{screen.speech}\"";
                if (string.IsNullOrEmpty(screen.speech)) {
                    speechBox.SetActive(false);
                } else {
                    speechBox.SetActive(true);
                    textRoutine = StartCoroutine(UpdateText_Co());
                }
            }
        }

        [SerializeField]
        float letterDelay = 0.01f;

        IEnumerator UpdateText_Co() {
            screen.isMeowing = true;

            string text = speech.text;

            for (int i = 0; i < text.Length; i++) {
                speech.maxVisibleCharacters = i;

                if (char.IsLetter(text[i])) {
                    yield return Wait.forSeconds[letterDelay];
                }

                if (!screen.isMeowing) {
                    break;
                }
            }

            speech.maxVisibleCharacters = text.Length;

            screen.isMeowing = false;

            textRoutine = null;
        }

        public void OnSetScreen(ScreenAsset screen) {
            this.screen = screen;
            UpdateScreen();
        }
    }
}
