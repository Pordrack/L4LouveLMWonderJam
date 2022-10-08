using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food_Effects : Card_Effect
{
    public Sprite Poutine_Sprite;
    public override void OnPlay(Dictionary<string, string[]> parameters,Card card_scriptable_objects)
    {
        Debug.Log("Old faim : " + Stats_Perso.Instance._faim.ToString());
        Debug.Log("Old action : " + Stats_Perso.Instance._action.ToString());
        //Recupere la valeur de "faim" de la carte
        int bouffe = int.Parse(parameters["faim"][1]);
        Stats_Perso.Instance.add_faim(bouffe);
        //Récupère le type de nourriture (poutine ou carots)
        string typeBouffe = parameters["food"][1];
        //Si c'est une poutine on baisse l'energie de moitié
        if (typeBouffe == "poutine")
        {
            Stats_Perso.Instance.down_action(Stats_Perso.Instance._action / 2);
        }
        Debug.Log("Nv faim : " + Stats_Perso.Instance._faim.ToString());
        Debug.Log("Nv action : " + Stats_Perso.Instance._action.ToString());
    }

    public override void OnValueGlitch(Dictionary<string, string[]> parameters, Card card_scriptable_objects)
    {
        //Redefini le paramètre "good" de la carte avec Poutine en valeur affiché et poutine en valeur "réelle"
        parameters["food"] = new string[] { "Poutine", "poutine" };
        //Remplace paramètre faim avec "infinity" en valeur affiché et beaucoup en valeur réelle
        parameters["faim"] = new string[] { "∞", "9999" };
        //Change l'image de la carte
        card_scriptable_objects.Icon = Poutine_Sprite;
    }
}
