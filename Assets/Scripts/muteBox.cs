using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class muteBox : MonoBehaviour
{
    public void Start()
    {
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
