﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class sceneSwitcher : MonoBehaviour
{
    public GameObject canvasToShow;
    public GameObject parentCanvas;

    public void Start()
    {
        gameObject.GetComponent<Button>().onClick.AddListener(delegate { proceed(); });
    }

    public void proceed()
    {
        canvasToShow.SetActive(true);
        parentCanvas.SetActive(false);
    }



}
