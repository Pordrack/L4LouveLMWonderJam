using System;
using System.Collections.Generic;
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

        [SerializeField] private Transform enemyParent;
        private List<Brain> _brains;
        

        /// <summary>
        /// This methods collects all the brain component of each enemy.
        /// </summary>
        private void GatherBrains()
        {
            foreach (var brain in enemyParent.GetComponentsInChildren<Brain>())
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
    }
}
