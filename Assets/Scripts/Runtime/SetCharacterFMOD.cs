using System.Collections;
using FMOD.Studio;
using FMODUnity;
using UnityEngine;

namespace CatsInCostumes {
    sealed class SetCharacterFMOD : MonoBehaviour, IScreenMessages {
        [SerializeField, ParamRef]
        string speakerParameter;
        PARAMETER_DESCRIPTION speakerDescription;

        [SerializeField]
        string narratorName = "NARRATOR";

        string speaker;
        bool isSetUp;

        IEnumerator Start() {
            speaker = narratorName;

            yield return new WaitUntil(() => RuntimeManager.HaveAllBanksLoaded);

            isSetUp = RuntimeManager.StudioSystem.getParameterDescriptionByName(speakerParameter, out speakerDescription) == FMOD.RESULT.OK;

            UpdateSpeaker();
        }

        public void OnSetScreen(ScreenAsset screen) {
            speaker = screen.isNarrator
                ? narratorName
                : screen.speaker;
            UpdateSpeaker();
        }

        void UpdateSpeaker() {
            if (isSetUp && !string.IsNullOrEmpty(speaker)) {
                var result = RuntimeManager.StudioSystem.setParameterByIDWithLabel(speakerDescription.id, speaker);
                Debug.Log($"Sent {speaker} to FMOD: {result}");
            }
        }
    }
}
