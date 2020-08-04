using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineField : MonoBehaviour
{

    public int width = 8;//default val
    public int height = 12;//default val
    public int mines = 10;
    public int minesLeft;
    public GameObject[,] minefield;

    void Start()
    {
        minefield = new GameObject[width,height];
        for(int y = 0; y < height; y++)
        {
            for(int x = 0; x < width; x++)
            {
                minefield[x, y] = new GameObject();
                minefield[x, y].AddComponent<Mine>();
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
                minefield[x, y].GetComponent<Mine>().state = Mine.Cellstate.MINE_EMPTY;
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
            if (minefield[x, y].GetComponent<Mine>().state != Mine.Cellstate.MINE)
            {
                minefield[x, y].GetComponent<Mine>().state = Mine.Cellstate.MINE;
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
                if(minefield[x,y].GetComponent<Mine>().state != Mine.Cellstate.MINE)
                {
                    minefield[x, y].GetComponent<Mine>().state = (Mine.Cellstate)minesNear(x, y);
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
        if (y >= 0 && y < height && x >= 0 && x < width && minefield[x,y].GetComponent<Mine>().state == Mine.Cellstate.MINE)
        {
            return 1;
        }
        else
        {
            return 0;
        }
    }
}
