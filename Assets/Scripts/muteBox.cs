using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class muteBox : MonoBehaviour
{
    public void Start()
    {
        int opt = PlayerPrefs.GetInt("mute", 0);
        if(opt == 0)
        {
            gameObject.GetComponent<Toggle>().isOn = false;
        }
        else
        {
            gameObject.GetComponent<Toggle>().isOn = true;
        }
        gameObject.GetComponent<Toggle>().onValueChanged.AddListener(delegate { change(); });

    }

    // Update is called once per frame
    void change()
    {
        if (gameObject.GetComponent<Toggle>().isOn)//turn off
        {
            PlayerPrefs.SetInt("mute", 1);
        }
        else
        {
            PlayerPrefs.SetInt("mute", 0);
        }
    }
}
