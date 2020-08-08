using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseHandler: MonoBehaviour
{
    void Update()
    {
        if (gameObject.activeSelf)
        {
            GameManager.instance.state = GameManager.GameState.PAUSE;
        }
    }
   
}
