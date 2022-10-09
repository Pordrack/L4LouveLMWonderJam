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
        
        public static Vector2Int GetPlayerPosInGrid() => new Vector2Int(_instance.PlayerX, _instance.PlayerZ);

        public static void UpdatePlayerPosInGrid(int x, int y)
        {
            _instance.PlayerX = x;
            _instance.PlayerZ = y;
        }
        public int PlayerX { get; private set; }
        public int PlayerZ { get; private set; }//indices of the player relatively to the map
        // This is different from its world position in Unity.
        
        
        public GameObject gameTable;
        private environnement_bloc[,] _map;
        
        
        private Transform _tf; //for fewer c++ calls
        private AnimationController _anim;
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

            if (Input.GetKeyDown(KeyCode.T))
            {
                GenerationMap.TeleportPlayer();
            }
        }
        

        private void Awake()
        {
            if(_instance != null && _instance != this) Destroy(gameObject);
            _instance = this;
            
            _tf = transform;
            GenerationMap.OnGenerationComplete += GetGeneratedMap;
 
            //init position of the player.
            //TODO : this must be changed
            var position = _tf.position;
            PlayerX = (int) position.x;
            PlayerZ = (int) position.z;
            
            _anim = GetComponent<AnimationController>();
            Debug.LogWarning("Ne pas oublier d'enlever les touches de debug");
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
                var angle = (int) Vector2.SignedAngle(new Vector2(_tf.forward.x, _tf.forward.z), direction);
                switch (angle)
                {
                    case 90:
                        _anim.MoveAnimation(AnimationController.AnimationMove.Right);
                        break;
                    case -90:
                        _anim.MoveAnimation(AnimationController.AnimationMove.Left);
                        break;
                    case 180:
                        _anim.MoveAnimation(AnimationController.AnimationMove.Down);
                        break;
                    case -180:
                        _anim.MoveAnimation(AnimationController.AnimationMove.Down);
                        break;
                    case 0:
                        _anim.MoveAnimation(AnimationController.AnimationMove.Up);
                        break;
                }
                Debug.Log($"We have a rotation of angle {angle}");
                _tf.rotation *= Quaternion.Euler(0,angle,0);
            }

        }

        private bool IsMoveLegal(environnement_bloc targetBlock)
        {
            //Check if there is an enemy on the move
            return targetBlock.get_type() == 1 && !EnemyManager.Singleton.IsThereAnEnemy(targetBlock.transform.position + 0.2f * Vector3.up);
        }
        

    }
}
