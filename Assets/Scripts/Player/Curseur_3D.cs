using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Curseur_3D : MonoBehaviour
{


    public GameObject cylindre;
    public GameObject txt;
    float valeur;
    float valeur_max=200;
    float scale_obj;

    // Start is called before the first frame update
    void Start()
    {
        valeur = valeur_max;
    }

    // Update is called once per frame
    void Update()
    {
        valeur --;
        charge_scale();
    }
    

    public void set_value(float valeur){
        this.valeur = valeur;
        charge_scale();
        if(valeur<0){
            valeur = 0;
        }
    }

    public float get_value(){
        return valeur;
    }

    public void charge_scale(){
        if(valeur<0){
            valeur = 0;
        }
        cylindre.transform.localScale = new Vector3(valeur/valeur_max,1,1);
        txt_change();
    }

    public void txt_change(){
        string affiche = valeur + " / " + valeur_max;
        txt.GetComponent<TextMesh>().text = affiche;
    }
}
