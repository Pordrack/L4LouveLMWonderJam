using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class playbutton : MonoBehaviour {

public string scenename;
public bool  showGUI;
public AudioClip sounds;
public AudioSource sound;

void Awake (){
}

void  Update (){
	if(showGUI == true){
		LOAD_SCENE();
	}
}

void  LOAD_SCENE (){
	SceneManager.LoadScene(scenename);
}

void  OnTriggerEnter ( Collider hit  ){
	if(hit.gameObject.tag == "Player"){
		showGUI = true;
	}
}

void  OnTriggerExit ( Collider hit  ){
	if(hit.gameObject.tag == "Player"){
		showGUI = false;
	}
}
}