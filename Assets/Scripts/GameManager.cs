using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public enum State {Player_Turn, Environnement_Turn, Glitch_Turn, Decision_Turn}
public class GameManager : MonoBehaviour
{

    
    public State state;
    public GameObject ennemy;
    [Tooltip("Probability qu'une carte glitch a chaque tour")]
    [Range(0f, 1f)]
    public float Glitch_Probability=0.8f;
    private int turnBeforeGlitch;
    public delegate void OnTurn();
    public static event OnTurn OnPlayerTurn;
    private bool startPlayerTurn = false;

    [Header("Glitch")]
    public int NbTourMin=3;

    public int NbTourMax=7;

    [SerializeField] private Glitch_Wave glitchWave;

    [Header("RessourceLose")]
    public int FoodLostByTurn=20;
    public int RegenActionByTurn=100;


    public static GameManager Instance { get; private set; }
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

    // Start is called before the first frame update
    void Start()
    {
        state = State.Player_Turn;

        turnBeforeGlitch = Random.Range(NbTourMin,NbTourMax);
    }
    

    public void StatePlayerTurn(){
        Stats_Perso.Instance.add_action(RegenActionByTurn);
        Stats_Perso.Instance.down_faim(FoodLostByTurn);
        PlayerH.InputController.Instance.SwitchInputToMovement(true);
        
        //deplacement du joueur
        //actions
        HandScript.Instance.Fill_Hand();

        
        OnPlayerTurn.Invoke();
        
    }
    // a la fin du tour du joueur
    private void EndPlayerTurn(){
        PlayerH.InputController.Instance.SwitchInputToMovement(true);
        PlayerH.InputController.Instance.EnableMovement(false);
        turnBeforeGlitch -=1;
    }

    public void StateEnemyTurn(){
        //deplacement des ennemis
        //actions des ennemis
        IA.EnemyManager.Singleton.PerformEnemiesTurn();       
        ChangeState(State.Player_Turn);
    }
    public void StateGlitchTurn(){
        //apparition des glitch
        glitchWave.Start_Wave();
        //Descente des glitchs
        //Glitchage des mobs

        //glitchage de la main
         HandScript.Instance.Glitch_Hand(Glitch_Probability);
         turnBeforeGlitch = Random.Range(NbTourMin,NbTourMax);
         ChangeState(State.Environnement_Turn);
    }

    public void ChangeState(State newState)
    {
        state = newState;

        switch (newState)
        {
            case State.Player_Turn:
                StatePlayerTurn();
                break;
            case State.Environnement_Turn:
                StateEnemyTurn();
                break;
            case State.Glitch_Turn:
                StateGlitchTurn();
                break;
            case State.Decision_Turn:
                EndPlayerTurn();
                ChangeState(turnBeforeGlitch == 0 ? State.Glitch_Turn : State.Environnement_Turn);
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(newState), newState, null);
        }
    }
    
}