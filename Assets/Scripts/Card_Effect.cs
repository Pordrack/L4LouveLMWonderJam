using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Interface que tous les scripts d'effets de cartes doivent avoir, renseigne sur ce qu'il doivent forcement avoir
public abstract class Card_Effect : MonoBehaviour
{
    //Cl� unique qui permettra de retrouver l'instance de cardd effects recherch� par le scriptable object
    public string Effects_Key;

    void Start()
    {
        if (CardScript.Card_Effects_Dictionnary == null)
        {
            CardScript.Card_Effects_Dictionnary = new Dictionary<string, Card_Effect>();
        }
        CardScript.Card_Effects_Dictionnary.Add(Effects_Key, this);
    }

    public abstract void OnPlay(Card card);
    public abstract void OnGlitch(Card card);
}
