using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineFieldRenderer : MonoBehaviour
{
    private MineField field;

    void Start()
    {
        field = gameObject.GetComponent<MineField>();

    }

    void Update()
    {
        drawBoard();
    }

    void drawBoard()
    {
        float heightHalfSize = Camera.main.orthographicSize;
        float widthHalfSize = heightHalfSize * Camera.main.aspect;

        //todo set cam to x , y * cellnum + HUDH
        for(int x = 0; x < field.width; x++)
        {
            for(int y = 0; y < field.height; y++)
            {
                field.minefield[x, y].GetComponent<SpriteRenderer>().sprite = Assets.dictionary[RegionNames.MINECELL1];
                field.minefield[x, y].gameObject.transform.position = new Vector3(
                    -widthHalfSize+ field.minefield[x, y].GetComponent<SpriteRenderer>().sprite.bounds.size.x / 2
                    + x*field.minefield[x,y].GetComponent<SpriteRenderer>().sprite.bounds.size.x, 
                    -heightHalfSize+ field.minefield[x, y].GetComponent<SpriteRenderer>().sprite.bounds.size.y / 2
                    + y*field.minefield[x, y].GetComponent<SpriteRenderer>().bounds.size.y, 0);
            }
        }
    }
}
