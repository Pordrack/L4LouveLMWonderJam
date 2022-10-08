using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Interface que tous les scripts d'effets de cartes doivent avoir, renseigne sur ce qu'il doivent forcement avoir
public abstract class Card_Effect : MonoBehaviour
{
    //Clé unique qui permettra de retrouver l'instance de cardd effects recherché par le scriptable object
    public string Effects_Key;

    void Start()
    {
        if (CardScript.Card_Effects_Dictionnary == null)
        {
            CardScript.Card_Effects_Dictionnary = new Dictionary<string, Card_Effect>();
        }
        CardScript.Card_Effects_Dictionnary.Add(Effects_Key, this);
    }

    public abstract void OnPlay(Dictionary<string,string[]> parameters, Card card);
    public void OnGlitch(Dictionary<string, string[]> parameters, Card card)
    {
        float random_value = Random.Range(0.0f, 1.0f);
        if (random_value <= 1)
        {
            OnValueGlitch(parameters,card);
        }
    }
    public abstract void OnValueGlitch(Dictionary<string, string[]> parameters,Card card);
}
