using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Card", menuName = "Card", order = 1)]
public class Card : ScriptableObject
{
    public Sprite Icon;
    public string Name;
    public string Description; //La description de la carte
    //La description peut contenir des refs aux parametre : avec texte avant |cléParametre| text après
    public Dictionary<string,string[]> Params; //Les paramètres, CléParametre={DisplayName,Value} Displayname = affiché sur la carte, Value=connerie
    public int Energy_Cost;
    public int Wood_Cost;
    public int Stone_Cost;

    public string TextParams; //Paramètre aux format text, converti en dictionnaire Params
    //Format = "Key:DisplayName-Value;"
    //Remplie le dico a partir de la valeur de text params
    public void FillDictionnary()
    {
        Params = new Dictionary<string, string[]>();
        string[] lines = TextParams.Split(";");
        foreach(string line in lines)
        {
            string key = line.Split(":")[0];
            string[] value = line.Split(":")[1].Split("-");
            Params.Add(key, value);
        }
    }
}
