using System.Collections;
using UnityEngine;

namespace CatsInCostumes {
    sealed class MainMenu : MonoBehaviour, IGameMessages {
        [SerializeField]
        string firstScene;
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

        public void HandleStartButtonPressed() {
            if (GameManager.TryGetStory(firstScene, out var story)) {
                gameObject.scene.BroadcastMessage(nameof(IInkMessages.OnSetInk), story);
            }
        }

        public void HandleCreditsButtonPressed() {
            creditsUI.SetActive(!creditsUI.activeSelf);
            selectUI.SetActive(!selectUI.activeSelf);

            if (creditsUI.activeSelf) {
                creditsUI.SelectFirstSelectable();
            } else {
                selectUI.SelectFirstSelectable();
            }
        }

        public void HandleQuitButtonPressed() {
            GameManager.Quit();
        }

        public void OnSetState(GameState state) {
            selectUI.SetActive(state == GameState.MainMenu);
            if (selectUI.activeSelf) {
                selectUI.SelectFirstSelectable();
            }
        }
    }
}
