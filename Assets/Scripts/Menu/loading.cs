using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class loading : MonoBehaviour {

	public Slider slider;
	public string sceneIndex;
	public GameObject loadingScreen;
	public Text load_txt;
	public string[] truc;

	// option

	public Dropdown dropresol;
	public AudioSource audio;
	public Slider slide_sound;
	public Text txt_sound;
	public Toggle toggle_full;
	public float sound_value;
	public bool fullscreen = true;
	public AudioSource[] audio_source;



	// Use this for initialization
	public void LoadLevel (int sceneIndex){
		StartCoroutine(Troll(sceneIndex));
		StartCoroutine(load_slider(sceneIndex));
		loadingScreen.SetActive(true);
	}

	IEnumerator LoadSAsync(int sceneIndex){
		AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);

		loadingScreen.SetActive(true);

		while(!operation.isDone){

			float progress = Mathf.Clamp01(operation.progress / 0.9f);
			slider.value = progress;
			yield return null;
		}
	}

	void Start(){
		setsound();
		//LoadParametre();


	}

	void Update(){
		if(toggle_full.isOn){
			fullscreen = true;
		}else{
			fullscreen = false;
		}

		

	}

	public void setresolution(){
		

		switch (dropresol.value){
			case 0:
				Screen.SetResolution(640,360,fullscreen);
				break;

			case 1:
				Screen.SetResolution(1920,1080,fullscreen);
				break;

			case 2:
				Screen.SetResolution(3840,2160,fullscreen);
				break;
		}
	}

	public void setsound(){
		sound_value = slide_sound.value;
		audio.volume = sound_value;
		txt_sound.text = (audio.volume*100).ToString("00") + "%";
	}

	public void setsoundall(){

		sound_value = slide_sound.value;
		AudioListener.volume = sound_value;
		txt_sound.text = (sound_value*100).ToString("00") + "%";
		
	}


	public void SaveParametre(){

		save_systeme.save_parametre(this);

	}

	public void setfullscreen(){
		Screen.fullScreen = !Screen.fullScreen;
	}

	public void LoadParametre(){

		parametre_data data = save_systeme.Load_parametre();

		sound_value = data.sound_value;
		slide_sound.value = sound_value;
		audio.volume = sound_value;
		txt_sound.text = (audio.volume*100).ToString("00") + "%";

		fullscreen = data.fullscreen;

		Screen.SetResolution(data.l,data.L,data.fullscreen);

	}

	 IEnumerator Troll(int sceneIndex)
    {
        //yield on a new YieldInstruction that waits for 5 seconds.
        yield return new WaitForSeconds(10);
        StartCoroutine(LoadSAsync(sceneIndex));

    }

    IEnumerator load_slider(int sceneIndex)
    {
        //yield on a new YieldInstruction that waits for 5 seconds.
        yield return new WaitForSeconds(Random.Range(0.2f,1.0f));
        slider.value = Random.Range(0.0f,1.0f);
        StartCoroutine(load_slider(sceneIndex));
        load_txt.text = truc[Random.Range(0,truc.Length)];

    }

}
