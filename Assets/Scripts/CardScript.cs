using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CardScript : MonoBehaviour
{
    public Card Card_Scriptable_Object;
    public TMP_Text Card_Name;
    public TMP_Text Card_Description;
    public Image Card_Image;
    public TMP_Text Energy_Cost;
    public TMP_Text Wood_Cost;
    public TMP_Text Stone_Cost;
    private string SEPARATOR = "|"; //Séparateur des paramètres de la description
    private string PLACEHOLDER = "paramètre introuvable";

    void Start()
    {
        Card_Scriptable_Object = Instantiate(Card_Scriptable_Object);
        Card_Scriptable_Object.FillDictionnary();
        LoadCard();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Remplie les champs de la carte avec les infos du scriptable object
    void LoadCard()
    {
        Card_Name.text = Card_Scriptable_Object.Name;
        Card_Description.text = Fill_Description(Card_Scriptable_Object.Description);
        Card_Image.sprite = Card_Scriptable_Object.Icon;
        Wood_Cost.text = Card_Scriptable_Object.Wood_Cost.ToString();
        Stone_Cost.text = Card_Scriptable_Object.Stone_Cost.ToString();
        Energy_Cost.text = Card_Scriptable_Object.Energy_Cost.ToString();

        //On cache les icones de couts si inutiles
        Energy_Cost.transform.parent.gameObject.SetActive(Card_Scriptable_Object.Energy_Cost > 0);
        Stone_Cost.transform.parent.gameObject.SetActive(Card_Scriptable_Object.Stone_Cost > 0);
        Wood_Cost.transform.parent.gameObject.SetActive(Card_Scriptable_Object.Wood_Cost > 0);
    }

    //Prend le texte "brute" de la description, puis rempli les paramètres en mettant leur valeur
    string Fill_Description(string description)
    {
        //On va d'abord séparer les strings en ségment
        string[] segments = description.Split(SEPARATOR);

        //On va regarder si le premier mot est un paramètre en regardant si la chaine commence par |
        //Si oui, on va prendre les bouts de string avec un index paire (0, 2, etc.) car ce sont les paramètres
        //Si non, ce sont ceux impaires
        int firstIndex = 1;
        if (description[0] == '|')
        {
            firstIndex = 0;
        }

        //On remplace toutes les clés de paramètres par leurs valeurs
        for(int i = firstIndex; i < segments.Length; i+=2)
        {
            string value = Card_Scriptable_Object.Params[segments[i]][0];
            //Debug.Log(i);
            if (value == null)
            {
                value = PLACEHOLDER;
            }
            segments[i] = value;
            //Debug.Log(value);
        }

        //On recolle les bouts puis on renvoie
        return string.Concat(segments);

    }
}
