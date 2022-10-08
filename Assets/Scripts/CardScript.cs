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
    public Vector3 Target_Position; //Une position dont on doit se rapprocher
    public bool Must_Reach_Target=false; //Doit se déplacer vers sa position cible
    public float Max_Speed = 2; //La vitesse par frame dont on se déplace

    public static Dictionary<string,Card_Effect> Card_Effects_Dictionnary;//Le dictionnaire qui contient
    //Les classes pour les effets de toutes les cartes
    void Start()
    {
        Card_Scriptable_Object = Instantiate(Card_Scriptable_Object);
        Card_Scriptable_Object.FillDictionnary();
        LoadCard();
    }

    void FixedUpdate()
    {
        if (!Must_Reach_Target)
        {
            return;
        }
        Target_Position.y = transform.position.y;
        //On se rapproche de la position cible
        transform.position = Vector3.MoveTowards(transform.position, Target_Position, Max_Speed);

        if (transform.position == Target_Position)
        {
            Must_Reach_Target = false;
        }
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

    //Joué quand on joue la carte
    public void On_Play()
    {
        if (Card_Effects_Dictionnary != null)
        {
            //On trouve son instance de Card_Effects puis on appel OnPlay
            if (Card_Effects_Dictionnary.ContainsKey(Card_Scriptable_Object.Effects_Key))
            {
                Card_Effects_Dictionnary[Card_Scriptable_Object.Effects_Key].OnPlay(Card_Scriptable_Object.Params);
            }
        }
        
        //A la fin on défausse
        GetComponentInChildren<Collider>().enabled = false;
        Destroy(gameObject, 1);
    }

    //Joué quand la carte glitch
    public void On_Glitch()
    {
        if (Card_Effects_Dictionnary.ContainsKey(Card_Scriptable_Object.Effects_Key))
        {
            Card_Effects_Dictionnary[Card_Scriptable_Object.Effects_Key].OnGlitch(Card_Scriptable_Object.Params);
        }
    }
}
