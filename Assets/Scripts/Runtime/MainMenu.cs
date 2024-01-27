using UnityEngine;

namespace CatsInCostumes {
    sealed class MainMenu : MonoBehaviour {

        public GameObject creditsUI;
        public GameObject selectUI;

        public void HandleStartButtonPressed() {
            UnityEngine.SceneManagement.SceneManager.LoadScene("SampleScene");
        }

        public void HandleCreditsButtonPressed() {
            Debug.Log(creditsUI.activeSelf);
            Debug.Log(selectUI.activeSelf);
            creditsUI.SetActive(!creditsUI.activeSelf);
            selectUI.SetActive(!selectUI.activeSelf);
        }

        public void HandleQuitButtonPressed() {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.ExitPlaymode();
#else
            Application.Quit();
#endif
        }
    }
}
