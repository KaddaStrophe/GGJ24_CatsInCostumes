using System.Collections;
using System.Collections.Generic;
using FMODUnity;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace CatsInCostumes {
    sealed class GameManager : MonoBehaviour {
        [Header("Addressables")]
        [SerializeField]
        string backgroundLabel = "backgrounds";
        [SerializeField]
        string characterLabel = "characters";
        [SerializeField]
        string storyLabel = "stories";
        [SerializeField]
        string iconLabel = "icons";

        static Dictionary<string, Sprite> backgrounds = new();
        static Dictionary<string, CharacterAsset> characters = new();
        static Dictionary<string, TextAsset> stories = new();
        static Dictionary<string, Sprite> icons = new();

        internal static readonly WaitUntil waitUntilReady = new(() => isReady);
        static bool isReady = false;

        internal static bool TryGetBackground(string id, out Sprite asset) => TryGetFromDictionary(id, out asset, backgrounds);
        internal static bool TryGetCharacter(string id, out CharacterAsset asset) => TryGetFromDictionary(id, out asset, characters);
        internal static bool TryGetStory(string id, out TextAsset asset) => TryGetFromDictionary(id, out asset, stories);
        internal static bool TryGetIcon(string id, out Sprite asset) => TryGetFromDictionary(id, out asset, icons);

        [Header("FMOD")]
        [SerializeField, ParamRef]
        string gameParameter;

        IEnumerator Start() {
            yield return LoadLabelToDictionary(backgroundLabel, backgrounds);
            yield return LoadLabelToDictionary(characterLabel, characters);
            yield return LoadLabelToDictionary(storyLabel, stories);
            yield return LoadLabelToDictionary(iconLabel, icons);

            yield return new WaitUntil(() => RuntimeManager.HaveAllBanksLoaded);

            if (RuntimeManager.StudioSystem.getParameterDescriptionByName(gameParameter, out var description) == FMOD.RESULT.OK) {
            }

            isReady = true;
        }

        IEnumerator LoadLabelToDictionary<T>(string label, Dictionary<string, T> dictionary) {
            var locations = Addressables.LoadResourceLocationsAsync(label, typeof(T));
            yield return locations;

            foreach (var location in locations.Result) {
                var handle = Addressables.LoadAssetAsync<T>(location);
                yield return handle;
                dictionary[location.PrimaryKey] = handle.Result;
            }
        }

        static bool TryGetFromDictionary<T>(string id, out T asset, Dictionary<string, T> dictionary) where T : Object {
            foreach (var (key, value) in dictionary) {
                if (string.Equals(key, id, System.StringComparison.OrdinalIgnoreCase)) {
                    asset = value;
                    return asset;
                }
            }

            asset = default;
            return false;
        }
    }
}
