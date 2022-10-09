using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gather_Effects : Card_Effect
{
    //public Sprite Axe_Icon;
    //public Sprite Pickaxe_Icon;
    //public Sprite Food_Icon;

    public ParameterEntry[] tool_values;
    public ParameterEntry[] ressources_values;
    public int min_range;
    public int max_range;

    public override void OnPlay(Dictionary<string, ParameterEntry> parameters,Card card_scriptable_objects)
    {
       
    }

    public override void OnStart(Dictionary<string, ParameterEntry> parameters, Card card)
    {
        switch(parameters["ressource"].real_value)
        {

        }
    }

    public override void OnTurn(Dictionary<string, ParameterEntry> parameters, Card card)
    {

    }

    public override void OnValueGlitch(Dictionary<string, ParameterEntry> parameters, Card card_scriptable_objects)
    {
        //On "corromp" une des valeurs de la carte au hasard (valeur plus affichée
        int index = Random.Range(0, tool_values.Length);
        tool_values[index].display_value = "υɿwɿO pɿdʇɿυƧ";
        tool_values[index].display_value = "ḧ̶̢̞̠̼̮̤̞̮̳̲̬̳́̌̽͂͝ͅų̵̢̢̞̠͙̖̥̼̠̞͍̹̉̐̾͒̀̕g̸̛̯̮͙̋̀̇͠o̵͈̼͐̍̅̔̎̆͐̑̓̃̚̚";

        //On retire les valeurs de l'outil
        index = Random.Range(0, tool_values.Length);
        parameters["tool"] = tool_values[index];
        parameters["ressource"] = tool_values[index];
        OnStart(parameters, card_scriptable_objects);
    }
}
