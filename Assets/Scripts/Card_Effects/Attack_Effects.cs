using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack_Effects : Card_Effect
{
    //public Sprite Axe_Icon;
    //public Sprite Pickaxe_Icon;
    //public Sprite Food_Icon;

    public MinMaxValue minmax_range;


    //[Tooltip("Entre 0 et 1, proba que l'outil sois faible et capable de recup qu'une ressource")]
    //public float weak_probability; 

    public override void OnPlay(Dictionary<string, ParameterEntry> parameters,Card card_scriptable_objects)
    {
        int range = int.Parse(parameters["range"].real_value);
        IA.EnemyManager.Singleton.KillEnemiesInAnArea(IA.EnemyManager.Singleton.GetPlayerPosition(), range);
    }

    public override void OnStart(Dictionary<string, ParameterEntry> parameters, Card card, CardScript card_script)
    {

        int range = ValueRandomizer.RandomizeValue(minmax_range);


        parameters["range"] = new ParameterEntry { display_value = range.ToString(), real_value = range.ToString() };

        //On aune chance sur 5 de corrompre l'affichage
        if (Random.Range(0, 5) == 0)
        {
            parameters["range"] = new ParameterEntry { display_value = "#ERRROR#", real_value = range.ToString() };
        }
    }

    public override void OnTurn(Dictionary<string, ParameterEntry> parameters, Card card, CardScript card_script)
    {

    }

    public override void OnValueGlitch(Dictionary<string, ParameterEntry> parameters, Card card_scriptable_objects)
    {
        if(Random.Range(0,3)==0)
            card_scriptable_objects.Name = "01100011 01101111 01110101 01100011 01101111 01110101";

        OnStart(parameters, card_scriptable_objects,null);
    }
}
