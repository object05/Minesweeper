using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class CustomAnimator : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;

    public bool busy;

    public int frameNum;
    public Sprite[] frames;
    public int currentFrame;
    public float timer;
    public float framerate =0.01f;

    int switcher;
    // Start is called before the first frame update
    void Awake()
    {
        switcher = 0;
        busy = false;
    }

    void Update()//TODO somethings up
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            spriteRenderer.sprite = frames[(switcher + 1) % frames.Length];
        }
    }

    public void Init(int framesCount)
    {
        frameNum = framesCount;
        frames = new Sprite[frameNum];
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();

    }




    public void PlayOnce()
    {
        busy = true;
        currentFrame = 0;
        bool end = false;

        while (!end)
        {
            timer += Time.deltaTime;
            if (timer >= framerate)
            {
                timer -= framerate;
                //currentFrame = (currentFrame + 1) % frames.Length;
                spriteRenderer.sprite = frames[currentFrame];
                Debug.Log(spriteRenderer.sprite);
                currentFrame++;
                if(currentFrame >= frames.Length)
                {
                    end = true;
                }
            }
        }
        busy = false;
    }
}
