using FMODUnity;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace CatsInCostumes {
    sealed class ButtonSFX : MonoBehaviour {

        [SerializeField]
        Button button;

        [Space]
        [SerializeField]
        EventReference onSelect = new();
        [SerializeField]
        EventReference onDeselect = new();
        [SerializeField]
        EventReference onClick = new();

        bool _isSelected;
        bool isSelected {
            get => _isSelected;
            set {
                if (_isSelected != value) {
                    _isSelected = value;
                    if (value) {
                        onSelect.PlayOneShot();
                    } else {
                        onDeselect.PlayOneShot();
                    }
                }
            }
        }

        void Update() {
            isSelected = EventSystem.current is { currentSelectedGameObject: var obj } && obj == gameObject;
        }

        void OnEnable() {
            button.onClick.AddListener(HandleClick);
        }

        void OnDisable() {
            button.onClick.RemoveListener(HandleClick);
        }

        void HandleClick() => onClick.PlayOneShot();
    }
}
