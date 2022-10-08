using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Generation : MonoBehaviour
{

    public static int tailleMap = 100;
    public GameObject bloc; //prefab of the block
    public GameObject[,] MapBlocks;
    

    public GameObject Environnement;
    public static GameObject[,] MapsEnvironment; //Position of the environment elements (tree, rocks, etc ...)
    
    public static event Action OnGenerationComplete;

    // Start is called before the first frame update
    void Start()
    {
        MapBlocks = new GameObject[tailleMap,tailleMap];

        for(int i = 0; i<tailleMap; i++){
            for(int v = 0; v<tailleMap; v++){
                GameObject go = Instantiate(bloc, new Vector3(i, 0, v), Quaternion.identity,transform) as GameObject;
                go.transform.localScale = Vector3.one;
                MapBlocks[i,v] = go;
                if(v==0 || i == 0 || i == tailleMap-1 || v == tailleMap-1){
                    MapBlocks[i,v].GetComponent<bloc>().set_type(0);
                }
            }
        }
        procedural();

        MapsEnvironment = new GameObject[tailleMap,tailleMap];

        for(int i = 0; i<tailleMap; i++){
            for(int v = 0; v<tailleMap; v++){
                GameObject go = Instantiate(Environnement, new Vector3(i, 1, v), Quaternion.identity,transform) as GameObject;
                go.transform.localScale = Vector3.one;
                MapsEnvironment[i,v] = go;
                if(v==0 || i == 0 || i == tailleMap-1 || v == tailleMap-1){
                    MapsEnvironment[i,v].GetComponent<environnement_bloc>().set_type(0);
                }
            }
        }
        procedural_generation_environnemnt();
        
        //Center the plate (so that (0,0) is in the center of the plate)
        //transform.position = new Vector3(-tailleMap/2+1,0,-tailleMap/2 + 1);
        
        OnGenerationComplete?.Invoke();
    }

    void procedural(){
        

        for(int i = 1; i<tailleMap-1; i++){
            for(int v = 1; v<tailleMap-1; v++){
                if((i+v)<=tailleMap-1){
                    if(MapBlocks[i-1,v].GetComponent<bloc>().get_type() == 0 || MapBlocks[i,v-1].GetComponent<bloc>().get_type() == 0){
                        if(Random.Range(0, 100)>50){
                           MapBlocks[tailleMap-i-1,tailleMap-v-1].GetComponent<bloc>().set_type(3); 
                           MapBlocks[i,v].GetComponent<bloc>().set_type(3); 
                        }else{
                             MapBlocks[tailleMap-i-1,tailleMap-v-1].GetComponent<bloc>().set_type(0); 
                             MapBlocks[i,v].GetComponent<bloc>().set_type(0); 
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
                    if(MapBlocks[i-1,v].GetComponent<bloc>().get_type() == 0 || MapBlocks[i,v-1].GetComponent<bloc>().get_type() == 0){
                        if(Random.Range(0, 100)>50){
                           MapBlocks[i,v].GetComponent<bloc>().set_type(3); 
                        }else{
                             MapBlocks[i,v].GetComponent<bloc>().set_type(0); 
                        }

                    }else{
                        procedural_2(i,v);
                    }
                }

            }
        }

        for(int i = 0; i<tailleMap; i++){
            for(int v = 0; v<tailleMap; v++){
                MapBlocks[i,v].GetComponent<bloc>().set_visibility();
            }
        }
    }


    void procedural_1(int i, int v){
        MapBlocks[i,v].GetComponent<bloc>().set_type(Random.Range(1, 3));
        MapBlocks[tailleMap-i-1,tailleMap-v-1].GetComponent<bloc>().set_type(Random.Range(1, 3));
    }

    void procedural_2(int i,int v){
        MapBlocks[i,v].GetComponent<bloc>().set_type(Random.Range(1, 3));
    }

    void procedural_generation_environnemnt(){
        for(int i = 0; i<tailleMap; i++){
            for(int v = 0; v<tailleMap; v++){
                switch(MapBlocks[i,v].GetComponent<bloc>().get_type()){
                    case 0:
                        MapsEnvironment[i,v].GetComponent<environnement_bloc>().set_type(0); 
                    break;

                    case 1:
                        switch(Random.Range(0, 5)){
                            case 0:
                                MapsEnvironment[i,v].GetComponent<environnement_bloc>().set_type(1); 
                            break;

                            case 1:
                                MapsEnvironment[i,v].GetComponent<environnement_bloc>().set_type(2); 
                            break;

                            case 2:
                                MapsEnvironment[i,v].GetComponent<environnement_bloc>().set_type(2); 
                            break;

                            case 3:
                                MapsEnvironment[i,v].GetComponent<environnement_bloc>().set_type(3); 
                            break;

                            case 4:
                                if(Random.Range(0, 100)>75){
                                    MapsEnvironment[i,v].GetComponent<environnement_bloc>().set_type(4); 
                                }else{
                                    MapsEnvironment[i,v].GetComponent<environnement_bloc>().set_type(1); 
                                }
                            break;
                        }
                    break;

                    case 2:
                        MapsEnvironment[i,v].GetComponent<environnement_bloc>().set_type(1); 
                    break;

                    case 3:
                        MapsEnvironment[i,v].GetComponent<environnement_bloc>().set_type(1); 
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
}
