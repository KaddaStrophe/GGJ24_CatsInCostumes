using UnityEngine;
using UnityEngine.InputSystem;
using InkStory = Ink.Runtime.Story;

namespace CatsInCostumes {
    sealed class PlayInk : MonoBehaviour, IInkMessages {
        [SerializeField]
        TextAsset inkFile;
        [SerializeField]
        InputActionReference advanceAction;

        InkStory story;

        void OnEnable() {
            if (advanceAction.action is { } action) {
                action.performed += HandleAdvance;
                action.Enable();
            }
        }

        void OnDisable() {
            if (advanceAction.action is { } action) {
                action.performed -= HandleAdvance;
                action.Disable();
            }
        }

        void HandleAdvance(InputAction.CallbackContext context) {
            if (story is not null) {
                NextPage();
            }
        }

        void Start() {
            SetInk();
        }

        void SetInk() {
            if (inkFile) {
                story = new(inkFile.text);
                NextPage();
            }
        }

        public void NextPage() {
            if (story.canContinue) {
                var screen = ScriptableObject.CreateInstance<ScreenAsset>();
                screen.speech = story.Continue();
                foreach (string tag in story.currentTags) {
                    string key = tag.Split(' ')[0];
                    string value = tag[(key.Length + 1)..];

                    switch (key) {
                        case "background":
                            screen.background = value;
                            break;
                        case "speaker":
                            screen.speaker = value;
                            break;
                        case "mood":
                            screen.mood = value;
                            break;
                    }
                }

                gameObject.BroadcastMessage(nameof(IScreenMessages.OnSetScreen), screen, SendMessageOptions.DontRequireReceiver);
            } else {
                Destroy(gameObject);
            }
        }

        public void OnSetInk(TextAsset inkFile) {
            this.inkFile = inkFile;
            SetInk();
        }
    }
}
