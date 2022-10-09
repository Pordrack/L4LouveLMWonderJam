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
    [Tooltip("Nombre de buches par arbre, de carotte par buissons etc.")]
    public MinMaxValue Ressource_Unit_Per_Map_Element;
    //public MinMaxValue Ressource_Unit_Per_Map_Element_Nourriture;

    //[Tooltip("Entre 0 et 1, proba que l'outil sois faible et capable de recup qu'une ressource")]
    //public float weak_probability; 

    public override void OnPlay(Dictionary<string, ParameterEntry> parameters,Card card_scriptable_objects)
    {
        bool weak = parameters["weak"].real_value == "1";
        int range = int.Parse(parameters["range"].real_value);
        switch (parameters["ressource"].real_value)
        {
            case "wood":
                Gather(Generation.ResourceType.Wood,range,weak);
                break;
            case "stone":
                Gather(Generation.ResourceType.Rock, range, weak);
                break;
            case "food":
                Gather(Generation.ResourceType.Food, range, weak);
                break;
            default:
                Gather(Generation.ResourceType.Wood, range, weak);
                Gather(Generation.ResourceType.Rock, range, weak);
                Gather(Generation.ResourceType.Food, range, weak);
                break;
        }
        Ressources.Instance.update_nourriture(Ressources.Instance._nourriture);
        Ressources.Instance.update_bois(Ressources.Instance._bois);
        Ressources.Instance.update_pierre(Ressources.Instance._pierre);
    }

    public override void OnStart(Dictionary<string, ParameterEntry> parameters, Card card,CardScript card_script)
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

        ////Regarde si le truc est weak
        //if (Random.Range(0.0f, 1.0F) < weak_probability)
        //{
        //    parameters["weak"] = new ParameterEntry { display_value = " en papier", real_value = "1" };
        //}
    }

    public override void OnTurn(Dictionary<string, ParameterEntry> parameters, Card card, CardScript card_script)
    {

    }

    public override void OnValueGlitch(Dictionary<string, ParameterEntry> parameters, Card card_scriptable_objects)
    {
        //On "corromp" une des valeurs de la carte au hasard (valeur plus affichée
        int index = Random.Range(0, tool_values.Length);
        tool_values[index].display_value = "υɿwɿO pɿdʇɿυƧ";
        tool_values[index].display_value = "[///@^{{}";

        OnStart(parameters, card_scriptable_objects,null);
    }

    //Pete les ressources et les ajoutes aux ressources du joueur
    public void Gather(Generation.ResourceType ressource_type,int range,bool weak)
    {
        Vector2Int ppos = Player.NavigationController.GetPlayerPosInGrid();
        int amount = 0;
        //if (weak && )
        //{
        //    amount = ValueRandomizer.RandomizeValue(Ressource_Unit_Per_Map_Element);
        //}
        //else if(!weak)
        //{
        amount = Generation.GenerationMap.GetAResourceInArea(ppos.x, ppos.y, range, ressource_type) * ValueRandomizer.RandomizeValue(Ressource_Unit_Per_Map_Element);
        //}

        switch (ressource_type)
        {
            case Generation.ResourceType.Wood:
                Ressources.Instance.add_bois(amount);
                break;
            case Generation.ResourceType.Rock:
                Ressources.Instance.add_pierre(amount);
                break;
            default :
                Ressources.Instance.add_nourriture(amount*3);
                break;
        }
    }
}
