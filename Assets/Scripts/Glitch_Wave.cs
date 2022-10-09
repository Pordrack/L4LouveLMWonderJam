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
        Start_Wave();
    }

    // Update is called once per frame
    void Update()
    {
        Start_Wave();
    }

    public void Start_Wave(){

        colision=Physics.OverlapBox(gameObject.transform.position, transform.localScale *5.5f, Quaternion.identity, m_LayerMask);

        for(int i = 0; i<colison.length; i++){
            if(Random.Range(probanum,probade))
        }
    }
}
