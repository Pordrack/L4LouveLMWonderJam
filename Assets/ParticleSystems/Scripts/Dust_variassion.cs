using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dust_variassion : MonoBehaviour
{

    int nbr_particule = 100;
    private ParticleSystem ps;

    // Start is called before the first frame update
    void Start()
    {
        ps = GetComponent<ParticleSystem>();
        StartCoroutine(alea_change());
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    IEnumerator alea_change()
    {

        //yield on a new YieldInstruction that waits for 5 seconds.
        yield return new WaitForSeconds(Random.Range(1.0f,5.0f));

        var emission = ps.emission;
        emission.rateOverTime = nbr_particule;

        if(Random.Range(0,100)>50){
            nbr_particule += Random.Range(1,11);
        }else{
            nbr_particule -= Random.Range(1,11);
        }
        StartCoroutine(alea_change());

    }
}
