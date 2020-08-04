using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineFieldRenderer : MonoBehaviour
{
    private MineField minefield;

    // Start is called before the first frame update
    void Start()
    {
        minefield = gameObject.GetComponent<MineField>();
    }


    // Update is called once per frame
    void Update()
    {

    }

    void drawBoard()
    {
        for(int x = 0; x < minefield.width; x++)
        {
            for(int y = 0; y < minefield.height; y++)
            {

            }
        }
    }
}
