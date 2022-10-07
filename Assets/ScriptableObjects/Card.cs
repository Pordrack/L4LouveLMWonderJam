using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : ScriptableObject
{
    public Sprite Icon;
    public string Name;
    public string Description; //La description de la carte
    //La description peut contenir des refs aux parametre : avec {Cl�Parametre}
    public Dictionary<string,string[]> Params; //Les param�tres, Cl�Parametre={DisplayName,Value} Displayname = affich� sur la carte, Value=connerie
}
