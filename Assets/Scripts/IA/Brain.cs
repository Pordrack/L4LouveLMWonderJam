using System;
using System.Collections.Generic;
using UnityEngine;

namespace IA
{
    [RequireComponent(typeof(MeshRenderer))]
    [RequireComponent(typeof(OtherNavBehavior))]
    public abstract class Brain : MonoBehaviour
    {
        [SerializeField] protected int moveAmount = 1;
        protected OtherNavBehavior Nav { get; private set; }
        protected Transform Tf { get; private set; }
        protected MeshRenderer Rend { get; private set; }
        private int _playerX = -1, _playerY = -1;
        private int _minX,_maxX,_minY,_maxY;

        private void Start()
        {
            Tf = transform;
            Nav = GetComponent<OtherNavBehavior>();
            Rend = GetComponent<MeshRenderer>();
            EnableRendering(false);
            var playerPos = EnemyManager.Singleton.GetPlayerPosition();
            _playerX = (int) playerPos.x ; 
            _playerY = (int) playerPos.z;
            var half = MapMaskHandler.DrawnMapSize/2;    
            _minX = _playerX - half;
            _maxX = _playerX + half;
            _minY = _playerY-half;
            _maxY = _playerY+half;
            
        }

        private void EnableRendering(bool b)
        {
            if (!b && !Rend.isVisible) return;
            if (b && Rend.isVisible) return;
            Rend.enabled = b;
            foreach (var childRenderer in transform.GetComponentsInChildren<MeshRenderer>())
            {
                childRenderer.enabled = b;
            }
        }

        public abstract void Decide();

        /// <summary>
        /// Permet d'obtenir les cases déplaçables aux alentours.
        /// Cette fonction prend aussi les cases diagonales pour l'instant.
        /// </summary>
        /// <param name="pos">Position of the player</param>
        /// <returns>List of possible position on the map.</returns>
        protected List<int[]> GetAvailableSurrounding(Vector3 pos)
        {
            var x = pos.x;
            var y = pos.z;

            var availableSurrounding = new List<int[]>();

            int posX, posY;
            for (var i = 0; i < 9; i++)
            {
                posX = (int) x + (i % 3) - 1;
                posY = (int) y + (i / 3) - 1;

                if (Generation.GenerationMap.IsAvailable(posX, posY))
                {
                    availableSurrounding.Add(new int[] {posX, posY});
                }
                
            }

            return availableSurrounding;
        }

        public void ShallBeDrawn()
        {
            if(_playerX == -1 || _playerY == -1)
                return;
            
            //We only hide the visual part, the animals still play, even out of bounds.
            var local = Tf.position;
            if (local.x < _minX || local.x > _maxX || local.z < _minY || local.z > _maxY)
            {
                EnableRendering(false);
            }
            else
            {
                EnableRendering(true);
            }
        }

        public virtual void Die()
        {
            Debug.Log("He's dead Jim.");
            Destroy(gameObject);
        }
    }
    
    
}
