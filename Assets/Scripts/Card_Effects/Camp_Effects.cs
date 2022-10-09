using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camp_Effects : Card_Effect
{
    //public Sprite Axe_Icon;
    //public Sprite Pickaxe_Icon;
    //public Sprite Food_Icon;

    public int min_energy;
    public int max_energy;

    public int min_life;
    public int max_life;

    public override void OnPlay(Dictionary<string, ParameterEntry> parameters,Card card_scriptable_objects)
    {
        Stats_Perso.Instance.add_action(int.Parse(parameters["energy"].real_value));
        Stats_Perso.Instance.add_santee(int.Parse(parameters["life"].real_value));
    }

    public override void OnStart(Dictionary<string, ParameterEntry> parameters, Card card, CardScript card_script)
    {
        int energy = Random.Range(min_energy, max_energy);
        int life = Random.Range(min_life, max_life);
        parameters["energy"] = new ParameterEntry { display_value = energy.ToString(), real_value = energy.ToString() };
        parameters["life"] = new ParameterEntry { display_value = life.ToString(), real_value = life.ToString() };
    }

    public override void OnTurn(Dictionary<string, ParameterEntry> parameters, Card card, CardScript card_script)
    {

    }

    public override void OnValueGlitch(Dictionary<string, ParameterEntry> parameters, Card card_scriptable_objects)
    {
        OnStart(parameters, card_scriptable_objects,null);
    }
}
