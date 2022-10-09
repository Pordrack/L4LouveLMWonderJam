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

        private float angularSpeed = 1000f;
        private Animator _anim;
        private int _moveHash;
        private Quaternion? _targetRotation;

        private float speed = 2f;
        private Vector3? _targetPosition;
        
        private Transform _tf; //for fewer c++ calls
        
        [SerializeField] private int movingEnergyCost = 10; 

        private void Update()
        {
            if (_targetRotation is not null)
            {
                _tf.rotation = Quaternion.RotateTowards(_tf.rotation, _targetRotation.Value, angularSpeed*Time.deltaTime);
                
                if (_tf.rotation == _targetRotation)
                {
                    _targetRotation = null;
                }
                
            }

            if (_targetPosition is not null)
            {
                AudioManager.instance.Play("Marche");
                gameTable.transform.position = Vector3.MoveTowards(gameTable.transform.position, _targetPosition.Value, speed*Time.deltaTime);
                EnemyManager.Singleton.DrawEnemies();
                if(gameTable.transform.position == _targetPosition)
                {
                    _targetPosition = null;
                }
            }
            
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
            Generation.GenerationMap.OnGenerationComplete += GetGeneratedMap;
            //init position of the player.
            //TODO : this must be changed
            var position = _tf.position;
            PlayerX = (int) position.x;
            PlayerZ = (int) position.z;

            _anim = GetComponentInChildren<Animator>();
            _moveHash = Animator.StringToHash("Move");
            
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
            if(_targetRotation is not null) return;
            if (_targetPosition is not null) return;
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
                _targetPosition = gameTable.transform.position + new Vector3(-direction.x, 0, -direction.y);
                //gameTable.transform.position += new Vector3(-direction.x, 0, -direction.y);
                
                //Update table visibility
                Generation.GenerationMap.UpdateMask(newX,newZ);
                //Update player position
                PlayerX = newX;
                PlayerZ = newZ;
                
                //Update its rotation according to the movement and play animation.
                _anim.SetTrigger(_moveHash);
                _targetRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.y));
                angularSpeed = Quaternion.Angle(_tf.rotation, _targetRotation.Value) * 2f;
                
                //Update energy
                Stats_Perso.Instance.down_action(movingEnergyCost);
            }

        }
        

        private bool IsMoveLegal(environnement_bloc targetBlock)
        {
            //Check if there is an enemy on the move
            return targetBlock.get_type() == 1 && !EnemyManager.Singleton.IsThereAnEnemy(targetBlock.transform.position + 0.2f * Vector3.up);
        }
        

    }
}
