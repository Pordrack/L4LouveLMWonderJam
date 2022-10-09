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
    [Tooltip("Nombre de buches par arbre, de carotte par buissons etc...")]
    public MinMaxValue Ressource_Unit_Per_Map_Element;

    [Tooltip("Entre 0 et 1, proba que l'outil sois faible et capable de recup qu'une ressource")]
    public float weak_probability; 

    public override void OnPlay(Dictionary<string, ParameterEntry> parameters,Card card_scriptable_objects)
    {
        bool weak = parameters["weak"].real_value == "1";
        int range = int.Parse(parameters["range"].real_value);
        switch (parameters["ressource"].real_value)
        {
            case "wood":
                Gather(2,range,weak);
                break;
            case "stone":
                Gather(3, range, weak);
                break;
            case "food":
                Gather(4, range, weak);
                break;
            default:
                Gather(2, range, weak);
                Gather(3, range, weak);
                Gather(4, range, weak);
                break;
        }
    }

    public override void OnStart(Dictionary<string, ParameterEntry> parameters, Card card)
    {
        int index = Random.Range(0, tool_values.Length);
        parameters["tool"] = tool_values[index];
        parameters["ressource"] = ressources_values[index];

        int range = Random.Range(min_range, max_range + 1);


        parameters["range"] = new ParameterEntry { display_value = range.ToString(), real_value = range.ToString() };

        //On aune chance sur 5 de corrompre l'affichage
        if (Random.Range(0, 5) == 0)
        {
            parameters["range"] = new ParameterEntry { display_value = "⛄", real_value = range.ToString() };
        }

        //Regarde si le truc est weak
        if (Random.Range(0.0f, 1.0F) < weak_probability)
        {
            parameters["weak"] = new ParameterEntry { display_value = " en papier", real_value = "1" };
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

        OnStart(parameters, card_scriptable_objects);
    }

    //Pete les ressources et les ajoutes aux ressources du joueur
    public void Gather(int ressource_type,int range,bool weak)
    {
        int amount = 0;
        if (weak && true)
        {
            amount = ValueRandomizer.RandomizeValue(Ressource_Unit_Per_Map_Element);
        }
        else if(!weak)
        {
            amount = 2*ValueRandomizer.RandomizeValue(Ressource_Unit_Per_Map_Element);
        }

        switch (ressource_type)
        {
            case 1:
                Ressources.Instance.add_bois(amount);
                break;
            case 2:
                Ressources.Instance.add_pierre(amount);
                break;
            default :
                Ressources.Instance.add_nourriture(amount);
                break;
        }
    }
}
