using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Glitch_Wave : MonoBehaviour
{

    public LayerMask m_LayerMask;
    public Collider[] colision;
    public int probanum,probade;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Start_Wave(){   

        colision=Physics.OverlapBox(gameObject.transform.position, transform.localScale *5.5f, Quaternion.identity, m_LayerMask);

        for(int i = 0; i<colision.Length; i++){
            if(Random.Range(probanum,probade+1)==1){
                Debug.Log("Switch_Mode");
                colision[i].GetComponent<IA.Brain>().GlitchSwitch();
            }
        }
    }

}
