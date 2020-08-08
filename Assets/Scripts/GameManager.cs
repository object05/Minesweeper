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
            Debug.Log("WIN");
            HUD temp = GameManager.instance.GetComponent<HUD>();
            HighscoreTable.AddHighscoreEntry((int)temp.elapsed, 
                PlayerPrefs.GetString("name"), 
                PlayerPrefs.GetInt("width") + "x" + PlayerPrefs.GetInt("height") + "_" + PlayerPrefs.GetInt("mines"));
        }
        else
        {
            Debug.Log("LOSE");
            state = GameState.END_LOSE;
        }
    }

}
