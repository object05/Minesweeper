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
}
