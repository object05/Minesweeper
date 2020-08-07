using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    // Start is called before the first frame update
    public enum GameState
    {
        BEGIN,RUNNING,END_WIN,END_LOSE,PAUSE
    }
    public GameState state;


    void Awake()
    {
        MakeSingleton();
        state = GameState.BEGIN;
    }

    void MakeSingleton()
    {
        if(instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public void endGame()
    {
        if (GetComponent<MineField>().isWin())
        {
            state = GameState.END_WIN;
            //hud.setState(GameObjectSmileButton.STATE.WIN);
        }
        else
        {
            state = GameState.END_LOSE;
            //hud.setState(GameObjectSmileButton.STATE.LOSE);
        }
    }

}
