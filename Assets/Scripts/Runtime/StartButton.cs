using System.Linq;
using System.Text.RegularExpressions;
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

        [Space]
        [SerializeField]
        string labelProlog = "Prolog";
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

            string binding = action
                .bindings
                .DefaultIfEmpty(new InputBinding("f?"))
                .FirstOrDefault()
                .path
                .Split('/')[^1];

            int actNumber = int.Parse(Regex.Replace(binding, "[^0-9]+", ""));

            actNumber--;

            label.text = actNumber == 0
                ? labelProlog
                : $"{labelPrefix}{actNumber}{labelSuffix}";
        }
    }
}
