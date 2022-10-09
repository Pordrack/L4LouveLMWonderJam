using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generic_Effects : Card_Effect
{
    [Header("Ressources")]
    public MinMaxValue FoodBonus;
    public MinMaxValue WoodBonus;
    public MinMaxValue StoneBonus;

    [Header("Stats joueurs")]
    public MinMaxValue HealthBonus;
    public MinMaxValue HungerBonus;
    public MinMaxValue ActionBonus;

    public override void OnPlay(Dictionary<string, ParameterEntry> parameters,Card card_scriptable_objects)
    {
        Ressources.Instance.add_bois(ValueRandomizer.RandomizeValue(WoodBonus));
        Ressources.Instance.add_nourriture(ValueRandomizer.RandomizeValue(FoodBonus));
        Ressources.Instance.add_pierre(ValueRandomizer.RandomizeValue(StoneBonus));

        Stats_Perso.Instance.add_action(ValueRandomizer.RandomizeValue(ActionBonus));
        Stats_Perso.Instance.add_faim(ValueRandomizer.RandomizeValue(HungerBonus));
        Stats_Perso.Instance.add_santee(ValueRandomizer.RandomizeValue(HealthBonus));

        Ressources.Instance.update_nourriture(Ressources.Instance._nourriture);
        Ressources.Instance.update_bois(Ressources.Instance._bois);
        Ressources.Instance.update_pierre(Ressources.Instance._pierre);
    }

    public override void OnStart(Dictionary<string, ParameterEntry> parameters, Card card, CardScript card_script)
    {

    }

    public override void OnTurn(Dictionary<string, ParameterEntry> parameters, Card card, CardScript card_script)
    {
        
    }

    public override void OnValueGlitch(Dictionary<string, ParameterEntry> parameters, Card card_scriptable_objects)
    {
        OnStart(parameters, card_scriptable_objects,null);
    }  
}

public class ValueRandomizer
{
    public static int RandomizeValue(MinMaxValue value)
    {
        return Random.Range(value.min, value.max + 1);
    }
}


//Structure qui sert a définir une vairbale qui sera générée aléatoirement par ValueRandomizer.RandomizeValue;
[System.Serializable]
public struct MinMaxValue
{
    public int min;
    public int max;
}
