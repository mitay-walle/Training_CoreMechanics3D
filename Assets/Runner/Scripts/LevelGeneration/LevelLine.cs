using Runner.Scripts.Players;
using UnityEngine;
using UnityEngine.Splines;

namespace Runner.Scripts.LevelGeneration
{
    [RequireComponent(typeof(SplineInstantiate))]
    [RequireComponent(typeof(SplineContainer))]
    public class LevelLine : MonoBehaviour
    {
        private SplineInstantiate SplineInstantiate => GetComponent<SplineInstantiate>();
        private LevelGenerator LevelGenerator => GetComponentInParent<LevelGenerator>();

        public void Initialize(eLine eLine)
        {
            transform.localPosition = new((float)eLine * LevelGenerator.LineDistanceX, 0);
            transform.localRotation = Quaternion.identity;
        }
    }
}