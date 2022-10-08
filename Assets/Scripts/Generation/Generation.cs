using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generation : MonoBehaviour
{

    public int taille;
    public GameObject bloc;
    GameObject[,]mapsbloc;

    public GameObject Environnement;
    public GameObject[,] mapsEnvironnement;


    // Start is called before the first frame update
    void Start()
    {
        mapsbloc = new GameObject[taille,taille];

        for(int i = 0; i<taille; i++){
            for(int v = 0; v<taille; v++){
                GameObject go = Instantiate(bloc, new Vector3(i, 0, v), Quaternion.identity,transform) as GameObject;
                go.transform.localScale = Vector3.one;
                mapsbloc[i,v] = go;
                if(v==0 || i == 0 || i == taille-1 || v == taille-1){
                    mapsbloc[i,v].GetComponent<bloc>().set_type(0);
                }
            }
        }
        procedural();

        mapsEnvironnement = new GameObject[taille,taille];

        for(int i = 0; i<taille; i++){
            for(int v = 0; v<taille; v++){
                GameObject go = Instantiate(Environnement, new Vector3(i, 1, v), Quaternion.identity,transform) as GameObject;
                go.transform.localScale = Vector3.one;
                mapsEnvironnement[i,v] = go;
                if(v==0 || i == 0 || i == taille-1 || v == taille-1){
                    mapsEnvironnement[i,v].GetComponent<environnement_bloc>().set_type(0);
                }
            }
        }
        procedural_generation_environnemnt();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void procedural(){
        

        for(int i = 1; i<taille-1; i++){
            for(int v = 1; v<taille-1; v++){
                if((i+v)<=taille-1){
                    if(mapsbloc[i-1,v].GetComponent<bloc>().get_type() == 0 || mapsbloc[i,v-1].GetComponent<bloc>().get_type() == 0){
                        if(Random.Range(0, 100)>50){
                           mapsbloc[taille-i-1,taille-v-1].GetComponent<bloc>().set_type(3); 
                           mapsbloc[i,v].GetComponent<bloc>().set_type(3); 
                        }else{
                             mapsbloc[taille-i-1,taille-v-1].GetComponent<bloc>().set_type(0); 
                             mapsbloc[i,v].GetComponent<bloc>().set_type(0); 
                        }

                    }else{
                        procedural_1(i,v);
                        
                    }
                }
            }
        }

        for(int i = 1; i<taille-1; i++){
            for(int v = 1; v<taille-1; v++){
                if((i+v)<=taille-1){
                    if(mapsbloc[i-1,v].GetComponent<bloc>().get_type() == 0 || mapsbloc[i,v-1].GetComponent<bloc>().get_type() == 0){
                        if(Random.Range(0, 100)>50){
                           mapsbloc[i,v].GetComponent<bloc>().set_type(3); 
                        }else{
                             mapsbloc[i,v].GetComponent<bloc>().set_type(0); 
                        }

                    }else{
                        procedural_2(i,v);
                    }
                }

            }
        }

        for(int i = 0; i<taille; i++){
            for(int v = 0; v<taille; v++){
                mapsbloc[i,v].GetComponent<bloc>().set_visibility();
            }
        }
    }


    void procedural_1(int i, int v){
        mapsbloc[i,v].GetComponent<bloc>().set_type(Random.Range(1, 3));
        mapsbloc[taille-i-1,taille-v-1].GetComponent<bloc>().set_type(Random.Range(1, 3));
    }

    void procedural_2(int i,int v){
        mapsbloc[i,v].GetComponent<bloc>().set_type(Random.Range(1, 3));
    }

    void procedural_generation_environnemnt(){
        for(int i = 0; i<taille; i++){
            for(int v = 0; v<taille; v++){
                switch(mapsbloc[i,v].GetComponent<bloc>().get_type()){
                    case 0:
                        mapsEnvironnement[i,v].GetComponent<environnement_bloc>().set_type(0); 
                    break;

                    case 1:
                        switch(Random.Range(0, 5)){
                            case 0:
                                mapsEnvironnement[i,v].GetComponent<environnement_bloc>().set_type(1); 
                            break;

                            case 1:
                                mapsEnvironnement[i,v].GetComponent<environnement_bloc>().set_type(2); 
                            break;

                            case 2:
                                mapsEnvironnement[i,v].GetComponent<environnement_bloc>().set_type(2); 
                            break;

                            case 3:
                                mapsEnvironnement[i,v].GetComponent<environnement_bloc>().set_type(3); 
                            break;

                            case 4:
                                if(Random.Range(0, 100)>75){
                                    mapsEnvironnement[i,v].GetComponent<environnement_bloc>().set_type(4); 
                                }
                            break;
                        }
                    break;

                    case 2:
                    break;

                    case 3:
                    break;
                }
            }
        }








        for(int i = 0; i<taille; i++){
            for(int v = 0; v<taille; v++){
                mapsEnvironnement[i,v].GetComponent<environnement_bloc>().set_visibility();
            }
        }
    }
}
