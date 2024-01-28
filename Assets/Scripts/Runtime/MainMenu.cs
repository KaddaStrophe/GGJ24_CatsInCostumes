using System.Collections;
using FMODUnity;
using UnityEngine;

namespace CatsInCostumes {
    sealed class MainMenu : MonoBehaviour, IGameMessages {
        [SerializeField]
        string firstScene;
        [Space]
        [SerializeField]
        GameObject selectUI;
        [SerializeField]
        GameObject creditsUI;

        IEnumerator Start() {
            selectUI.SetActive(false);
            creditsUI.SetActive(false);

            yield return GameManager.waitUntilReady;

            yield return null;
            yield return null;
            yield return null;

            OnSetState(GameManager.gameState);
        }

        public void OnSetState(GameState state) {
            selectUI.SetActive(state == GameState.MainMenu);
            if (selectUI.activeSelf) {
                selectUI.SelectFirstSelectable();
            }

            creditsUI.SetActive(state == GameState.Credits);
            if (creditsUI.activeSelf) {
                creditsUI.SelectFirstSelectable();
            }

            if (state == GameState.Exit) {
                if (exitSound.IsNull) {
                    HandleQuitButtonPressed();
                } else {
                    Invoke(nameof(HandleQuitButtonPressed), exitDelay);
                    exitSound.PlayOneShot();
                }
            }
        }

        [Space]
        [SerializeField]
        EventReference exitSound = new();
        [SerializeField]
        float exitDelay = 2;

        void HandleQuitButtonPressed() {
            GameManager.Quit();
        }
    }
}
