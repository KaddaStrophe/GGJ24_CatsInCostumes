using FMODUnity;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace CatsInCostumes {
    sealed class ButtonSFX : MonoBehaviour, IPointerEnterHandler {

        [SerializeField]
        Button button;

        [Space]
        [SerializeField]
        EventReference onSelect = new();
        [SerializeField]
        EventReference onHover = new();
        [SerializeField]
        EventReference onDeselect = new();
        [SerializeField]
        EventReference onClick = new();

        bool _isSelected;
        bool isSelected {
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
            _isSelected = false;
            button.onClick.AddListener(HandleClick);
        }

        void OnDisable() {
            _isSelected = false;
            button.onClick.RemoveListener(HandleClick);
        }

        void HandleClick() => onClick.PlayOneShot();

        public void OnPointerEnter(PointerEventData eventData) {
            onHover.PlayOneShot();
        }
    }
}
