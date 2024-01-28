using System;
using System.Collections;
using MyBox;
using UnityEngine;
using InkStory = Ink.Runtime.Story;

namespace CatsInCostumes {
    sealed class PlayInk : MonoBehaviour, IInkMessages, IGameMessages {
        [SerializeField]
        TextAsset inkFile;

        InkStory story;
        ScreenAsset currentScreen;
        bool storyHasChoices => story is { currentChoices: var choices } && choices.Count > 0;

        IEnumerator Start() {
            currentScreen = ScreenAsset.empty;

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

            if (storyHasChoices) {
                return;
            }

            if (story.canContinue) {
                currentScreen = Instantiate(currentScreen);
                currentScreen.speech = story.Continue().Trim();
                currentScreen.title = "";
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
                        case "nextScene":
                            currentScreen.nextScene = value;
                            break;
                        case "title":
                            currentScreen.title = value;
                            break;
                        default:
                            throw new NotImplementedException(key);
                    }
                }

                gameObject.BroadcastMessage(nameof(IScreenMessages.OnSetScreen), currentScreen, SendMessageOptions.DontRequireReceiver);
                GameManager.gameState = storyHasChoices
                    ? GameState.WaitingForReaction
                    : GameState.PlayingDialog;
            } else {
                story = null;

                gameObject.BroadcastMessage(nameof(IScreenMessages.OnSetScreen), ScreenAsset.empty, SendMessageOptions.DontRequireReceiver);

                if (currentScreen.TryGetNextScene(out var nextScene)) {
                    GameManager.gameState = GameState.WaitingForScene;

                    inkFile = nextScene;
                    Invoke(nameof(SetInk), sceneDelay);
                } else {
                    GameManager.gameState = GameState.MainMenu;
                }
            }
        }

        public void OnSetInk(TextAsset inkFile) {
            this.inkFile = inkFile;
            SetInk();
        }

        public void OnAdvanceInk() {
            if (currentScreen.isMeowing) {
                currentScreen.isMeowing = false;
            } else {
                NextPage();
            }
        }

        [Space]
        [SerializeField]
        float reactionDelay = 1;
        [SerializeField]
        float sceneDelay = 2;

        public void OnReact(string reaction) {
            if (story is { currentChoices: var choices }) {
                int id = choices.FirstIndex(choice => choice.text.Equals(reaction, StringComparison.OrdinalIgnoreCase));
                if (id != -1) {
                    story.ChooseChoiceIndex(id);
                    currentScreen.speaker = "";
                    currentScreen.speech = "";
                    gameObject.BroadcastMessage(nameof(IScreenMessages.OnSetScreen), currentScreen, SendMessageOptions.DontRequireReceiver);
                    Invoke(nameof(NextPage), reactionDelay);
                }
            }
        }

        public void OnSetState(GameState state) {
            if (state == GameState.MainMenu) {
                gameObject.BroadcastMessage(nameof(IScreenMessages.OnSetScreen), ScreenAsset.empty, SendMessageOptions.DontRequireReceiver);
            }
        }
    }
}
