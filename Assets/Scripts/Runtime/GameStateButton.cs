using UnityEngine;
using UnityEngine.UI;

namespace CatsInCostumes {
    sealed class GameStateButton : MonoBehaviour {
        [SerializeField]
        GameState targetState;

        [SerializeField]
        Button button;

        void OnEnable() {
            button.onClick.AddListener(HandleClick);
        }

        void OnDisable() {
            button.onClick.RemoveListener(HandleClick);
        }

        void HandleClick() => GameManager.gameState = targetState;
    }
}
