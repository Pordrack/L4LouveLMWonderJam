using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Ending_Button : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void button_change_scene(int sceneIndex){
        StartCoroutine(LoadSAsync(sceneIndex));
    }


    IEnumerator LoadSAsync(int sceneIndex){
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);


        while(!operation.isDone){
            yield return null;
        }
    }
}
