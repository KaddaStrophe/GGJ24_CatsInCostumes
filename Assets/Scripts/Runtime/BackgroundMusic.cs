using System.Collections;
using FMOD.Studio;
using FMODUnity;
using UnityEngine;

namespace CatsInCostumes {
    sealed class BackgroundMusic : MonoBehaviour, IGameMessages {
        [SerializeField]
        EventReference menuMusic = new();

        EventInstance menuInstance;

        [SerializeField]
        EventReference gameMusic = new();

        EventInstance gameInstance;

        bool isSetUp = false;

        IEnumerator Start() {
            yield return new WaitUntil(() => RuntimeManager.HaveAllBanksLoaded);

            menuInstance = RuntimeManager.CreateInstance(menuMusic.Guid);
            gameInstance = RuntimeManager.CreateInstance(gameMusic.Guid);

            isSetUp = true;

            OnSetState(GameManager.gameState);
        }

        public void OnSetState(GameState state) {
            if (isSetUp) {
                var (menuPlaying, gamePlaying) = state switch {
                    GameState.MainMenu => (true, false),
                    GameState.Credits => (true, false),
                    GameState.PlayingDialog => (false, true),
                    GameState.WaitingForReaction => (false, true),
                    GameState.WaitingForScene => (false, true),
                    _ => (false, false),
                };

                menuInstance.SetIsPlaying(menuPlaying);
                gameInstance.SetIsPlaying(gamePlaying);
            }
        }
    }
}
