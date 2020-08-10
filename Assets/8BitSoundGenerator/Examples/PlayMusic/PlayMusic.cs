using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class PlayMusic : MonoBehaviour
{
    EightBitSoundGenerator.Music music;

    public TextAsset musicFile;
    // Start is called before the first frame update
    void Start()
    {
        music = new EightBitSoundGenerator.Music();

        MemoryStream stream = new MemoryStream(musicFile.bytes);
        BinaryReader reader = new BinaryReader(stream);
        music.Load(reader);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Play()
    {
        music.Play(gameObject);
    }

    public void Pause()
    {
        if (music.IsPlaying)
            music.Pause();
        else
            music.Resume();
    }

    public void Stop()
    {
        music.Stop();
    }
}
