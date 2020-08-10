using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UI;
using EightBitSoundGenerator;
using System.IO;

public class Assets : MonoBehaviour
{
    public static Assets instance;



    public SpriteAtlas atlas;
    public Dictionary<string, Sprite> dictionary;
    //public Animation exp_animation;

    //public AnimationClip explosion;
    public AnimatorController explosionController;

    public float cellWidth;
    public float cellHeight;

    public Music empty;
    public Music mine;


    void Awake()
    {

        MakeSingleton();

        empty = new Music();
        mine = new Music();

        using (BinaryReader reader = new BinaryReader(File.Open(FilePaths.SOUND_EMPTY, FileMode.Open)))
        {
            empty.Load(reader);
        }
        using (BinaryReader reader = new BinaryReader(File.Open(FilePaths.SOUND_MINE, FileMode.Open)))
        {
            mine.Load(reader);
        }


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


        dictionary.Add(RegionNames.MINEB_0, atlas.GetSprite(RegionNames.MINEB_0));
        dictionary.Add(RegionNames.MINEB_1, atlas.GetSprite(RegionNames.MINEB_1));
        dictionary.Add(RegionNames.MINEB_2, atlas.GetSprite(RegionNames.MINEB_2));
        dictionary.Add(RegionNames.MINEB_3, atlas.GetSprite(RegionNames.MINEB_3));
        dictionary.Add(RegionNames.MINEB_4, atlas.GetSprite(RegionNames.MINEB_4));
        dictionary.Add(RegionNames.MINEB_5, atlas.GetSprite(RegionNames.MINEB_5));
        dictionary.Add(RegionNames.MINEB_6, atlas.GetSprite(RegionNames.MINEB_6));
        dictionary.Add(RegionNames.MINEB_7, atlas.GetSprite(RegionNames.MINEB_7));
        dictionary.Add(RegionNames.MINEB_8, atlas.GetSprite(RegionNames.MINEB_8));
        dictionary.Add(RegionNames.MINEB_9, atlas.GetSprite(RegionNames.MINEB_9));
    }

    void MakeSingleton()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }




}
