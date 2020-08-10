using EightBitSoundGenerator;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomSound2 : MonoBehaviour
{
    public Camera camera;
    public void PlaySound0()
    {
        SoundClip sound = new SoundClip(SoundGenType.TRIANGLE, 4000, 20, 300, 0, 5.0f, false);
        AudioClip clip = AudioClip.Create("", sound.Data.Length, 1, 4000, false);
        clip.SetData(sound.Data, 0);

        Vector3 pos = camera ? camera.transform.position : new Vector3();
        AudioSource.PlayClipAtPoint(clip, pos);
    }

    public void PlaySound1()
    {
        SoundClip sound = new SoundClip(SoundGenType.SINE, 4000, 600, 350, 0, 3.0f, false);
        AudioClip clip = AudioClip.Create("", sound.Data.Length, 1, 4000, false);
        clip.SetData(sound.Data, 0);

        Vector3 pos = camera ? camera.transform.position : new Vector3();
        AudioSource.PlayClipAtPoint(clip, pos);
    }

    public void PlaySound2()
    {
        SoundClip sound = new SoundClip(SoundGenType.SQUARE, 4000, 200, 700, 0, 7.0f, false);
        AudioClip clip = AudioClip.Create("", sound.Data.Length, 1, 4000, false);
        clip.SetData(sound.Data, 0);

        Vector3 pos = camera ? camera.transform.position : new Vector3();
        AudioSource.PlayClipAtPoint(clip, pos);
    }

    public void PlaySound3()
    {
        SoundClip sound = new SoundClip(SoundGenType.PULSE, 4000, 20, 700, 0, 0.2f, false);
        AudioClip clip = AudioClip.Create("", sound.Data.Length, 1, 4000, false);
        clip.SetData(sound.Data, 0);

        Vector3 pos = camera ? camera.transform.position : new Vector3();
        AudioSource.PlayClipAtPoint(clip, pos);
    }

    public void PlayMix()
    {
        SoundClip sound1 = new SoundClip(SoundGenType.SQUARE, 8000, 350, 500, 0, 1.5f, true);
        SoundClip sound2 = new SoundClip(SoundGenType.SQUARE, 8000, 500, 350, 0, 1.5f, true);

        int repeat = 5;
        AudioClip clip = AudioClip.Create("", (sound1.Data.Length + sound2.Data.Length) * repeat, 1, 8000, false);

        int pp = 0;
        for(int i=0;i< repeat; i++)
        {
            clip.SetData(sound1.Data, pp);
            pp += sound1.Data.Length;
            clip.SetData(sound2.Data, pp);
            pp += sound2.Data.Length;
        }

        Vector3 pos = camera ? camera.transform.position : new Vector3();
        AudioSource.PlayClipAtPoint(clip, pos);
    }
}
