using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : ScriptableObject
{
    public Sprite Icon;
    public string Name;
    public string Description; //La description de la carte
    //La description peut contenir des refs aux parametre : avec {CléParametre}
    public Dictionary<string,string[]> Params; //Les paramètres, CléParametre={DisplayName,Value} Displayname = affiché sur la carte, Value=connerie
}
