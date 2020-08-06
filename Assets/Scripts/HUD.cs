﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    public Image status_image;
    void Start()
    {
        status_image.sprite = Assets.dictionary[RegionNames.SMILE_HAPPY];
    }

    void Update()
    {
        if(GameManager.instance.state == GameManager.GameState.END_LOSE)
        {
            status_image.sprite = Assets.dictionary[RegionNames.SMILE_DEAD];
        }
        else if (GameManager.instance.state == GameManager.GameState.END_WIN)
        {
            status_image.sprite = Assets.dictionary[RegionNames.SMILE_WIN];
        }
        else
        {
            status_image.sprite = Assets.dictionary[RegionNames.SMILE_HAPPY];
        }


    }




}
