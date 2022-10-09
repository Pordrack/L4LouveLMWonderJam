using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum State {Player_Turn, Environnement_Turn, Glitch_Turn}
public class GameManager : MonoBehaviour
{
    public State state;
    private int turnBeforeGlitch;
    public TestInput Input;
    public GameObject ennemy;
<<<<<<< Updated upstream

    public delegate void OnTurn();
    public static event OnTurn On_Player_Turn;
    public static event OnTurn On_Enemy_Turn;
    
=======
    [Tooltip("Probability qu'une carte glitch a chaque tour")]
    [Range(0f, 1f)]
    public float Glitch_Probability=0.8f;
    public delegate void OnTurn();
    public static event OnTurn On_Player_Turn;
    public static event OnTurn On_Enemy_Turn;

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

>>>>>>> Stashed changes
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
<<<<<<< Updated upstream
        if (state == State.Player_Turn){    
            StatePlayerTurn();
            
            // a la fin du tour
            state = State.Environnement_Turn;

            Debug.Log("player end");
        }

        if (state == State.Environnement_Turn){      
            StateEnemyTurn();

            state = State.Player_Turn;
             Debug.Log("ennemy end");
        }
=======
       if (state == State.Player_Turn){    
           StatePlayerTurn();
            
           // a la fin du tour
           ChangeState(state);

           Debug.Log("player end");
       }

       if (state == State.Environnement_Turn){      
           StateEnemyTurn();

           ChangeState(state);
            Debug.Log("ennemy end");
       }
       if(state == State.Glitch_Turn){
           StateGlitchTurn();
           ChangeState(state);
           Debug.Log("Glitch end");
       }
>>>>>>> Stashed changes
    }

    public void StatePlayerTurn(){
        Input.Player.Enable();
        //deplacement du joueur
        //actions
<<<<<<< Updated upstream
        
=======
        HandScript.Instance.Fill_Hand();

        turnBeforeGlitch -=1;
        On_Player_Turn.Invoke();
>>>>>>> Stashed changes
    }

    public void StateEnemyTurn(){
        Input.Player.Disable();
        //deplacement des ennemis
<<<<<<< Updated upstream
        //actions des ennemis
    }


=======
        //actions des ennemis si agressif
        
        On_Enemy_Turn.Invoke();
    }

    public void StateGlitchTurn(){
        Input.Player.Disable();
        //apparition des glitchs
        //actions des glitchs vers le bas
        //Glitchage des ennemy et nous
        //glitch des cartes fait
        HandScript.Instance.Glitch_Hand(Glitch_Probability);
        // on recalcule le nb de tour avant le prochain glitch
        turnBeforeGlitch = Random.Range(1,6);

    }

    

    public void ChangeState(State state){
        if(state == State.Player_Turn && turnBeforeGlitch!=0){
            state = State.Environnement_Turn;
        }
        if(state == State.Environnement_Turn){
            state = State.Player_Turn;
        }
        if(state == State.Player_Turn && turnBeforeGlitch == 0){
            state = State.Glitch_Turn;
        }
    }
>>>>>>> Stashed changes
}
