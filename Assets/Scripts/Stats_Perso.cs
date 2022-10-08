using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats_Perso : MonoBehaviour
{
    // gestion santee perso, max peut etre modifie entre autre en cas de glitch
    public int max_santee { get; private set; } = 200; 
    public int min_santee { get; private set; } = 0;
    public int santee { get; set; }

    // gestion faim perso, max peut etre modifie en cas de glitch
    public int max_faim { get; private set; } = 200;
    public int min_faim { get; private set; } = 0;
    public int faim { get; set; }

    // gestion action perso (=énergie), max peut etre modifie en cas de glitch
    public int max_action { get; private set; } = 200;
    public int min_action { get; private set; } = 0;
    public int action { get; set; }
}
