using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineFieldRenderer : MonoBehaviour
{
    private MineField field;

    float heightHalfSize;
    float widthHalfSize;

    Vector2 tileDimensions;


    void Start()
    {
        field = gameObject.GetComponent<MineField>();


        heightHalfSize = Camera.main.orthographicSize;
        widthHalfSize = heightHalfSize * Camera.main.aspect;

        //drawBoard();
        tileDimensions = getTileDimension();

        adjustCamera();
        drawBoard();

    }

    void adjustCamera()
    {
        Debug.Log(tileDimensions);
        while (widthHalfSize * 2 <= field.width * tileDimensions.x)
        {
            heightHalfSize = Camera.main.orthographicSize;
            widthHalfSize = heightHalfSize * Camera.main.aspect;
            Camera.main.orthographicSize += 0.1f;
        }
        

        while (heightHalfSize * 2 <= field.height * tileDimensions.y)
        {
            heightHalfSize = Camera.main.orthographicSize;
            widthHalfSize = heightHalfSize * Camera.main.aspect;
            Camera.main.orthographicSize += 0.1f;
        }
        //Camera.main.transform.position = new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y, 0);


    }
    
    Vector2 getTileDimension()//position 0,0 tile dimension
    {
        return field.minefield[0, 0].GetComponent<SpriteRenderer>().sprite.bounds.size;
    }

    void Update()
    {
        //draw method not relative to cam
    }

    void drawBoard()
    {
        //todo set cam to x , y * cellnum + HUDH
        for(int x = 0; x < field.width; x++)
        {
            for(int y = 0; y < field.height; y++)
            {
                //field.minefield[x, y].GetComponent<SpriteRenderer>().sprite = Assets.dictionary[RegionNames.MINECELL3];

                field.minefield[x, y].gameObject.transform.position = new Vector3(
                    -widthHalfSize+ field.minefield[x, y].GetComponent<SpriteRenderer>().sprite.bounds.size.x / 2
                    + x*field.minefield[x,y].GetComponent<SpriteRenderer>().sprite.bounds.size.x, 
                    -heightHalfSize+ field.minefield[x, y].GetComponent<SpriteRenderer>().sprite.bounds.size.y / 2
                    + y*field.minefield[x, y].GetComponent<SpriteRenderer>().bounds.size.y, 0);
            }
        }
    }
}
