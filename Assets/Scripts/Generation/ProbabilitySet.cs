using System;
using UnityEngine;

namespace GenerationNS
{
    [CreateAssetMenu(fileName = "Probability", menuName = "App/ProbabilitySheet", order = 0)]
    public class ProbabilitySet : ScriptableObject
    {
        [Range(0, 1)] [Tooltip("The proportion of enemies set on a free zone. (Try not to exceed 50%)")]
        public float enemyApparitionRate = 0.33f;

        public ProbabilitySpawn[] probabilities;
        
        public GameObject GetRandomEnemy()
        {
            float random = UnityEngine.Random.Range(0f, 1f);
            float current = 0f;
            foreach (var probability in probabilities)
            {
                current += probability.probability;
                if (random <= current)
                {
                    return probability.enemyPrefab;
                }
            }
            return null; //Shall never happen
        }
    }


    [Serializable]
    public struct ProbabilitySpawn
    {
        public string enemyName;
        public GameObject enemyPrefab;
        [Range(0, 1)] public float probability;
    }
}