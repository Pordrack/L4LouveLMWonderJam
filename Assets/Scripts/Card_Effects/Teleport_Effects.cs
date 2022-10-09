using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport_Effects : Card_Effect
{
    //public Sprite Axe_Icon;
    //public Sprite Pickaxe_Icon;
    //public Sprite Food_Icon;


    //[Tooltip("Entre 0 et 1, proba que l'outil sois faible et capable de recup qu'une ressource")]
    //public float weak_probability; 

    public override void OnPlay(Dictionary<string, ParameterEntry> parameters,Card card_scriptable_objects)
    {
        
    }

    public override void OnStart(Dictionary<string, ParameterEntry> parameters, Card card, CardScript card_script)
    {

    }

    public override void OnTurn(Dictionary<string, ParameterEntry> parameters, Card card, CardScript card_script)
    {

    }

    public override void OnValueGlitch(Dictionary<string, ParameterEntry> parameters, Card card_scriptable_objects)
    {
        card_scriptable_objects.Name = ToHexString(Random.Range(0f,10f));

        OnStart(parameters, card_scriptable_objects, null);
    }

    string ToHexString(float f)
    {
        var bytes = System.BitConverter.GetBytes(f);
        var i = System.BitConverter.ToInt32(bytes, 0);
        return "0x" + i.ToString("X8");
    }
}
