using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Mine : MonoBehaviour
{

    //public enum Cellstate
    //{
    //    MINE_EMPTY=2<<0,
    //    MINE_NEAR_1=2<<1,
    //    MINE_NEAR_2=2<<2, 
    //    MINE_NEAR_3=2<<3, 
    //    MINE_NEAR_4=2<<4, 
    //    MINE_NEAR_5=2<<5,
    //    MINE_NEAR_6=2<<6, 
    //    MINE_NEAR_7=2<<7, 
    //    MINE_NEAR_8=2<<8, 
    //    MINE=2<<9, 
    //    CLICK_OPEN=2<<10, 
    //    CLICK_MARK=2<<11
    //}

    //public Cellstate state;

    public const int MINE_EMPTY = 2 << 0;
    public const int MINE_NEAR_1 = 2 << 1;
    public const int MINE_NEAR_2 = 2 << 2;
    public const int MINE_NEAR_3 = 2 << 3;
    public const int MINE_NEAR_4 = 2 << 4;
    public const int MINE_NEAR_5 = 2 << 5;
    public const int MINE_NEAR_6 = 2 << 6;
    public const int MINE_NEAR_7 = 2 << 7;
    public const int MINE_NEAR_8 = 2 << 8;
    public const int MINE = 2 << 9;
    public const int CLICK_OPEN = 2 << 10;
    public const int CLICK_MARK = 2 << 11;

    public int state;
    public int x;
    public int y;

    void Awake()
    {
        gameObject.AddComponent<BoxCollider2D>();
        gameObject.AddComponent<clickDetect>();
        gameObject.AddComponent<SpriteRenderer>();
        gameObject.GetComponent<SpriteRenderer>().sprite = Assets.dictionary[RegionNames.MINECELL];
        DontDestroyOnLoad(this);

    }


}
