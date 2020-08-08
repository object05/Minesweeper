using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UI;

public class Assets : MonoBehaviour
{
    public SpriteAtlas atlas;
    public static Dictionary<string, Sprite> dictionary;
    public Animation exp_animation;

    public static AnimationClip explosion;

    public static float cellWidth;
    public static float cellHeight;

    void Awake()
    {

        //foreach (AnimationState state in exp_animation)
        //{
        //    state.speed = 0.5F;
        //}
        //explosion = exp_animation.clip;

        dictionary = new Dictionary<string, Sprite>();
        dictionary.Add(RegionNames.MINE, atlas.GetSprite(RegionNames.MINE));
        dictionary.Add(RegionNames.MINECELL, atlas.GetSprite(RegionNames.MINECELL));
        dictionary.Add(RegionNames.MINECELL1, atlas.GetSprite(RegionNames.MINECELL1));
        dictionary.Add(RegionNames.MINECELL2, atlas.GetSprite(RegionNames.MINECELL2));
        dictionary.Add(RegionNames.MINECELL3, atlas.GetSprite(RegionNames.MINECELL3));
        dictionary.Add(RegionNames.MINECELL4, atlas.GetSprite(RegionNames.MINECELL4));
        dictionary.Add(RegionNames.MINECELL5, atlas.GetSprite(RegionNames.MINECELL5));
        dictionary.Add(RegionNames.MINECELL6, atlas.GetSprite(RegionNames.MINECELL6));
        dictionary.Add(RegionNames.MINECELL7, atlas.GetSprite(RegionNames.MINECELL7));
        dictionary.Add(RegionNames.MINECELL8, atlas.GetSprite(RegionNames.MINECELL8));
        dictionary.Add(RegionNames.MINECELL_EMPTY, atlas.GetSprite(RegionNames.MINECELL_EMPTY));
        dictionary.Add(RegionNames.MINECELL_FLAG, atlas.GetSprite(RegionNames.MINECELL_FLAG));
        dictionary.Add(RegionNames.SMILE_DEAD, atlas.GetSprite(RegionNames.SMILE_DEAD));
        dictionary.Add(RegionNames.SMILE_HAPPY, atlas.GetSprite(RegionNames.SMILE_HAPPY));
        dictionary.Add(RegionNames.SMILE_SCARED, atlas.GetSprite(RegionNames.SMILE_SCARED));
        dictionary.Add(RegionNames.SMILE_WIN, atlas.GetSprite(RegionNames.SMILE_WIN));


    }



}
