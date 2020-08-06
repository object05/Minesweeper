﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineField : MonoBehaviour
{

    public int width = 8;//default val
    public int height = 12;//default val
    public int mines = 10;
    public int minesLeft;
    public GameObject[,] minefield;

    public float unitDimension;

    void Start()
    {
        minefield = new GameObject[width,height];
        for(int y = 0; y < height; y++)
        {
            for(int x = 0; x < width; x++)
            {
                minefield[x, y] = new GameObject();
                minefield[x, y].AddComponent<Mine>();
                minefield[x, y].name = "Cell [" + x + "," + y + "]";
            }
        }
    }


    public void clearAndInit()
    {
        minesLeft = mines;
        clearMineField();
        placeMines();
        calculateHints();

    }

    public void clearMineField()
    {
        for(int y = 0; y < height; y++)
        {
            for(int x = 0; x < width; x++)
            {
                minefield[x, y].GetComponent<Mine>().state = Mine.MINE_EMPTY;
            }
        }
    }

    public void placeMines()
    {
        int minesPlaced = 0;
        while(minesPlaced < mines)
        {
            int x = Random.Range(0, width - 1);
            int y = Random.Range(0, height - 1);
            if (minefield[x, y].GetComponent<Mine>().state != Mine.MINE)
            {
                minefield[x, y].GetComponent<Mine>().state = Mine.MINE;
                minesPlaced++;
            }
        }
    }
    public void calculateHints()
    {
        for(int y = 0; y < height; y++)
        {
            for(int x = 0; x < width; x++)
            {
                if(minefield[x,y].GetComponent<Mine>().state != Mine.MINE)
                {
                    minefield[x, y].GetComponent<Mine>().state = minesNear(x, y);
                }
            }
        }
    }

    public int minesNear(int x, int y)
    {
        int mines = 0;
        mines += mineAt(x - 1, y - 1);
        mines += mineAt(x, y - 1);
        mines += mineAt(x + 1, y - 1);
        mines += mineAt(x - 1, y);
        mines += mineAt(x + 1, y);
        mines += mineAt(x - 1, y + 1);
        mines += mineAt(x, y + 1);
        mines += mineAt(x + 1, y + 1);
        return 2<<mines;
    }

    public int mineAt(int x, int y)
    {
        if (y >= 0 && y < height && x >= 0 && x < width && minefield[x,y].GetComponent<Mine>().state == Mine.MINE)
        {
            return 1;
        }
        else
        {
            return 0;
        }
    }

    public bool markClick(GameObject obj)
    {
        if((obj.GetComponent<Mine>().state & Mine.CLICK_MARK) == Mine.CLICK_MARK)
        {
            obj.GetComponent<Mine>().state = obj.GetComponent<Mine>().state & ~Mine.CLICK_MARK;
            minesLeft++;
            return false;
        }
        else
        {
            obj.GetComponent<Mine>().state = obj.GetComponent<Mine>().state | Mine.CLICK_MARK;
            minesLeft--;
            return true;
        }
    }

    public void clickAll()
    {
        for(int y = 0; y < height; y++)
        {
            for(int x = 0; x < width; x++)
            {
                minefield[x, y].GetComponent<Mine>().state = minefield[x, y].GetComponent<Mine>().state | Mine.CLICK_OPEN;
            }
        }
    }

    public void clickOne(int x, int y)
    {
        if (x == width) return;
        if (x < 0) return;
        if (y == height) return;
        if (y < 0) return;
        if ((minefield[x,y].GetComponent<Mine>().state & Mine.CLICK_OPEN) == Mine.CLICK_OPEN) return; //already clicked
        if ((minefield[x,y].GetComponent<Mine>().state & Mine.CLICK_MARK) == Mine.CLICK_MARK) return; //position marked
        if (minefield[x,y].GetComponent<Mine>().state == Mine.MINE) return; //it is a mine
        minefield[x,y].GetComponent<Mine>().state = minefield[x,y].GetComponent<Mine>().state | Mine.CLICK_OPEN; //mark clicked
        if ((minefield[x, y].GetComponent<Mine>().state & Mine.MINE_EMPTY) == Mine.MINE_EMPTY)
        { //empty clear soround fields
            clickOne(x - 1, y - 1);
            clickOne(x + 1, y - 1);
            clickOne(x - 1, y + 1);
            clickOne(x + 1, y + 1);
            clickOne(x - 1, y);
            clickOne(x + 1, y);
            clickOne(x, y - 1);
            clickOne(x, y + 1);
        }
    }

    public bool isWin()
    {
        return openMarked() == (width * height - mines);
    }

    public bool mineAt(GameObject obj)
    {
        if(obj.GetComponent<Mine>().state == Mine.MINE)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    int openMarked()
    {
        int sum = 0;
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                if ((minefield[x,y].GetComponent<Mine>().state & Mine.CLICK_OPEN) == Mine.CLICK_OPEN)
                {
                    sum++;
                }
            }
        }
        return sum;
    }

}
