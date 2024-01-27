using System;
using System.Collections.Generic;
using System.Linq;
using Slothsoft.UnityExtensions;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace CatsInCostumes {
    [CreateAssetMenu]
    sealed class CharacterAsset : ScriptableObject {
        internal static CharacterAsset empty => CreateInstance<CharacterAsset>();
        internal static CharacterAsset GetAssetById(string id) {
            if (IsNarrator(id)) {
                return empty;
            }

            var locationHandle = Addressables.LoadResourceLocationsAsync(id, typeof(CharacterAsset));
            locationHandle.WaitForCompletion();
            var locations = locationHandle.Result;
            if (locations.Count == 0) {
                return empty;
            }

            var assetHandle = Addressables.LoadAssetAsync<CharacterAsset>(locations[0]);
            assetHandle.WaitForCompletion();
            if (!assetHandle.Result) {
                return empty;
            }

            return assetHandle.Result;
        }
        internal static bool IsNarrator(string id) {
            return string.IsNullOrEmpty(id) || id == "-";
        }

        [SerializeField]
        SerializableKeyValuePairs<Mood, Sprite> sprites = new();

        internal bool TryGetSprite(Mood mood, out Sprite sprite) => sprites.TryGetValue(mood, out sprite) && sprite;

#if UNITY_EDITOR
        void OnValidate() {
            var newSprites = Enum
                .GetValues(typeof(Mood))
                .OfType<Mood>()
                .ToDictionary(m => m, m => sprites.GetValueOrDefault(m));

            sprites.SetItems(newSprites);
        }
#endif
    }
}
