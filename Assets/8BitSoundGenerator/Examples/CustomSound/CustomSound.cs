using EightBitSoundGenerator;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CustomSound : MonoBehaviour
{
    public Slider freqCtrl;
    public Dropdown timbreCtrl;
    public Toggle autoCtrl;

    SoundGenType type = SoundGenType.PULSE;

    int hz = 10;
    int lastHz = 10;

    bool auto = true;

    float timeCtrl = 0.0f;
    float step = 1.0f;
    // Start is called before the first frame update
    void Start()
    {
        AudioSource source = gameObject.AddComponent<AudioSource>();

        freqCtrl.minValue = 10;
        freqCtrl.maxValue = 600;

        freqCtrl.onValueChanged.AddListener((v) =>
        {
            lastHz = hz;
            hz = (int)v;
        });

        List<string> ops = new List<string>();
        for(int i=0;i< (int)SoundGenType.MAX_COUNT; i++)
        {
            ops.Add(((SoundGenType)i).ToString());
        }
        timbreCtrl.ClearOptions();
        timbreCtrl.AddOptions(ops);

        timbreCtrl.onValueChanged.AddListener((v) =>
        {
            type = (SoundGenType)v;
        });

        autoCtrl.isOn = true;

        autoCtrl.onValueChanged.AddListener((v) =>
        {
            auto = v;
        });
    }

    // Update is called once per frame
    void Update()
    {
        float dt = Time.deltaTime;

        SoundClip sound = new SoundClip(type, 4000, lastHz, hz, 0, dt, true);

        float[] data = sound.Data;

        if (data.Length > 0)
        {
            AudioClip clip = AudioClip.Create("", data.Length, 1, 4000, false);
            clip.SetData(data, 0);

            AudioSource source = gameObject.GetComponent<AudioSource>();
            source.clip = clip;
            source.Play();
        }

        if (auto)
        {
            timeCtrl += dt;

            if (timeCtrl > 3.0f)
            {
                timeCtrl = 0.0f;

                step = UnityEngine.Random.Range(-3.0f, 3.0f);
            }

            freqCtrl.value += step;
        }
    }
}
