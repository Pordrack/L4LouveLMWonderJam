using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Effect_Key_Enum { food,camp,gather,attack,teleport,queen,loading,sacrifice,phytotherapy}

[CreateAssetMenu(fileName = "New Card", menuName = "Card", order = 1)]
public class Card : ScriptableObject
{
    public Effect_Key_Enum Effects_Key;
    public Sprite Icon;
    public string Name;
    public string Description; //La description de la carte
                               //La description peut contenir des refs aux parametre : avec texte avant |cléParametre| text après
    public Dictionary<string, ParameterEntry> Params; //Les paramètres, CléParametre={DisplayName,Value} Displayname = affiché sur la carte, Value=connerie
    public int Energy_Cost;
    public int Wood_Cost;
    public int Stone_Cost;
    public int Food_Cost;

    public AudioClip Audio_Clip;

    [Tooltip("Between 0 and 1, probability to become an all new card when glitched")]
    public float Probability_to_change; //Probabilité de devenir une nouvelle carte lors du glitch
                               
    [SerializeField]
    ParameterEntryWithKeys[] parameter_Entries;

    public void FillDictionnary()
    {
        Params = new Dictionary<string, ParameterEntry>();
        foreach(ParameterEntryWithKeys parameter_Entry_With_Keys in parameter_Entries)
        {
            ParameterEntry parameter_Entry = new ParameterEntry();
            parameter_Entry.display_value = parameter_Entry_With_Keys.display_value;
            parameter_Entry.real_value = parameter_Entry_With_Keys.real_value;
            Params.Add(parameter_Entry_With_Keys.key, parameter_Entry);
        }
    }
}
[Serializable]
struct ParameterEntryWithKeys
{
    public string key;
    public string display_value;
    public string real_value;
}

[Serializable]
public struct ParameterEntry
{

    public string display_value;
    public string real_value;
}

