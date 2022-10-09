using System;
using System.Collections.Generic;
using Generation;
using UnityEngine;

namespace IA
{
    public class EnemyManager : MonoBehaviour
    {
        #region Singleton declaration

        public static EnemyManager Singleton;

        private void Awake()
        {
            if(Singleton != null && Singleton != this) Destroy(gameObject);

            Singleton = this;

            _brains = new List<Brain>();
        }

        #endregion
        
        [SerializeField] private Transform player;
        [SerializeField] private LayerMask enemyLayer;
        private Transform _enemyParent;
        private List<Brain> _brains;

        private void Start()
        {
            _enemyParent = transform;

            GenerationMap.OnGenerationComplete += (x, y) =>
            {
                GatherBrains();
                DrawEnemies();
            };
        }

        /// <summary>
        /// This methods collects all the brain component of each enemy.
        /// </summary>
        private void GatherBrains()
        {
            foreach (var brain in _enemyParent.GetComponentsInChildren<Brain>())
            {
                _brains.Add(brain);
            }
        }

        #region Adders & Setters

        public void AddEnemy(Transform enemy)
        {
            _brains.Add(enemy.GetComponent<Brain>());
        }
        
        public void RemoveEnemy(Transform enemy)
        {
            _brains.Remove(enemy.GetComponent<Brain>());
        }
        
        public void RemoveEnemy(Brain brain)
        {
            _brains.Remove(brain);
        }

        #endregion

        /// <summary>
        /// This method is called on enemy turn and perform the turn logic of each enemy existing on the board (not only the visible part)
        /// </summary>
        public void PerformEnemiesTurn()
        {
            foreach (var brain in _brains)
            {
                brain.Decide();
            }
        }

        public void KillEnemiesInAnArea(Vector3 worldPos, int range)
        {
            var colliders = Physics.OverlapBox(worldPos, range * (Vector3.right + Vector3.forward),
                Quaternion.identity, enemyLayer);
            Debug.Log("There is " + colliders.Length + " enemies in the area.");
            
            //Remove the brain from the list
            foreach (var collider in colliders)
            {
                var brain = collider.GetComponent<Brain>();
                RemoveEnemy(brain);
                brain.Die();
            }
        }

        public void DrawEnemies()
        {
            foreach (var brain in _brains)
            {
                brain.ShallBeDrawn();
            }
        }

        public bool IsThereAnEnemy(Vector3 worldPos)
        {
            var colliders = Physics.OverlapSphere(worldPos, 0.5f, enemyLayer);
            return colliders.Length > 0;
        }

        public Vector3 GetPlayerPosition() => player.position;
    }
}
