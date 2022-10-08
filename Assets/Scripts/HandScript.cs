using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandScript : MonoBehaviour
{
    public float Z_Variation=2; //Valeur absolu maximum de la variation aleatoire du la position Z
    public float Angle_Variation=18; //Valeur absolu maximum de la variation aleatoire de l'angle
    public float SpawnAltitude = 20;
    //Permet d'avoir les cartes un peu pos�s a l'arrache comme en vrai

    public Transform[] Cards_Holder; //Les positions ou doivent atterir les cartes sur la table
    public int Max_Number_Of_Cards;

    public GameObject Card_Prefab; //Prefab de la carte physique
    public Card[] Cards_Templates; //Liste des scritable objects de cartes randoms
    public int[] Ponderations; //Les ponderation des cartes randoms
    private List<Card> Cards_Templates_With_Ponderations; //La fusion des deux, avec des cartes en multiples pour la pond�ration

    private List<CardScript> Cards_Scripts; //Les cartes "physiquement" dans la main

    public static HandScript Instance { get; private set; }
    private void Awake()
    {
        // If there is an instance, and it's not me, delete myself.

        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    void Start()
    {
        Cards_Scripts = new List<CardScript>();
        Cards_Templates_With_Ponderations = new List<Card>();
        //On cr�� le tableau cards with ponderations
        //En mettant x fois les cards templates dans cards with ponderations
        for(int i=0;i<Cards_Templates.Length;i++)
        {
            Card card = Cards_Templates[i];
            int ponderation = 1;

            if (i < Ponderations.Length)
                ponderation = Ponderations[i];

            for(int j = 0; j < ponderation;j++)
            {
                Cards_Templates_With_Ponderations.Add(card);
            }
        }

        Fill_Hand();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Indique leur (nouvelle) position cible aux cartes;
    void Update_Target_Pos()
    {
        for(int i = 0; i < Cards_Scripts.Count; i++)
        {
            CardScript script = Cards_Scripts[i];
            if (script != null)
            {
                script.Target_Position = Cards_Holder[i].position;
                script.Must_Reach_Target = true;
            }
        }
    }

    //Rempli la main avec de nouvelles cartes
    public void Fill_Hand()
    {
        int first_index = 0;
        if (Cards_Scripts.Count > 0)
        {
            first_index = Cards_Scripts.Count;
        }
        for(int i = first_index; i < Max_Number_Of_Cards; i++)
        {
            Cards_Scripts.Add(Deal_Card(Cards_Holder[i].position));
        }
    }

    //Ajoute une nouvelle carte � la main
    //Renvoie le card script
    CardScript Deal_Card(Vector3 SpawnPosition)
    {
        //On commence par choisir un mod�le de carte � cr�er � partir du tableau
        int index = Random.Range(0,Cards_Templates_With_Ponderations.Count);
        Card card_template = Cards_Templates_With_Ponderations[index];

        //On rajotue des variations en Z et en rotation, pour l'effet "naturel"
        float rotation = Random.Range(-Angle_Variation, Angle_Variation);
        float z_offset = Random.Range(-Z_Variation, Z_Variation);
        Quaternion quaternion = Quaternion.Euler(0, rotation, 0);
        SpawnPosition.z += z_offset;
        SpawnPosition.y += SpawnAltitude;

        //On cr�� l'objet
        GameObject new_card_go = Instantiate(Card_Prefab, SpawnPosition, quaternion);
        CardScript new_card = new_card_go.GetComponent<CardScript>();

        //On applique le template
        new_card.Card_Scriptable_Object = card_template;

        return new_card;
    }

    //Joue une carte de la main en fonction de son index
    public void Play_Card_Of_Index(int index)
    {
        if (index>= Cards_Scripts.Count)
        {
            return;
        }

        Cards_Scripts[index].On_Play();
        Cards_Scripts.RemoveAt(index);

        Update_Target_Pos();
    }
}
