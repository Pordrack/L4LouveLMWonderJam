using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loading_Effects : Card_Effect
{
    public MinMaxValue Total_Loading_Time;

    public override void OnPlay(Dictionary<string, ParameterEntry> parameters, Card card_scriptable_objects)
    {

    }

    public override void OnStart(Dictionary<string, ParameterEntry> parameters, Card card, CardScript card_script)
    {
        card_script.transform.Find("Loading").gameObject.SetActive(true);
        int loading_time=ValueRandomizer.RandomizeValue(Total_Loading_Time);
        parameters["loading_time"] = new ParameterEntry { display_value = loading_time.ToString(), real_value = loading_time.ToString() };
    }

    public override void OnTurn(Dictionary<string, ParameterEntry> parameters, Card card, CardScript card_script)
    {
        int remaining_loading_time = int.Parse(parameters["loading_time"].real_value);
        remaining_loading_time--;
        if (remaining_loading_time<=0)
        {
            card_script.Card_Scriptable_Object = Instantiate(HandScript.Instance.GetRandomCardTemplate());
            return;
        }
        parameters["loading_time"] = new ParameterEntry { display_value = remaining_loading_time.ToString(), real_value = remaining_loading_time.ToString() };
    }

    public override void OnValueGlitch(Dictionary<string, ParameterEntry> parameters, Card card_scriptable_objects)
    {

    }
}
