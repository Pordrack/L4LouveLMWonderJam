using System;
using PlayerH;
using UnityEngine;

namespace Player
{
    public class NavigationController : MonoBehaviour
    {

        private int _playerX, _playerZ; //indices of the player relatively to the map
        // This is different from its world position in Unity.
        
        public GameObject gameTable;
        private environnement_bloc[,] _map;
        
        
        private Transform _tf; //for fewer c++ calls
        

        private void Awake()
        {
            _tf = transform;
            Generation.OnGenerationComplete += GetGeneratedMap;
            //init position of the player.
            //TODO : this must be changed
            var position = _tf.position;
            _playerX = (int) position.x;
            _playerZ = (int) position.z;
        }

        private void GetGeneratedMap(int x, int y)
        {
            //Set player position given by the generator
            _playerX = x;
            _playerZ = y;
            
            //Move the plate 
            var playerRealPos = _tf.position;
            gameTable.transform.position = new Vector3(playerRealPos.x-x, gameTable.transform.position.y, playerRealPos.z-y);

            var map = Generation.MapsEnvironment;
            _map = new environnement_bloc[map.GetLength(0), map.GetLength(1)];
            //Go through each element of the map
            for (var i = 0; i < map.GetLength(0); i++)
            {
                for (var j = 0; j < map.GetLength(1); j++)
                {
                    _map[i, j] = map[i, j].GetComponent<environnement_bloc>();
                }
            }
            InputController.Instance.SwitchInputToMovement(true);
        }


        public void TryToMove(Vector2 direction)
        {
            if (_map is null) return;
            // Get the wanted new player's position
            var newX = _playerX + (int) direction.x;
            var newZ = _playerZ + (int) direction.y;
            //Debug.Log($"Current position registered is {_playerX}, {_playerZ}.\n Current position (transform) is {_tf.position.x}, {_tf.position.z}.\n" +
              //        $" The direction we get is {direction} .\n" +
                //      $" New position is {newX}, {newZ}.");
            
            //Ensure it is in bounds.
            if(newX < 0 || newX >= _map.GetLength(0) || newZ < 0 || newZ >= _map.GetLength(1)) return;
            
            //Get the targeted block
            var targetBlock = _map[newX, newZ];
            if (IsMoveLegal(targetBlock))
            {
                //Move the table
                gameTable.transform.position += new Vector3(-direction.x, 0, -direction.y);
                //Update table visibility
                Generation.UpdateMask(newX,newZ);
                //Update player position
                _playerX = newX;
                _playerZ = newZ;
                
                //Update its rotation according to the movement.
                _tf.rotation = Quaternion.RotateTowards(_tf.rotation, Quaternion.LookRotation(new Vector3(direction.x, 0, direction.y)), 180);
            }

        }

        private bool IsMoveLegal(environnement_bloc targetBlock)
        {
            print($"The type of the target block is {targetBlock.get_type()}");
            return targetBlock.get_type() == 1;
        }
        

    }
}
