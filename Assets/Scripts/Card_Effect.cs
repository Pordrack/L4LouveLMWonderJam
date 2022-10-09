using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Interface que tous les scripts d'effets de cartes doivent avoir, renseigne sur ce qu'il doivent forcement avoir
public abstract class Card_Effect : MonoBehaviour
{
    //Clé unique qui permettra de retrouver l'instance de cardd effects recherché par le scriptable object
    public Effect_Key_Enum Effects_Key;

    void Start()
    {
        if (CardScript.Card_Effects_Dictionnary == null)
        {
            CardScript.Card_Effects_Dictionnary = new Dictionary<Effect_Key_Enum, Card_Effect>();
        }
        CardScript.Card_Effects_Dictionnary.Add(Effects_Key, this);
    }

    public abstract void OnStart(Dictionary<string, ParameterEntry> parameters, Card card);

    public abstract void OnPlay(Dictionary<string, ParameterEntry> parameters, Card card);
    public void OnGlitch(Dictionary<string, ParameterEntry> parameters, Card card,CardScript card_script)
    {
        float random_value = Random.Range(0.0f, 1.0f);
        if (random_value <= 1) //Evolue en version glitchée
        {
            OnValueGlitch(parameters,card);
        }
        else //Est retirée
        {
            card_script.Card_Scriptable_Object = Instantiate(HandScript.Instance.GetRandomCardTemplate());
        }
    }

    public abstract void OnTurn(Dictionary<string, ParameterEntry> parameters, Card card);

    public abstract void OnValueGlitch(Dictionary<string, ParameterEntry> parameters,Card card);
}
