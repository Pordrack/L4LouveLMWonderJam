using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Glitch_Wave : MonoBehaviour
{
    
    
    public LayerMask m_LayerMask;
    private Collider[] colision;
    public int probanum,probade;


    public void Start_Wave(){   

        colision=Physics.OverlapBox(gameObject.transform.position, transform.localScale *5.5f, Quaternion.identity, m_LayerMask);

        for(var i = 0; i<colision.Length; i++){
            if(Random.Range(probanum,probade+1)==1){
                Debug.Log("Switch_Mode");
                print($"On collide avec {colision[i].name}");
                colision[i].GetComponent<IA.Brain>().GlitchSwitch();
            }
        }
    }

}
