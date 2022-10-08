using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public static class save_systeme
{

	public static void save_parametre(loading loading){

		BinaryFormatter formatter = new BinaryFormatter();

		string path = Application.persistentDataPath + "/paramtre.fun";
		FileStream stream = new FileStream(path, FileMode.Create);

		parametre_data data = new parametre_data(loading);

		formatter.Serialize(stream, data);
		stream.Close();


	}

	public static parametre_data Load_parametre(){
		string path = Application.persistentDataPath + "/paramtre.fun";

		if(File.Exists(path)){

			BinaryFormatter formatter = new BinaryFormatter();
			FileStream stream = new FileStream(path, FileMode.Open);
			parametre_data data = formatter.Deserialize(stream) as parametre_data;
			stream.Close();

			return data;

		}else{
			Debug.LogError("Save file not found in" + path);
			return null;
		}

	}

}
