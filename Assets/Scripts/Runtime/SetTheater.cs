using UnityEngine;
using UnityEngine.UI;

namespace CatsInCostumes {
    sealed class SetTheater : MonoBehaviour, IGameMessages {
        [SerializeField]
        Image image;
        [SerializeField]
        Sprite mainMenuSprite;
        [SerializeField]
        Sprite gameSprite;

        void Start() {
            OnSetState(GameState.MainMenu);
        }

        public void OnSetState(GameState state) {
            image.sprite = state switch {
                GameState.MainMenu => mainMenuSprite,
                GameState.PlayingDialog => gameSprite,
                GameState.WaitingForReaction => gameSprite,
                GameState.WaitingForScene => mainMenuSprite,
                _ => throw new System.NotImplementedException(),
            };
        }
        public void OnLoadScene(string scene) {
        }
    }
}
