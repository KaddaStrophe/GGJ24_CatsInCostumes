using System;
using System.Collections;
using MyBox;
using UnityEngine;
using InkStory = Ink.Runtime.Story;

namespace CatsInCostumes {
    sealed class PlayInk : MonoBehaviour, IInkMessages {
        [SerializeField]
        TextAsset inkFile;

        InkStory story;
        ScreenAsset currentScreen;

        IEnumerator Start() {
            NextPage();

            yield return GameManager.waitUntilReady;

            SetInk();
        }

        void SetInk() {
            if (inkFile) {
                story = new(inkFile.text);
                currentScreen = ScriptableObject.CreateInstance<ScreenAsset>();
            }

            NextPage();
        }

        public void NextPage() {
            if (story is null) {
                gameObject.BroadcastMessage(nameof(IScreenMessages.OnSetScreen), ScreenAsset.empty, SendMessageOptions.DontRequireReceiver);
                return;
            }

            if (story is { currentChoices: var choices } && choices.Count > 0) {
                return;
            }

            if (story.canContinue) {
                currentScreen = Instantiate(currentScreen);
                currentScreen.speech = story.Continue().Trim();
                foreach (string tag in story.currentTags) {
                    string key = tag.Split(' ')[0];
                    string value = tag[(key.Length + 1)..];

                    switch (key) {
                        case "background":
                            currentScreen.background = value;
                            break;
                        case "speaker":
                            currentScreen.speaker = value;
                            break;
                        case "mood":
                            currentScreen.mood = Enum.TryParse<Mood>(value, true, out var mood)
                                ? mood
                                : Mood.Neutral;
                            break;
                    }
                }

                gameObject.BroadcastMessage(nameof(IScreenMessages.OnSetScreen), currentScreen, SendMessageOptions.DontRequireReceiver);
            } else {
                story = null;
            }
        }

        public void OnSetInk(TextAsset inkFile) {
            this.inkFile = inkFile;
            SetInk();
        }

        public void OnAdvanceInk() => NextPage();

        public void OnReact(string reaction) {
            if (story is { currentChoices: var choices }) {
                int id = choices.FirstIndex(choice => choice.text.Equals(reaction, StringComparison.OrdinalIgnoreCase));
                if (id != -1) {
                    story.ChooseChoiceIndex(id);
                    NextPage();
                }
            }
        }
    }
}
