using System;
using AYellowpaper.SerializedCollections;
using Runner.Scripts.Players;
using UnityEngine;

namespace Runner.Scripts.LevelGeneration
{
    public class LevelGenerator : MonoBehaviour
    {
        [field: SerializeField] public float LineDistanceX { get; private set; } = 5;
        [SerializedDictionary] public SerializedDictionary<eLine, LevelLine> Lines = new();

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
            }
        }
    }
}