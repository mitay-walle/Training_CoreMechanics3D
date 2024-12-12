using UnityEngine;
using UnityEngine.Splines;

namespace Runner.Scripts.LevelGeneration
{
    [RequireComponent(typeof(SplineInstantiate))]
    [RequireComponent(typeof(SplineContainer))]
    public class LevelLineChunk : MonoBehaviour
    {
        [field: SerializeField] public bool IsWalkable { get; private set; } = true;
        private SplineInstantiate SplineInstantiate => GetComponent<SplineInstantiate>();
    }
}