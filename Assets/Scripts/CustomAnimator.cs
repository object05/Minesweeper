using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class CustomAnimator : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    public bool busy;
    public bool working;

    public int frameNum;
    public Sprite[] frames;
    public int currentFrame;

    public float interval = 0.1f;
    float tempTime;


    // Start is called before the first frame update
    void Awake()
    {
        busy = false;
        working = false;
    }

    void Update()
    {
        if (working)
        {
            tempTime += Time.deltaTime;
            if (tempTime > interval)
            {
                tempTime = 0;
                spriteRenderer.sprite = frames[currentFrame];
                currentFrame++;
                if(currentFrame >= frames.Length)
                {
                    working = false;
                    busy = false;
                }

            }
        }
    }
    public void PlayOnce()
    {
        working = true;
        busy = true;
        currentFrame = 0;
    }


    public void Init(int framesCount)
    {
        frameNum = framesCount;
        frames = new Sprite[frameNum];
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();

    }

    //public void PlayOnce()
    //{
    //    bool end = false;
    //    busy = true;
    //    float last = 0;

    //    while (!end)
    //    {
    //        if (last >= 0.3)
    //        {
    //            spriteRenderer.sprite = frames[currentFrame];
    //            currentFrame++;
    //            Debug.Log(last+interval+">="+0.3f);
    //            if (currentFrame >= frames.Length)
    //            {
    //                end = true;
    //            }
    //            last = 0;
    //        }
    //        last += Time.deltaTime;
    //    }
    //    busy = false;
    //}




    //public void PlayOnce()
    //{
    //    timer = 0;
    //    busy = true;
    //    currentFrame = 0;
    //    bool end = false;

    //    while (!end)
    //    {
    //        timer += Time.deltaTime;
    //        if (timer >= framerate)
    //        {
    //            timer -= framerate;
    //            spriteRenderer.sprite = frames[currentFrame];
    //            Debug.Log(spriteRenderer.sprite);
    //            currentFrame++;
    //            if(currentFrame >= frames.Length)
    //            {
    //                end = true;
    //            }
    //        }
    //    }
    //    busy = false;
    //}
}
