﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineFieldRenderer : MonoBehaviour
{
    private MineField field;
    public bool started = false;

    float heightHalfSize;
    float widthHalfSize;

    Vector2 tileDimensions;

    public void beginGame()
    {
        started = true;
        field = gameObject.GetComponent<MineField>();
        if(field.minefield != null)//to clear previous cells if any
        {
            field.DestroyAll();
        }
        field.beginGame();
        heightHalfSize = Camera.main.orthographicSize;
        widthHalfSize = heightHalfSize * Camera.main.aspect;
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
        if (started && GameManager.instance.state != GameManager.GameState.PAUSE)
        {
            updateBoard();
        }
    }

    void updateBoard()
    {
        for(int x = 0; x < field.width; x++)
        {
            for(int y = 0; y < field.height; y++)
            {
                if (field.minefield[x, y].GetComponent<CustomAnimator>().busy)
                {
                    continue;
                }
                switch (field.minefield[x, y].GetComponent<Mine>().state)
                {
                    case Mine.CLICK_OPEN + Mine.MINE_EMPTY:
                        field.minefield[x,y].GetComponent<SpriteRenderer>().sprite = Assets.instance.dictionary[RegionNames.MINECELL_EMPTY];
                        continue;
                    case Mine.CLICK_OPEN + Mine.MINE_NEAR_1:
                        field.minefield[x, y].GetComponent<SpriteRenderer>().sprite = Assets.instance.dictionary[RegionNames.MINECELL1];
                        continue;
                    case Mine.CLICK_OPEN + Mine.MINE_NEAR_2:
                        field.minefield[x, y].GetComponent<SpriteRenderer>().sprite = Assets.instance.dictionary[RegionNames.MINECELL2];
                        continue;
                    case Mine.CLICK_OPEN + Mine.MINE_NEAR_3:
                        field.minefield[x, y].GetComponent<SpriteRenderer>().sprite = Assets.instance.dictionary[RegionNames.MINECELL3];
                        continue;
                    case Mine.CLICK_OPEN + Mine.MINE_NEAR_4:
                        field.minefield[x, y].GetComponent<SpriteRenderer>().sprite = Assets.instance.dictionary[RegionNames.MINECELL4];
                        continue;
                    case Mine.CLICK_OPEN + Mine.MINE_NEAR_5:
                        field.minefield[x, y].GetComponent<SpriteRenderer>().sprite = Assets.instance.dictionary[RegionNames.MINECELL5];
                        continue;
                    case Mine.CLICK_OPEN + Mine.MINE_NEAR_6:
                        field.minefield[x, y].GetComponent<SpriteRenderer>().sprite = Assets.instance.dictionary[RegionNames.MINECELL6];
                        continue;
                    case Mine.CLICK_OPEN + Mine.MINE_NEAR_7:
                        field.minefield[x, y].GetComponent<SpriteRenderer>().sprite = Assets.instance.dictionary[RegionNames.MINECELL7];
                        continue;
                    case Mine.CLICK_OPEN + Mine.MINE_NEAR_8:
                        field.minefield[x, y].GetComponent<SpriteRenderer>().sprite = Assets.instance.dictionary[RegionNames.MINECELL8];
                        continue;

                }
                if(GameManager.instance.state == GameManager.GameState.END_LOSE)
                {
                    if ((field.minefield[x, y].GetComponent<Mine>().state & Mine.MINE) == Mine.MINE)
                    {
                        if (field.minefield[x, y].GetComponent<CustomAnimator>().busy == false)
                        {
                            field.minefield[x, y].GetComponent<SpriteRenderer>().sprite = Assets.instance.dictionary[RegionNames.MINE];//TODO PLAY EXPLOSION AND SOUND HERE
                            continue;
                        }
                    }
                }

                if((field.minefield[x,y].GetComponent<Mine>().state & Mine.CLICK_MARK) == Mine.CLICK_MARK)
                {
                    field.minefield[x, y].GetComponent<SpriteRenderer>().sprite = Assets.instance.dictionary[RegionNames.MINECELL_FLAG];
                }
                else
                {
                    field.minefield[x, y].GetComponent<SpriteRenderer>().sprite = Assets.instance.dictionary[RegionNames.MINECELL];
                }
            }
        }
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
    bool isPlaying(Animator animator)
    {
        return animator.GetCurrentAnimatorStateInfo(0).length >
               animator.GetCurrentAnimatorStateInfo(0).normalizedTime;
    }


}
