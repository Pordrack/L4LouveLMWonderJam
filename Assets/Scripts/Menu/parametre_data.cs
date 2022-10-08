using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable	]
public class parametre_data {

	public float sound_value;
	public bool fullscreen;
	public int l,L;

	public parametre_data(loading loading){
		sound_value = loading.sound_value;
		if(loading.dropresol.value == 0){
			l = 640;
			L = 360;
		}else if(loading.dropresol.value == 1){
			l = 1920;
			L = 1080;
		}else{
			l = 3840;
			L = 2160;
		}

		fullscreen = loading.fullscreen;
	}

}
