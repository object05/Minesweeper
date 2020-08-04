using System.Collections;
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


}
