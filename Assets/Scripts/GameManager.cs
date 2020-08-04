using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update


    public enum GameState
    {
        BEGIN,RUNNING,END_WIN,END_LOSE
    }
    public GameState state;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
