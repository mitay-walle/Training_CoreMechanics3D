using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace GameJam.Plugins.Randomize
{
    public static class RandomExtensions
    {
        public static int GetWeightedIndex(IReadOnlyList<int> weights)
        {
            int sum = weights.Sum();
            int random = UnityEngine.Random.Range(0, sum);
            int current = 0;
            for (int i = 0; i < weights.Count; i++)
            {
                current += weights[i];

                if (random < current && weights[i] > 0)
                {
                    return i;
                }
            }

            return -1;
        }

        public static T Random<T>(this IList<T> objects, int maxI = -1, int minI = 0)
        {
            if (objects.Count == 0)
            {
                return default;
            }

            var rand = UnityEngine.Random.Range(minI, maxI == -1 ? objects.Count : maxI);
            return objects[rand];
        }

        public static float SignedRandom(float range)
        {
            return UnityEngine.Random.Range(-range, range);
        }

        public static float Random(this Vector4 minMax, int XYOrZW = 0)
        {
            if (XYOrZW == 0) return UnityEngine.Random.Range(minMax.x, minMax.y);

            return UnityEngine.Random.Range(minMax.z, minMax.w);
        }

        public static int RandomDirection()
        {
            return UnityEngine.Random.value > .5f ? 1 : -1;
        }

        public static float Random(this Vector2 minMax)
        {
            return UnityEngine.Random.Range(minMax.x, minMax.y);
        }

        public static int Random(this Vector2Int minMax)
        {
            return UnityEngine.Random.Range(minMax.x, minMax.y);
        }

        public static AudioClip Random(this AudioClip[] clips)
        {
            if (clips == null) return null;
            if (clips.Length == 0) return null;

            return clips[UnityEngine.Random.Range(0, clips.Length)];
        }

        public static void Shuffle<T>(this IList<T> ts)
        {
            var count = ts.Count;
            var last = count - 1;
            for (var i = 0; i < last; ++i)
            {
                var r = UnityEngine.Random.Range(i, count);
                (ts[i], ts[r]) = (ts[r], ts[i]);
            }
        }

        public static bool RandomBool(float chance = .5f)
        {
            return UnityEngine.Random.value > chance;
        }
    }
}