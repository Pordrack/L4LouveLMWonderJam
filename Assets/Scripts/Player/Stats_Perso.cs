using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats_Perso : MonoBehaviour
{
    private List<CardScript> Cards_Scripts; //Les cartes "physiquement" dans la main

    public static Stats_Perso Instance { get; private set; }
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

    // gestion santee perso, max peut etre modifie entre autre en cas de glitch
    public int _max_santee = 200;
    public int _santee { get; set; }

    // gestion faim perso, max peut etre modifie en cas de glitch
    public int _max_faim = 200;
    public int _faim { get; set; }

    // gestion action perso (=�nergie), max peut etre modifie en cas de glitch
    public int _max_action = 200;
    public int _action { get; set; }

    [SerializeField] private GameObject endScreenCanvas;



    public int Get_Max_Santee()
    {
        return _max_santee;
    }

    public int Get_Max_Faim()
    {
        return _max_faim;
    }

    public int Get_Max_Action()
    {
        return _action;
    }



    void Start()
    {
        _santee = _max_santee;
        _faim = _max_faim;
        _action = _max_action;
    }

    void Update()
    {
        //if (Random.Range(0, 2) == 0)
        //{
        //    add_santee(Random.Range(0, 11));
        //    add_faim(Random.Range(0, 11));
        //    add_action(Random.Range(0, 11));
        //}
        //else
        //{
        //    down_santee(Random.Range(0, 11));
        //    down_faim(Random.Range(0, 11));
        //    down_action(Random.Range(0, 11));
        //}
        //down_santee(1);
    }

    // ajout de valeurs

    public void add_santee(int hp)
    {
        int santee = _santee;
        int max_santee = _max_santee;

        if (santee == max_santee)
        {
            return;
        }
        else
        {
            santee += hp;
            if (santee > max_santee)
            {
                _santee = max_santee;
            }
            else
            {
                _santee = santee;
            }
            Debug.Log("Sant�e + " + hp + " : " + _santee);
        }
    }

    public void add_faim(int bouffe)
    {
        int faim = _faim;
        int max_faim = _max_faim;

        if (faim == max_faim)
        {
            return;
        }
        else
        {
            faim += bouffe;
            if (faim > max_faim)
            {
                _faim = max_faim;
                add_action(max_faim - faim);
            }
            else
            {
                _faim = faim;
            }
            Debug.Log("Faim + " + bouffe + " : " + _faim);
        }
    }

    public void add_action(int energie)
    {
        int action = _action;
        int max_action = _max_action;

        if (action == max_action)
        {
            return;
        }
        else
        {
            action += energie;
            if (action > max_action)
            {
                _action = max_action;
            }
            else
            {
                _action = action;
            }
//            Debug.Log("Action + " + energie + " : " + _action);
        }
    }

    // enleve valeurs

    public void down_santee(int hp)
    {
        int santee = _santee;

        santee -= hp;

        if (santee <= 0)
        {
            _santee = 0;
            GameFail();
        }
        else
        {
            AudioManager.instance.GetDmgSound();
            _santee = santee;
        }
//        Debug.Log("Santee - " + hp + " : " + _santee);
    }

    public void down_faim(int bouffe)
    {
        int faim = _faim;

        if ((faim - bouffe) <= 0)
        {
            if (_faim != 0)
            {
                faim = bouffe - faim;
                _faim = 0;
            }
            else
            {
                faim = bouffe;
            }
            down_santee(faim);
        }
        else
        {
            faim -= bouffe;
            _faim = faim;
        }
//        Debug.Log("Faim - " + bouffe + " : " + _faim);
    }

    public void down_action(int energie)
    {
        int action = _action;

        if ((action - energie) <= 0)
        {
            if (_action != 0)
            {
                action = energie - action;
                _action = 0;
            }
            else
            {
                action = energie;
            }
            down_santee(action);
        }
        else
        {
            action -= energie;
            _action = action;
        }
//        Debug.Log("Action - " + energie + " : " + _action);
    }

    //method to call when game is lost

    public void GameFail()
    {
        AudioManager.instance.Play("Mort");
        //enable the game over screen
        endScreenCanvas.SetActive(true);
    }
}


