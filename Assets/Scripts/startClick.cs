using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class startClick : MonoBehaviour
{
    public string sceneName;
    public InputField width;
    public InputField height;
    public InputField mines;
    public InputField name;

    public GameObject parent;
    public GameObject CanvasToShow;

    int w;
    int h;
    int m;
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
            Int32.TryParse(mines.text, out m);
            Int32.TryParse(width.text, out w);
            Int32.TryParse(height.text, out h);

            if (mines.GetComponent<mineInput>().isGood && (m <= (20f / 100f) * w * h))
            {
                GameManager.instance.state = GameManager.GameState.BEGIN;

                PlayerPrefs.SetString("name", name.text);
                PlayerPrefs.SetInt("width", Int32.Parse(width.text));
                PlayerPrefs.SetInt("height", Int32.Parse(height.text));
                PlayerPrefs.SetInt("mines", Int32.Parse(mines.text));

                GameManager.instance.boardWidth = Int32.Parse(width.text);
                GameManager.instance.boardHeight = Int32.Parse(height.text);
                GameManager.instance.mines = Int32.Parse(mines.text);
                GameManager.instance.minesLeft = Int32.Parse(mines.text);

                //SceneManager.LoadScene(sceneName);
                parent.SetActive(false);
                CanvasToShow.SetActive(true);//gui
                GameManager.instance.GetComponent<MineFieldRenderer>().beginGame();

            }
            else
            {
                mines.GetComponent<InputField>().textComponent.color = Color.red;
            }
        }

    }
}
