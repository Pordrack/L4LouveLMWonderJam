using System;
using System.Collections.Generic;
using IA;
using Player;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Generation
{
    public class GenerationMap : MonoBehaviour
    {
        private static GenerationMap _instance;

        private void Awake()
        {
            if(_instance != null && _instance != this) Destroy(gameObject);
            _instance = this;
        }

        public static int tailleMap = 100;
        public GameObject bloc; //prefab of the block
        private static GameObject[,] _mapBlocks;
    

        public GameObject environment;
        public static GameObject[,] MapsEnvironment; //Position of the environment elements (tree, rocks, etc ...)
    
        public static event Action<int,int> OnGenerationComplete;
        private static MapMaskHandler _maskHandler;

        // Start is called before the first frame update
        void Start()
        {
            _mapBlocks = new GameObject[tailleMap,tailleMap]; //Init
            _freeBlocks = new List<Vector2Int>();

            //generate blocks for the mapBlocks
            for(int i = 0; i<tailleMap; i++){
                for(int v = 0; v<tailleMap; v++){
                    GameObject go = Instantiate(bloc, new Vector3(i, 0, v), Quaternion.identity,transform) as GameObject;
                    go.transform.localScale = Vector3.one;
                    go.name = "Bloc (" + i + "," + v + ")";
                    _mapBlocks[i,v] = go;
                    if(v==0 || i == 0 || i == tailleMap-1 || v == tailleMap-1){
                        _mapBlocks[i,v].GetComponent<bloc>().set_type(ResourceType.Blocked);
                    }
                    go.SetActive(false);
                }
            }
            procedural(); //Define their type

            MapsEnvironment = new GameObject[tailleMap,tailleMap]; //init
        
            //generate environment elements
            for(int i = 0; i<tailleMap; i++){
                for(int v = 0; v<tailleMap; v++){
                    GameObject go = Instantiate(environment, new Vector3(i, 1, v), Quaternion.identity,transform) as GameObject;
                    go.transform.localScale = Vector3.one;
                    go.name = "Environment (" + i + "," + v + ")";
                    MapsEnvironment[i,v] = go;
                    if(v==0 || i == 0 || i == tailleMap-1 || v == tailleMap-1){
                        MapsEnvironment[i,v].GetComponent<environnement_bloc>().set_type(ResourceType.Blocked);
                    }
                    go.SetActive(false);
                }
            }
            procedural_generation_environment(); //Define their type

            //Player spawn position
            var playerPos = Set_Player_Position();

            //mask logic
            _maskHandler = new MapMaskHandler(playerPos[0], playerPos[1]);
            MaskInit();
        
            //Enemy generation
            PositionEnemy();

            //Warn subscribed classes that the generation is complete, and provide player spawn position (relatively to the map basis)
            OnGenerationComplete?.Invoke(playerPos[0], playerPos[1]);
        }

        //
        void procedural(){
        

            for(int i = 1; i<tailleMap-1; i++){
                for(int v = 1; v<tailleMap-1; v++){
                    if((i+v)<=tailleMap-1){
                        if(_mapBlocks[i-1,v].GetComponent<bloc>().get_type() == (int) ResourceType.Blocked || _mapBlocks[i,v-1].GetComponent<bloc>().get_type() == (int)ResourceType.Blocked){
                            if(Random.Range(0, 100)>50){
                                _mapBlocks[tailleMap-i-1,tailleMap-v-1].GetComponent<bloc>().set_type(ResourceType.Rock); 
                                _mapBlocks[i,v].GetComponent<bloc>().set_type(ResourceType.Rock); 
                            }else{
                                _mapBlocks[tailleMap-i-1,tailleMap-v-1].GetComponent<bloc>().set_type(ResourceType.Blocked); 
                                _mapBlocks[i,v].GetComponent<bloc>().set_type(ResourceType.Blocked); 
                            }

                        }else{
                            procedural_1(i,v);
                        
                        }
                    }
                }
            }

            for(int i = 1; i<tailleMap-1; i++){
                for(int v = 1; v<tailleMap-1; v++){
                    if((i+v)<=tailleMap-1){
                        if(_mapBlocks[i-1,v].GetComponent<bloc>().get_type() == (int) ResourceType.Blocked || _mapBlocks[i,v-1].GetComponent<bloc>().get_type() == (int) ResourceType.Blocked){
                            if(Random.Range(0, 100)>50){
                                _mapBlocks[i,v].GetComponent<bloc>().set_type(ResourceType.Rock); 
                            }else{
                                _mapBlocks[i,v].GetComponent<bloc>().set_type(ResourceType.Blocked); 
                            }

                        }else{
                            procedural_2(i,v);
                        }
                    }

                }
            }

            for(int i = 0; i<tailleMap; i++){
                for(int v = 0; v<tailleMap; v++){
                    _mapBlocks[i,v].GetComponent<bloc>().set_visibility();
                }
            }
        }
    
        void procedural_1(int i, int v){
            _mapBlocks[i,v].GetComponent<bloc>().set_type(Random.Range(1, 3));
            _mapBlocks[tailleMap-i-1,tailleMap-v-1].GetComponent<bloc>().set_type(Random.Range(1, 3));
        }

        void procedural_2(int i,int v){
            _mapBlocks[i,v].GetComponent<bloc>().set_type(Random.Range(1, 3));
        }

        void procedural_generation_environment(){
            for(int i = 0; i<tailleMap; i++){
                for(int v = 0; v<tailleMap; v++){
                    switch(_mapBlocks[i,v].GetComponent<bloc>().get_type()){
                        case 0:
                            MapsEnvironment[i,v].GetComponent<environnement_bloc>().set_type(ResourceType.Blocked); 
                            break;

                        case 1:
                            switch(Random.Range(0, 5)){
                                case 0:
                                    MapsEnvironment[i,v].GetComponent<environnement_bloc>().set_type(ResourceType.Free);
                                    _freeBlocks.Add(new Vector2Int(i,v));
                                    break;

                                case 1:
                                    MapsEnvironment[i,v].GetComponent<environnement_bloc>().set_type(ResourceType.Wood);
                                    break;

                                case 2:
                                    MapsEnvironment[i,v].GetComponent<environnement_bloc>().set_type(ResourceType.Wood); 
                                    break;

                                case 3:
                                    MapsEnvironment[i,v].GetComponent<environnement_bloc>().set_type(ResourceType.Rock); 
                                    break;

                                case 4:
                                    if(Random.Range(0, 100)>75){
                                        MapsEnvironment[i,v].GetComponent<environnement_bloc>().set_type(ResourceType.Food); 
                                    }else{
                                        MapsEnvironment[i,v].GetComponent<environnement_bloc>().set_type(ResourceType.Free); 
                                        _freeBlocks.Add(new Vector2Int(i,v));
                                    }
                                    break;
                            }
                            break;

                        case 2:
                            MapsEnvironment[i,v].GetComponent<environnement_bloc>().set_type(ResourceType.Free);
                            _freeBlocks.Add(new Vector2Int(i,v));
                            break;

                        case 3:
                            MapsEnvironment[i,v].GetComponent<environnement_bloc>().set_type(ResourceType.Free); 
                            _freeBlocks.Add(new Vector2Int(i,v));
                            break;
                    }
                }
            }




            for(int i = 0; i<tailleMap; i++){
                for(int v = 0; v<tailleMap; v++){
                    MapsEnvironment[i,v].GetComponent<environnement_bloc>().set_visibility();
                }
            }
        }

        private int[] Set_Player_Position()
        {
            //init
            var found = false;
            var amount = 10;
            var count = 0;
            var playerPos = Vector2Int.zero;
            var freeAroundIndexes = new List<int>(); 
        
        
            while(count < amount && !found )
            {
                freeAroundIndexes.Clear(); //Reset the list for a new try.
                //Take a random index from the free block list
                var index = Random.Range(0, _freeBlocks.Count);
                //Check if most of the surrounding blocks are free (the player can move)
                playerPos = _freeBlocks[index];
                for (var i = -2; i < 3; i++)
                {
                    for (var j = -2; j < 3; j++)
                    {
                        var intPos = new Vector2Int(playerPos.x + i, playerPos.y + j);
                        if (_freeBlocks.Contains(intPos))
                        {
                            freeAroundIndexes.Add(_freeBlocks.IndexOf(intPos));
                        }
                    }
                }

                found = freeAroundIndexes.Count >= 6;
                count++;
            }

            //Return the correct position and clear the blocks arounds
            if (found)
            {
                foreach (var index in freeAroundIndexes)
                {
                    _freeBlocks.RemoveAt(index);
                }
                return new[] {playerPos.x, playerPos.y};
            }
            else
            {
                for (var i = -2; i < 3; i++)
                {
                    for (var j = -2; j < 3; j++)
                    {
                        var intPos = new Vector2Int(playerPos.x + i, playerPos.y + j);
                        _freeBlocks.Remove(intPos);
                    }
                }
                return new[] {30, 30};
            }
            
        }

        public static void TeleportPlayer()
        {
            //TODO : might need to be modified if we make enemies respawn. (need to clear area near player) 
            //get a random location.
            var index = Random.Range(0,_freeBlocks.Count);
            var target = _freeBlocks[index];
            
            //teleport the player.
            var dir = NavigationController.GetPlayerPosInGrid() - target;
            _instance.transform.position += new Vector3(dir.x, 0, dir.y);
            
            //Updates : mask and player pos in grid
            UpdateAllMask(target.x,target.y);
            NavigationController.UpdatePlayerPosInGrid(target.x,target.y);
        }

        #region Enemy generation

        [Header("Procedural enemy generation")]
    
        private static List<Vector2Int> _freeBlocks;
        [SerializeField] private ProbabilitySet probaSet;
        [SerializeField] private Transform enemyParent;

        private void PositionEnemy()
        {
            Debug.Log("------ Start Generating Enemy ------");
            var amountToSpawn = (int) (probaSet.enemyApparitionRate * _freeBlocks.Count);
            Debug.Log("freeblocks contains : " + _freeBlocks.Count + " blocks and we have to spawn " + amountToSpawn + " enemies");
            for (var i = 0; i < amountToSpawn; i++)
            {
                //We take an index from the free list
                var index = Random.Range(0, _freeBlocks.Count);
            
                //Get its position
                var gridPos = _freeBlocks[index];
                var pos = new Vector3(gridPos.x, 0.6f, gridPos.y);
            
                //Spawn the enemy
                var go = probaSet.GetRandomEnemy();
                var enemy = Instantiate(go, pos, go.transform.rotation, enemyParent);
                enemy.name = go.name + " " + i;
            
//                Debug.Log("Enemy spawned at " + pos);
                //Remove the block from the free list for further spawn
                _freeBlocks.RemoveAt(index);
            }
        }

        public static void AddFreeBlock(Vector2Int pos) => _freeBlocks.Add(pos);
        
        public static void RemoveFreeBlock(Vector2Int pos) => _freeBlocks.Remove(pos);
        
        #endregion
    
        #region Masking logic

        private void MaskInit()
        {
            var mask =_maskHandler.InitMask();
            for(var i=0; i<mask.GetLength(0); i++)
            {
                for(var j=0; j<mask.GetLength(1); j++)
                {
                    var x = mask[i, j, 0];
                    var y = mask[i, j, 1];
                    _mapBlocks[x,y].SetActive(true);
                    MapsEnvironment[x,y].SetActive(true);
                }
            }
        }

        public static void UpdateMask(int newX, int newZ)
        {
            var updated = _maskHandler.UpdateMap(newX, newZ);
            //Hide the blocks that are not in the mask
            for (var j = 0; j < updated.GetLength(1); j++)
            {
                var x = updated[0, j, 0];
                var y = updated[0, j, 1];
            
                if (!IsInMap(x, y)) continue;
            
                _mapBlocks[x, y].SetActive(false);
                MapsEnvironment[x, y].SetActive(false);
            }
            //Show the new blocks
            for (var j = 0; j < updated.GetLength(1); j++)
            {
                var x = updated[1, j, 0];
                var y = updated[1, j, 1];
                if (!IsInMap(x, y)) continue;
                _mapBlocks[x,y].SetActive(true);
                MapsEnvironment[x,y].SetActive(true);
            }
        }

        private static void UpdateAllMask(int newX, int newY)
        {
            var around = _maskHandler.InitMask(); //get the mask around the player
            var newMask = _maskHandler.GetSurroundingMap(newX, newY);
            
            //Hide the old mask
            for (var i = 0; i < around.GetLength(0); i++)
            {
                for (var j = 0; j < around.GetLength(1); j++)
                {
                    var x = around[i, j, 0];
                    var y = around[i, j, 1];
                    if (!IsInMap(x, y)) continue;
                    _mapBlocks[x, y].SetActive(false);
                    MapsEnvironment[x, y].SetActive(false);
                }
            }
            
            //Show the new mask
            for (var i = 0; i < newMask.GetLength(0); i++)
            {
                for (var j = 0; j < newMask.GetLength(1); j++)
                {
                    var x = newMask[i, j, 0];
                    var y = newMask[i, j, 1];
                    if (!IsInMap(x, y)) continue;
                    _mapBlocks[x, y].SetActive(true);
                    MapsEnvironment[x, y].SetActive(true);
                }
            }
            
            //Update player position for the maskhandler
            _maskHandler.UpdatePlayerPosition(newX, newY);
        }
    
        public static bool IsInMap(int x, int y)
        {
            return x >= 0 && x < GenerationMap.tailleMap && y >= 0 && y < GenerationMap.tailleMap;
        }

        public static bool IsAvailable(GameObject bloc)
        {
            return bloc.GetComponent<environnement_bloc>().get_type() == 1;
        }

        public static bool IsAvailable(int x, int y)
        {
            if (!IsInMap(x, y)) return false; //We make sure he is in the map
        
            return IsAvailable(MapsEnvironment[x, y]);
        }

        #endregion

        #region Getter ressources

        /// <summary>
        /// This method gather all resources of a specific type in an area.
        /// </summary>
        /// <param name="x">x pos of the block</param>
        /// <param name="y">y pos of the block</param>
        /// <param name="range">Range as a block</param>
        /// <param name="type">Type of the resource to get</param>
        /// <returns></returns>
        public static int GetAResourceInArea(int x, int y, int range, ResourceType type)
        {
            if(type == ResourceType.All) throw new Exception("Use GetResourcesInArea instead.");

            var count = 0;
            //Get the blocks around the pos (x,y) with range of range blocks
            for(var i = -range; i <= range; i++)
            {
                for(var j = -range; j <= range; j++)
                {
                    if (i == 0 && j == 0) continue; //Ignore the player tile
                    
                    if (!IsInMap(x + i, y + j)) continue; //make sure there is a block there.
                    var block = MapsEnvironment[x + i, y + j].GetComponent<environnement_bloc>(); 
                    
                    if (block.get_type() == (int) type)
                    {
                        count++; //update count 
                        block.set_type(ResourceType.Free); //Modify the block.
                    }

                }
            }
            return count;
        }

        public static int[] GetAllResourcesInArea(int x, int y, int range)
        {
            var resources = new int[3];
            //Get the blocks around the pos (x,y) with range of range blocks
            for(var i = -range; i <= range; i++)
            {
                for(var j = -range; j <= range; j++)
                {
                    if (i == 0 && j == 0) continue; //Ignore the player tile
                    
                    if (!IsInMap(x + i, y + j)) continue; //make sure there is a block there.
                    var block = MapsEnvironment[x + i, y + j].GetComponent<environnement_bloc>();

                    switch (block.get_type())
                    {
                        case (int) ResourceType.Wood:
                            resources[0]++;
                            break;
                        case (int) ResourceType.Rock:
                            resources[1]++;
                            break;
                        case (int) ResourceType.Food:
                            resources[2]++;
                            break;
                        default:
                            break;
                    }
                    block.set_type(ResourceType.Free); //Modify the block.
                }
            }
            return resources;
        }
        
        #endregion
    }
}
