using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace CatsInCostumes {
    sealed class StartButton : MonoBehaviour, IActionMessage {
        [SerializeField]
        TextMeshProUGUI label;
        [SerializeField]
        Button button;
        [SerializeField]
        string sceneToLoad;
        [SerializeField]
        string labelPrefix = "Act ";
        [SerializeField]
        string labelSuffix = "";

        void OnEnable() {
            button.onClick.AddListener(HandleClick);
        }
        void OnDisable() {
            button.onClick.RemoveListener(HandleClick);
        }

        void HandleClick() {
            GameManager.LoadScene(sceneToLoad);
        }

        public void OnSetAction(InputAction action) {
            sceneToLoad = action.name;
            label.text = action
                .bindings
                .DefaultIfEmpty(new InputBinding("f?"))
                .FirstOrDefault()
                .path
                .Split('/')[^1]
                .Replace("f", labelPrefix) + labelSuffix;
        }
    }
}
