using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ressources : MonoBehaviour
{
    public int _bois { get; set; } = 0;
    public int _pierre { get; set; } = 0;
    public int _nourriture { get; set; } = 0;

    public GameObject[] rondin;
    public GameObject[] rocher;
    public GameObject[] carotte;



    void Start()
    {
        //rondin[0].SetActive(false);
    }



    public void add_bois(int rondin)
    {
        _bois += rondin;

        Debug.Log("Bois + " + rondin + " : " + _bois);
    }

    public void add_pierre(int rocher)
    {
        _pierre += rocher;

        Debug.Log("Pierre + " + rocher + " : " + _pierre);
    }

    public void add_nourriture(int carotte)
    {
        _nourriture += carotte;

        Debug.Log("Nourriture + " + carotte + " : " + _nourriture);
    }

    // enleve valeurs

    public void down_bois(int rondin)
    {
        if (_bois - rondin <= 0)
        {
            _bois = 0;
        }
        else
        {
            _bois -= rondin;
        }
        Debug.Log("Bois - " + rondin + " : " + _bois);
    }

    public void down_pierre(int rocher)
    {
        if (_pierre - rocher <= 0)
        {
            _pierre = 0;
        }
        else
        {
            _pierre -= rocher;
        }
        Debug.Log("Pierre - " + rocher + " : " + _pierre);
    }

    public void down_nourriture(int carotte)
    {
        if (_nourriture - carotte <= 0)
        {
            _nourriture = 0;
        }
        else
        {
            _nourriture -= carotte;
        }
        Debug.Log("Nourriture - " + carotte + " : " + _nourriture);
    }

    public void update_bois(int _rondin)
    {
        switch (_rondin)
        {
            case 0:
                for (int i = 0; i < 9; i++)
                {
                    rondin[i].SetActive(false);
                }
                break;
            case <= 10:
                for (int i = 1; i < 9; i++)
                {
                    rondin[i].SetActive(false);
                }
                rondin[0].SetActive(true);
                break;
            case <= 20:
                for (int i = 2; i < 9; i++)
                {
                    rondin[i].SetActive(false);
                }
                rondin[0].SetActive(true);
                rondin[1].SetActive(true);
                break;
            case <= 30:
                for (int i = 3; i < 9; i++)
                {
                    rondin[i].SetActive(false);
                }
                for (int i = 0; i < 3; i++)
                {
                    rondin[i].SetActive(true);
                }
                break;
            case <= 40:
                for (int i = 4; i < 9; i++)
                {
                    rondin[i].SetActive(false);
                }
                for (int i = 0; i < 4; i++)
                {
                    rondin[i].SetActive(true);
                }
                break;
            case <= 50:
                for (int i = 5; i < 9; i++)
                {
                    rondin[i].SetActive(false);
                }
                for (int i = 0; i < 5; i++)
                {
                    rondin[i].SetActive(true);
                }
                break;
            case <= 60:
                for (int i = 6; i < 9; i++)
                {
                    rondin[i].SetActive(false);
                }
                for (int i = 0; i < 6; i++)
                {
                    rondin[i].SetActive(true);
                }
                break;
            case <= 70:
                for (int i = 0; i < 7; i++)
                {
                    rondin[i].SetActive(true);
                }
                rondin[7].SetActive(false);
                rondin[8].SetActive(false);
                break;
            case <= 80:
                for (int i = 0; i < 8; i++)
                {
                    rondin[i].SetActive(true);
                }
                rondin[8].SetActive(false);
                break;
            default:
                for (int i = 0; i < 9; i++)
                {
                    rondin[i].SetActive(true);
                }
                break;
        }
    }

    public void update_pierre(int _rocher)
    {
        switch (_rocher)
        {
            case 0:
                for (int i = 0; i < 7; i++)
                {
                    rocher[i].SetActive(false);
                }
                break;
            case <= 10:
                for (int i = 1; i < 7; i++)
                {
                    rocher[i].SetActive(false);
                }
                rocher[0].SetActive(true);
                break;
            case <= 20:
                for (int i = 2; i < 7; i++)
                {
                    rocher[i].SetActive(false);
                }
                rocher[0].SetActive(true);
                rocher[1].SetActive(true);
                break;
            case <= 30:
                for (int i = 3; i < 7; i++)
                {
                    rocher[i].SetActive(false);
                }
                for (int i = 0; i < 3; i++)
                {
                    rocher[i].SetActive(true);
                }
                break;
            case <= 40:
                for (int i = 4; i < 7; i++)
                {
                    rocher[i].SetActive(false);
                }
                for (int i = 0; i < 4; i++)
                {
                    rocher[i].SetActive(true);
                }
                break;
            case <= 50:
                for (int i = 0; i < 5; i++)
                {
                    rocher[i].SetActive(true);
                }
                rocher[5].SetActive(false);
                rocher[6].SetActive(false);
                break;
            case <= 60:
                for (int i = 0; i < 6; i++)
                {
                    rocher[i].SetActive(true);
                }
                rocher[6].SetActive(false);
                break;
            default:
                for (int i = 0; i < 7; i++)
                {
                    rocher[i].SetActive(true);
                }
                break;
        }
    }

    public void update_nourriture(int _carotte)
    {
        switch (_carotte)
        {
            case 0:
                for (int i = 0; i < 9; i++)
                {
                    carotte[i].SetActive(false);
                }
                break;
            case <= 10:
                for (int i = 1; i < 9; i++)
                {
                    carotte[i].SetActive(false);
                }
                carotte[0].SetActive(true);
                break;
            case <= 20:
                for (int i = 2; i < 9; i++)
                {
                    carotte[i].SetActive(false);
                }
                carotte[0].SetActive(true);
                carotte[1].SetActive(true);
                break;
            case <= 30:
                for (int i = 3; i < 9; i++)
                {
                    carotte[i].SetActive(false);
                }
                for (int i = 0; i < 3; i++)
                {
                    carotte[i].SetActive(true);
                }
                break;
            case <= 40:
                for (int i = 4; i < 9; i++)
                {
                    carotte[i].SetActive(false);
                }
                for (int i = 0; i < 4; i++)
                {
                    carotte[i].SetActive(true);
                }
                break;
            case <= 50:
                for (int i = 5; i < 9; i++)
                {
                    carotte[i].SetActive(false);
                }
                for (int i = 0; i < 5; i++)
                {
                    carotte[i].SetActive(true);
                }
                break;
            case <= 60:
                for (int i = 6; i < 9; i++)
                {
                    carotte[i].SetActive(false);
                }
                for (int i = 0; i < 6; i++)
                {
                    carotte[i].SetActive(true);
                }
                break;
            case <= 70:
                for (int i = 0; i < 7; i++)
                {
                    carotte[i].SetActive(true);
                }
                carotte[7].SetActive(false);
                carotte[8].SetActive(false);
                break;
            case <= 80:
                for (int i = 0; i < 8; i++)
                {
                    carotte[i].SetActive(true);
                }
                carotte[8].SetActive(false);
                break;
            default:
                for (int i = 0; i < 9; i++)
                {
                    carotte[i].SetActive(true);
                }
                break;
        }
    }
}
