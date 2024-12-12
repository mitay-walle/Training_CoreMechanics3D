using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Plugins.GameObjectOperations.Placement
{
    [Serializable]
    public class WeightRandom
    {
    }

    [Serializable]
    public class WeightRandomPrefab : WeightRandom<GameObject>
    {
        
    }
    
    [Serializable]
    public class WeightRandom<T> : WeightRandom
    {
        [Serializable]
        private class ValueClass
        {
            public int Weight;
            public T Value;
        }

        [SerializeField] private ValueClass[] _values;

        public T GetRandomValue()
        {
            int weightSum = 0;

            for (int i = 0; i < _values.Length; ++i)
            {
                weightSum += _values[i].Weight;
            }

            int index = 0;
            int lastIndex = _values.Length - 1;

            while (index < lastIndex)
            {
                if (Random.Range(0, weightSum) < _values[index].Weight) return _values[index].Value;

                ;
                weightSum -= _values[index++].Weight;
            }

            return _values[index].Value;
        }
    }
    
    
}
