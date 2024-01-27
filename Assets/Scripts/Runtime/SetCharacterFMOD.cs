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
        [SerializeField]
        EventReference meowingReference = new();

        ScreenAsset screen;
        string speaker => !screen || screen.isNarrator
            ? narratorName
            : screen.speaker;

        bool isSetUp;

        IEnumerator Start() {
            yield return new WaitUntil(() => RuntimeManager.HaveAllBanksLoaded);

            isSetUp = RuntimeManager.StudioSystem.getParameterDescriptionByName(speakerParameter, out speakerDescription) == FMOD.RESULT.OK;

            meowingInstance = RuntimeManager.CreateInstance(meowingReference.Guid);

            UpdateSpeaker();
        }

        bool shouldBeMeowing => screen && screen.isMeowing;
        bool isMeowing;
        EventInstance meowingInstance;

        void Update() {
            if (!meowingInstance.isValid()) {
                return;
            }

            if (isMeowing != shouldBeMeowing) {
                isMeowing = shouldBeMeowing;

                if (isMeowing) {
                    meowingInstance.start();
                } else {
                    meowingInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
                }
            }
        }

        public void OnSetScreen(ScreenAsset screen) {
            this.screen = screen;
            UpdateSpeaker();
        }

        void UpdateSpeaker() {
            if (isSetUp && !string.IsNullOrEmpty(speaker)) {
                RuntimeManager.StudioSystem.setParameterByIDWithLabel(speakerDescription.id, speaker);
            }
        }
    }
}
