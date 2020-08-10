using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    public Image status_image;
    public Text clock;
    public Text mines;
    float last;
    public float elapsed;

    void Start()
    {
        elapsed = 0;
        mines.text = GameManager.instance.minesLeft.ToString();

        last = Time.time;
        //status_image.GetComponent<Button>().onClick.AddListener(delegate { pause(); });
        status_image.sprite = Assets.instance.dictionary[RegionNames.SMILE_HAPPY];
    }

    //void Awake()
    //{
    //    DontDestroyOnLoad(this);
    //}

    void Update()
    {
        mines.text = GameManager.instance.minesLeft.ToString();
        if (GameManager.instance.state == GameManager.GameState.BEGIN)
        {
            elapsed = 0;
        }

        if(GameManager.instance.state == GameManager.GameState.END_LOSE)
        {
            status_image.sprite = Assets.instance.dictionary[RegionNames.SMILE_DEAD];
        }
        else if (GameManager.instance.state == GameManager.GameState.END_WIN)
        {
            status_image.sprite = Assets.instance.dictionary[RegionNames.SMILE_WIN];
        }
        else
        {
            status_image.sprite = Assets.instance.dictionary[RegionNames.SMILE_HAPPY];
            if (GameManager.instance.state == GameManager.GameState.RUNNING)
            {
                if (Time.time > last + 1f)
                {
                    elapsed++;
                    clock.text = elapsed.ToString();
                    last = Time.time;
                }
            }

        }

    }

    void pause()
    {
        status_image.GetComponent<sceneSwitcher>().proceed();
    }




}
