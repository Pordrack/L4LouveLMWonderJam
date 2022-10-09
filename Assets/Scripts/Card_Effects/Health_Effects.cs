using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health_Effects : Card_Effect
{
    public Sprite Satan_Sprite;
    public override void OnPlay(Dictionary<string, ParameterEntry> parameters,Card card_scriptable_objects)
    {
        Debug.Log("Old health : " + Stats_Perso.Instance._santee.ToString());
        Debug.Log("Old action : " + Stats_Perso.Instance._action.ToString());

        //Récupère le type de val montée avec la carte
        string typePlus = parameters["item2"].real_value;

        //Recupere la valeur de stat remonté grace à la carte
        int plus = int.Parse(parameters["quantity2"].real_value);
        //Recupere la valeur de stat décendue avec la carte
        int moins = int.Parse(parameters["quantity"].real_value);

        if (typePlus == "points de vie")
        {
            Stats_Perso.Instance.add_santee(plus);
            Ressources.Instance.down_nourriture(moins);
        }
        else
        {
            Stats_Perso.Instance.add_action(plus);
            Stats_Perso.Instance.down_santee(moins);
        }

        Debug.Log("Nv santee : " + Stats_Perso.Instance._santee.ToString());
        Debug.Log("Nv action : " + Stats_Perso.Instance._action.ToString());
    }

    public override void OnStart(Dictionary<string, ParameterEntry> parameters, Card card, CardScript card_script)
    {

    }

    public override void OnTurn(Dictionary<string, ParameterEntry> parameters, Card card, CardScript card_script)
    {

    }

    public override void OnValueGlitch(Dictionary<string, ParameterEntry> parameters, Card card_scriptable_objects)
    {
        //Redefini les paramètres  de la carte pour avoir la carte sacrifice satanique

        parameters["quantity"] = new ParameterEntry { display_value = "30",real_value="30" };
        parameters["item"] = new ParameterEntry { display_value = "points de vie", real_value = "points de vie" };
        parameters["quantity2"] = new ParameterEntry { display_value = "60", real_value = "60" };
        parameters["item2"] = new ParameterEntry { display_value = "points d'action", real_value = "points d'action" };
        //Change l'image de la carte
        card_scriptable_objects.Icon = Satan_Sprite;
        card_scriptable_objects.Name = "Sacrifice satanique";
    }
}
