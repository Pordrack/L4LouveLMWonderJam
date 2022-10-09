using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Unlimited_Load : MonoBehaviour
{

    public Slider slider;
    public Text load_txt;
    public string[] truc;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(load_slider());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator load_slider()
    {
        //yield on a new YieldInstruction that waits for 5 seconds.
        while (true)
        {
            slider.value = Random.Range(0.0f, 1.0f);
            load_txt.text = truc[Random.Range(0, truc.Length)];
            yield return new WaitForSeconds(Random.Range(0.2f, 1.0f));
        }
        
    }
}
