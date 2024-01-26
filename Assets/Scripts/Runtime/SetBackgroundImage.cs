using Slothsoft.UnityExtensions;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.UI;

namespace CatsInCostumes {
    sealed class SetBackgroundImage : MonoBehaviour, IScreenMessages {
        [SerializeField]
        Image image;
        [SerializeField, Expandable]
        ScreenAsset screen;

        void Start() {
            UpdateScreen();
        }

        void UpdateScreen() {
            if (screen) {
                Addressables.LoadAssetAsync<Sprite>(screen.background).Completed += SetImage;
            }
        }

        void SetImage(AsyncOperationHandle<Sprite> handle) {
            image.sprite = handle.Result;
        }

        public void OnSetScreen(ScreenAsset screen) {
            this.screen = screen;
            UpdateScreen();
        }
    }
}
