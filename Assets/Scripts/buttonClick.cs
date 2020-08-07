using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class buttonClick : MonoBehaviour
{
    public string sceneName;
    public InputField width;
    public InputField height;
    public InputField mines;
    // Start is called before the first frame update
    public void Start()
    {
        gameObject.GetComponent<Button>().onClick.AddListener(delegate { proceed(); });
    }

    // Update is called once per frame
    public void proceed()
    {
        if (width.GetComponent<textInput>().isGood || height.GetComponent<textInput>().isGood)
        {
            if (mines.GetComponent<mineInput>().isGood && (Int32.Parse(mines.text) < (20 / 100) * Int32.Parse(width.text) * Int32.Parse(height.text)))
            {
                PlayerPrefs.SetInt("width", Int32.Parse(width.text));
                PlayerPrefs.SetInt("height", Int32.Parse(height.text));
                PlayerPrefs.SetInt("mines", Int32.Parse(mines.text));
                SceneManager.LoadScene(sceneName);
            }
        }

    }
}
