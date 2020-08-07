using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class textInput : MonoBehaviour
{
    public bool isGood;

    public void Start()
    {
        gameObject.GetComponent<InputField>().onValueChanged.AddListener(delegate { check(); });
        isGood = true;
    }

    public void check()
    {
        int num;
        Int32.TryParse(gameObject.GetComponent<InputField>().text, out num);
        if (num < 9 || num > 30)
        {
            //gameObject.GetComponent<InputField>().selectionColor = Color.red;
            gameObject.GetComponent<InputField>().textComponent.color = Color.red;
            isGood = false;
        }
        else
        {
            gameObject.GetComponent<InputField>().textComponent.color = Color.black;
            isGood = true;
        }
    }
}
