using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using static Stats_Perso;

public class Curseur_3D : MonoBehaviour
{
    public Stats_Perso perso;

    public GameObject cylindre_santee;
    public GameObject txt_santee;

    public GameObject cylindre_faim;
    public GameObject txt_faim;

    public GameObject cylindre_action;
    public GameObject txt_action;

    float max_santee;
    float santee { get; set; }

    float max_faim;
    float faim { get; set; }

    float max_action;
    float action { get; set; }

    float scale_obj;

    

    // Start is called before the first frame update
    void Start()
    {
        max_santee = perso._max_santee;
        max_faim = perso._max_faim;
        max_action = perso._max_action;

        santee = max_santee;
        faim = max_faim;
        action = max_action;

        Debug.Log("Max : " + max_santee);
        Debug.Log("Val : " + santee);
    }

    // Update is called once per frame
    void Update()
    {
        santee = perso._santee;
        faim = perso._faim;
        action = perso._action;

        charge_scale();
    }
    

    //public void set_santee(float valeur){
    //    santee = valeur;
    //    charge_scale();
    //    if(santee<0){
    //        santee = 0;
    //    }
    //}

    //public float get_santee(){
    //    return santee;
    //}

    public void charge_scale(){
        cylindre_santee.transform.localScale = new Vector3(santee / max_santee, 1, 1);
        cylindre_faim.transform.localScale = new Vector3(faim / max_faim, 1, 1);
        cylindre_action.transform.localScale = new Vector3(action / max_action, 1, 1);
       
        txt_change();
    }

    public void txt_change(){
        string affiche_santee = santee + " / " + max_santee;
        string affiche_faim = faim + " / " + max_faim;
        string affiche_action = action + " / " + max_action;

        txt_santee.GetComponent<TextMesh>().text = affiche_santee;
        txt_faim.GetComponent<TextMesh>().text = affiche_faim;
        txt_action.GetComponent<TextMesh>().text = affiche_action;
    }
}
