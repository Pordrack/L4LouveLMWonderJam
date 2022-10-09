using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum State {Player_Turn, Environnement_Turn, Glitch_Turn}
public class GameManager : MonoBehaviour
{

    
    public State state;
    public TestInput Input;
    public GameObject ennemy;
    [Tooltip("Probability qu'une carte glitch a chaque tour")]
    [Range(0f, 1f)]
    public float Glitch_Probability=0.8f;
    private int turnBeforeGlitch;
    public delegate void OnTurn();
    public static event OnTurn On_Player_Turn;
    public static event OnTurn On_Enemy_Turn;
    private bool startPlayerTurn = false;

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
        Input = new TestInput();
        turnBeforeGlitch = Random.Range(1,6);
    }

    // Update is called once per frame
    void Update()
    {
       
       if (state == State.Player_Turn){    
            if(startPlayerTurn == false){
                StatePlayerTurn();
                startPlayerTurn = true;
            }
            
       }

       if (state == State.Environnement_Turn){      
           StateEnemyTurn();

           changeStateEvent();
            Debug.Log("ennemy end");
       }
       if(state == State.Glitch_Turn){
        StateGlitchTurn();
         changeStateEvent();
         Debug.Log("Glitch end");
       }
    }

    public void StatePlayerTurn(){
        Input.Player.Enable();
        //deplacement du joueur
        //actions
        HandScript.Instance.Fill_Hand();

        
        On_Player_Turn.Invoke();
    }
    // a la fin du tour du joueur
    public void EndPlayerTurn(){
        turnBeforeGlitch -=1;
        changeStateEvent();
        startPlayerTurn = false;
        Debug.Log("player end");
    }

    public void StateEnemyTurn(){
        Input.Player.Disable();
        
        

        //deplacement des ennemis
        //actions des ennemis
        
//        On_Enemy_Turn.Invoke();
    }
    public void StateGlitchTurn(){
        Input.Player.Disable();

        //apparition des glitch
        //Descente des glitchs
        //Glitchage des mobs
        //glitchage de la main
         HandScript.Instance.Glitch_Hand(Glitch_Probability);
         turnBeforeGlitch = Random.Range(1,6);
         

    }

    //Pour le debug pour moi
    public static void CallPlayerTurnEvent()
    {
        On_Player_Turn.Invoke();
    }

    public void changeStateEvent(){
        if(state == State.Player_Turn && turnBeforeGlitch!=0){
          
            state = State.Environnement_Turn;
         
        }
        else if(state == State.Player_Turn && turnBeforeGlitch==0){
            state = State.Glitch_Turn;
        }
        else if(state == State.Environnement_Turn){
            state = State.Player_Turn;
        }
        else if(state == State.Glitch_Turn){
            state = State.Environnement_Turn;
        }
    }
}