using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using UnityEngine;

namespace EightBitSoundGenerator
{
    public enum SoundGenType
    {
        PULSE,
        TRIANGLE,
        SINE,
        SQUARE,
        SAWTOOTH,
        NOISE,

        MAX_COUNT
    }

    public class SoundClip
    {
        static int[] randomNoise = { UnityEngine.Random.Range(63, 127),
        UnityEngine.Random.Range(63, 127),
        UnityEngine.Random.Range(63, 127),
        UnityEngine.Random.Range(63, 127),
        UnityEngine.Random.Range(63, 127),
        UnityEngine.Random.Range(63, 127),
        UnityEngine.Random.Range(63, 127),
        UnityEngine.Random.Range(63, 127),
        UnityEngine.Random.Range(63, 127),
        UnityEngine.Random.Range(63, 127),
        UnityEngine.Random.Range(63, 127),
        UnityEngine.Random.Range(63, 127),
        UnityEngine.Random.Range(63, 127),
        UnityEngine.Random.Range(63, 127),
        UnityEngine.Random.Range(63, 127),
        UnityEngine.Random.Range(63, 127),
        UnityEngine.Random.Range(63, 127),
        UnityEngine.Random.Range(63, 127),
        UnityEngine.Random.Range(63, 127),
        UnityEngine.Random.Range(63, 127),
        UnityEngine.Random.Range(63, 127),
        UnityEngine.Random.Range(63, 127),
        UnityEngine.Random.Range(63, 127),
        UnityEngine.Random.Range(63, 127),
        UnityEngine.Random.Range(63, 127),
        };

        float[] data;
        SByte GetSample(SoundGenType type, int hz, int i, int sample)
        {
            SByte ret = (SByte)0;
            float t = i / (float)sample;
            float freq = t * hz * Mathf.PI * 2.0f;
            float sl = (float)sample / hz;

            switch (type)
            {
                case SoundGenType.PULSE:
                    ret = (SByte)(Mathf.Sin(freq) * Mathf.Max(1.0f - t, 0.0f) * 127);
                    break;
                case SoundGenType.TRIANGLE:
                    ret = (SByte)((Math.Abs(((i + sl / 2) % (float)(sl)) / (sl)) * 2.0f) * 127);
                    break;
                case SoundGenType.SINE:
                    ret = (SByte)(Mathf.Sin(freq) * 127);
                    break;
                case SoundGenType.SQUARE:
                    ret = (SByte)(((Mathf.Round(((i % (float)(sl)) / (sl))))) * 127);
                    break;
                case SoundGenType.SAWTOOTH:
                    ret = (SByte)((((i % (float)(sl)) / (sl))) * 127);
                    break;
                case SoundGenType.NOISE:
                    ret = (SByte)(((Mathf.Round(((i % (float)(sl)) / (sl))))) * randomNoise[i % randomNoise.Length]);
                    break;
            }
            return ret;
        }

        public SoundClip(SoundGenType type, int sample, int hz, int hz2, int harmonicNum, float time, bool loopType = false)
        {
            int count = (int)((float)sample * time);

            if(hz == hz2)
                count = Mathf.RoundToInt((float)sample * time / hz) * hz;

            data = new float[count];

            for(int i = 0; i < count; i++)
            {
                int chz = (int)Mathf.Lerp(hz, hz2, (float)i / count);
                data[i] += GetSample(type, chz, i, sample) / 128.0f;
                for (int j = 1; j <= harmonicNum; j++)
                    data[i] += GetSample(type, chz, i * j, sample) * ((float)(harmonicNum - j) / (float)(harmonicNum)) / 128.0f;

                if(harmonicNum > 1)
                    data[i] /= 2.7f;
                else if(harmonicNum > 0)
                    data[i] /= 1.5f;
            }

            if (!loopType && data.Length > 20)
            {
                for (int i = 0; i < 10; i++)
                {
                    data[i] *= i / 10.0f;
                    data[count - 1 - i] *= i / 10.0f;
                }
            }
        }

        public float[] Data
        {
            get
            {
                return data;
            }
        }
    }
}
