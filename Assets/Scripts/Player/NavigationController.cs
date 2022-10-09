using System;
using System.Security.Cryptography;
using Generation;
using IA;
using PlayerH;
using UnityEditor;
using UnityEngine;

namespace Player
{
    public class NavigationController : MonoBehaviour
    {
        private static NavigationController _instance;
        
        public static Vector2Int GetPlayerPos() => new Vector2Int(_instance.PlayerX, _instance.PlayerZ);

        public int PlayerX { get; private set; }
        public int PlayerZ { get; private set; }//indices of the player relatively to the map
        // This is different from its world position in Unity.
        
        
        public GameObject gameTable;
        private environnement_bloc[,] _map;
        
        
        private Transform _tf; //for fewer c++ calls

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.M))
            {
                var a = GenerationMap.GetAllResourcesInArea(PlayerX, PlayerZ, 2);
                var str = "[";
                foreach (var value in a)
                {
                    str += $"{value},";
                }

                str += "]";
                Debug.Log(str);
            }
            
            if (Input.GetKeyDown(KeyCode.K))
            {
                EnemyManager.Singleton.KillEnemiesInAnArea(transform.position,2);
            }
        }
        

        private void Awake()
        {
            if(_instance != null && _instance != this) Destroy(gameObject);
            _instance = this;
            
            _tf = transform;
            Generation.GenerationMap.OnGenerationComplete += GetGeneratedMap;
            //init position of the player.
            //TODO : this must be changed
            var position = _tf.position;
            PlayerX = (int) position.x;
            PlayerZ = (int) position.z;
        }

        private void GetGeneratedMap(int x, int y)
        {
            //Set player position given by the generator
            PlayerX = x;
            PlayerZ = y;
            
            //Move the plate 
            var playerRealPos = _tf.position;
            gameTable.transform.position = new Vector3(playerRealPos.x-x, gameTable.transform.position.y, playerRealPos.z-y);

            var map = Generation.GenerationMap.MapsEnvironment;
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
            var newX = PlayerX + (int) direction.x;
            var newZ = PlayerZ + (int) direction.y;
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
                Generation.GenerationMap.UpdateMask(newX,newZ);
                //Update player position
                PlayerX = newX;
                PlayerZ = newZ;
                
                //Update its rotation according to the movement.
                _tf.rotation = Quaternion.RotateTowards(_tf.rotation, Quaternion.LookRotation(new Vector3(direction.x, 0, direction.y)), 180);
            }

        }

        private bool IsMoveLegal(environnement_bloc targetBlock)
        {
            //print($"The type of the target block is {targetBlock.get_type()}");
            return targetBlock.get_type() == 1;
        }
        

    }
}
