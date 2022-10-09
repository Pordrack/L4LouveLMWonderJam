using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food_Effects : Card_Effect
{
    public Sprite Poutine_Sprite;
    public override void OnPlay(Dictionary<string, ParameterEntry> parameters,Card card_scriptable_objects)
    {
        Debug.Log("Old faim : " + Stats_Perso.Instance._faim.ToString());
        Debug.Log("Old action : " + Stats_Perso.Instance._action.ToString());
        //Recupere la valeur de "faim" de la carte
        int bouffe = int.Parse(parameters["faim"].real_value);
        Stats_Perso.Instance.add_faim(bouffe);
        //Récupère le type de nourriture (poutine ou carots)
        string typeBouffe = parameters["food"].real_value;
        //Si c'est une poutine on baisse l'energie de moitié
        if (typeBouffe == "poutine")
        {
            Stats_Perso.Instance.down_action(Stats_Perso.Instance._action / 2);
        }
        Debug.Log("Nv faim : " + Stats_Perso.Instance._faim.ToString());
        Debug.Log("Nv action : " + Stats_Perso.Instance._action.ToString());
    }

    public override void OnStart(Dictionary<string, ParameterEntry> parameters, Card card)
    {

    }

    public override void OnTurn(Dictionary<string, ParameterEntry> parameters, Card card)
    {

    }

    public override void OnValueGlitch(Dictionary<string, ParameterEntry> parameters, Card card_scriptable_objects)
    {
        //Redefini le paramètre "food" de la carte avec Poutine en valeur affiché et poutine en valeur "réelle"
        parameters["food"] = new ParameterEntry { display_value = "Poutine",real_value="poutine" };
        //Remplace paramètre faim avec "infinity" en valeur affiché et beaucoup en valeur réelle
        ParameterEntry newValue = new ParameterEntry { display_value = "Poutine", real_value = "poutine" };
        parameters["faim"] = new ParameterEntry { display_value = "∞", real_value = "9999" }; 
        //Change l'image de la carte
        card_scriptable_objects.Icon = Poutine_Sprite;
    }
}
