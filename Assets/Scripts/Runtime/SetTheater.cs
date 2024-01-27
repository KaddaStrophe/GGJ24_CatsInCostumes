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
            image.sprite = state == GameState.MainMenu
                ? mainMenuSprite
                : gameSprite;
        }
        public void OnLoadScene(string scene) {
        }
    }
}
