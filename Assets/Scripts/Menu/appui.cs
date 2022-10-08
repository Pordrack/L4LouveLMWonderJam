using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class appui : MonoBehaviour
{

	bool ok = true;
	public GameObject txt;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown("space")){
        	gameObject.SetActive(false);
        }

        if(ok == true){
        	ok = false;
        	StartCoroutine (clinionte ());
        }

    }

    IEnumerator clinionte(){
		yield return new WaitForSeconds (1f);
		txt.SetActive(!txt.activeInHierarchy);
		ok = true;
    }
}
