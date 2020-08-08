using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunHandler : MonoBehaviour
{
    void Update()
    {
        if (gameObject.activeSelf && (GameManager.instance.state != GameManager.GameState.END_LOSE &&
            GameManager.instance.state != GameManager.GameState.END_WIN))
        {
            GameManager.instance.state = GameManager.GameState.RUNNING;
        }
    }
}
