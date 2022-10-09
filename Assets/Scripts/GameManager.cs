using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum State {Player_Turn, Environnement_Turn, Glitch_Turn}
public class GameManager : MonoBehaviour
{
    public State state;
    public TestInput Input;
    public GameObject ennemy;

    public delegate void OnTurn();
    public static event OnTurn On_Player_Turn;
    public static event OnTurn On_Enemy_Turn;
    
    // Start is called before the first frame update
    void Start()
    {
        state = State.Player_Turn;
        Input = new TestInput();
    }

    // Update is called once per frame
    void Update()
    {
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
    }

    public void StatePlayerTurn(){
        Input.Player.Enable();
        //deplacement du joueur
        //actions
        
    }

    public void StateEnemyTurn(){
        Input.Player.Disable();
        //deplacement des ennemis
        //actions des ennemis
    }


}
