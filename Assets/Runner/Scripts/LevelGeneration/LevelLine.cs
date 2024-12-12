using System.Collections.Generic;
using Runner.Scripts.Players;
using UnityEngine;

namespace Runner.Scripts.LevelGeneration
{
    public class LevelLine : MonoBehaviour
    {
        private LevelGenerator LevelGenerator => GetComponentInParent<LevelGenerator>();
        public eLine Line { get; private set; }
        public float X => LevelGenerator.LineDistanceX * (int)Line;
        private Dictionary<GameObject, List<GameObject>> _chunks = new();

        public void Initialize(eLine line)
        {
            Line = line;
            transform.localPosition = new((float)line * LevelGenerator.LineDistanceX, 0);
            transform.localRotation = Quaternion.identity;
        }

        public void CreateChunk(GameObject chunkPrefab, float z)
        {
            if (!_chunks.TryGetValue(chunkPrefab, out var chunks))
            {
                _chunks[chunkPrefab] = chunks = new List<GameObject>();
            }

            AsyncInstantiateOperation<GameObject> async =
                InstantiateAsync(chunkPrefab, new Vector3(X, 0, z), Quaternion.identity);

            async.completed += _ =>
            {
                if (async.Result.Length > 0)
                {
                    chunks.Add(async.Result[0]);
                }
            };
        }
    }
}