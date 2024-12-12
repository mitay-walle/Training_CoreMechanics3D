using System;
using System.Collections.Generic;
using System.Linq;
using AYellowpaper.SerializedCollections;
using Runner.Scripts.Players;
using UnityEngine;
using GameJam.Plugins.Randomize;
using TriInspector;
using UnityEditor;

namespace Runner.Scripts.LevelGeneration
{
    public class LevelGenerator : MonoBehaviour
    {
        [SerializeField, Required, AssetPath.Attribute(typeof(DefaultAsset))] private string _walkableChunksFolder;
        [SerializeField, Required, AssetPath.Attribute(typeof(DefaultAsset))] private string _unwalkableChunksFolder;
        [SerializeField, Required] private GameObject[] _walkableChunks;
        [SerializeField, Required] private GameObject[] _unwalkableChunks;
        [field: SerializeField] public float LineDistanceX { get; private set; } = 5;
        [field: SerializeField] public float LineDistanceZ { get; private set; } = 5;
        [SerializeField] private int _prespawnChunksCount = 10;
        [SerializedDictionary] public SerializedDictionary<eLine, LevelLine> Lines = new();
        private List<LevelLine> _buffer = new();

        private void Awake()
        {
            transform.position = default;
            transform.rotation = Quaternion.identity;

            foreach (eLine lineValue in Enum.GetValues(typeof(eLine)))
            {
                var line = new GameObject($"Line {lineValue}", typeof(LevelLine)).GetComponent<LevelLine>();
                line.transform.SetParent(transform);
                line.Initialize(lineValue);
                Lines.Add(lineValue, line);
                _buffer.Add(line);
            }

            for (int i = 0; i < _prespawnChunksCount; i++)
            {
                GenerateChunk(LineDistanceZ * i);
            }
        }

        private void GenerateChunk(float z)
        {
            _buffer.Shuffle();
            foreach (LevelLine line in _buffer)
            {
                bool isLast = _buffer[^1] == line;

                GameObject[] chunks = _walkableChunks;
                if (!isLast)
                {
                    // one last guaranteed walkable line, other are random
                    chunks = RandomExtensions.RandomBool(.5f) ? _walkableChunks : _unwalkableChunks;
                }

                line.CreateChunk(chunks.Random(), z);
            }
        }

        [Button]
        private void LoadPrefabs()
        {
#if UNITY_EDITOR

            _walkableChunks = AssetDatabase.FindAssets("t:prefab", new[] { _walkableChunksFolder })
                .Select(AssetDatabase.GUIDToAssetPath).Select(AssetDatabase.LoadMainAssetAtPath).OfType<GameObject>()
                .ToArray();
            _unwalkableChunks = AssetDatabase.FindAssets("t:prefab", new[] { _unwalkableChunksFolder })
                .Select(AssetDatabase.GUIDToAssetPath).Select(AssetDatabase.LoadMainAssetAtPath).OfType<GameObject>()
                .ToArray();
#endif
        }
    }
}