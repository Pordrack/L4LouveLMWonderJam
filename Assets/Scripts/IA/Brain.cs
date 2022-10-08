using System;
using System.Collections.Generic;
using UnityEngine;

namespace IA
{
    [RequireComponent(typeof(Renderer))]
    [RequireComponent(typeof(OtherNavBehavior))]
    public abstract class Brain : MonoBehaviour 
    {
        protected OtherNavBehavior Nav { get; private set; }
        protected Transform Tf { get; private set; }
        protected Renderer Rend { get; private set; }
        private int _playerX, _playerY;
        private int _minX,_maxX,_minY,_maxY;

        private void Start()
        {
            Tf = transform;
            Nav = GetComponent<OtherNavBehavior>();
            Rend = GetComponent<Renderer>();
            Rend.enabled = false;
            Generation.OnGenerationComplete += (x,y) =>
            {
                _playerX = x ; 
                _playerY = y;
                var half = MapMaskHandler.DrawnMapSize/2;    
                _minX = x - half;
                _maxX = x + half;
                _minY = y-half;
                _maxY = y+half;
            };
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

                if (Generation.IsAvailable(posX, posY))
                {
                    availableSurrounding.Add(new int[] {posX, posY});
                }
                
            }

            return availableSurrounding;
        }

        protected void ShallBeDrawn()
        {
            if(_playerX == 0 || _playerY == 0)
                return;
            
            //We only hide the visual part, the animals still play, even out of bounds.
            var local = Tf.localPosition;
            Debug.Log($"We have ({local.x},{local.z}) with extrema being X = ({_minX},{_maxX}) and Y = ({_minY},{_maxY})");
            if (local.x < _minX || local.x > _maxX || local.z < _minY || local.z > _maxY)
                Rend.enabled = false;
            else
                Rend.enabled = true;
            print("We get in true statement.");
        }
    }
    
    
}
