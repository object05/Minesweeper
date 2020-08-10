using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace EightBitSoundGenerator
{
    public class Music
    {
        List<MusicPart> partsList = new List<MusicPart>();
        float speed = 60.0f / 100.0f;
        float time = 0.0f;

        float duration = 1.0f;

        string name = "new music";

        const int version = 1;

        List<AudioSource> sourceList = new List<AudioSource>();

        GameObject temp;

        public Music()
        {
            NewPart();
        }

        public void Save(BinaryWriter writer)
        {
            UpdateParts();

            writer.Write("8BM_");
            writer.Write(version);

            writer.Write(name);
            writer.Write(speed);

            writer.Write(partsList.Count);

            for(int i = 0; i < partsList.Count; i++)
            {
                partsList[i].Save(writer);
            }
        }

        public void Load(BinaryReader reader)
        {
            string mark = reader.ReadString();
            if(mark != "8BM_")
            {
                Debug.LogError("File is not 8bit sound generator format!");
                return;
            }

            int ver = reader.ReadInt32();
            if(ver != version)
            {
                Debug.LogError("File version not fit!");
                return;
            }

            name = reader.ReadString();
            speed = reader.ReadSingle();

            int count = reader.ReadInt32();
            partsList.Clear();

            for(int i = 0; i < count; i++)
            {
                MusicPart part = new MusicPart();
                part.Load(reader);

                partsList.Add(part);
            }
        }

        public void Play(GameObject baseObject = null)
        {
            Stop();

            UpdateParts();

            if (!baseObject)
            {
                baseObject = new GameObject();

                baseObject.name = name;

                temp = baseObject;
            }

            foreach (var p in partsList)
            {
                AudioClip clip = p.Play(speed);
                AudioSource cur = baseObject.AddComponent<AudioSource>();
                cur.clip = clip;
                sourceList.Add(cur);
            }

            foreach(var s in sourceList)
            {
                s.Play();
            }
        }

        public void Pause()
        {
            foreach (var s in sourceList)
            {
                s.Pause();
            }
        }

        public void Resume()
        {
            foreach (var s in sourceList)
            {
                s.Play();
            }

        }

        public void Stop()
        {
            foreach (var s in sourceList)
            {
                if (s)
                {
                    UnityEngine.Object.DestroyImmediate(s.clip);
                    s.Stop();
                    UnityEngine.Object.DestroyImmediate(s);
                }
            }

            sourceList.Clear();

            if (temp)
                UnityEngine.Object.DestroyImmediate(temp);
            temp = null;
        }

        public float[] ExportData()
        {
            float[] ret = new float[(int)(duration * 4000)];

            return ret;
        }

        public MusicPart NewPart()
        {
            MusicPart part = new MusicPart();
            partsList.Add(part);
            return part;
        }

        public void RemovePart(MusicPart part)
        {
            partsList.Remove(part);
        }

        public void RemovePartAt(int partId)
        {
            if (partId < 0 || partId >= partsList.Count)
                return;

            partsList.RemoveAt(partId);
        }

        public int GetPartsNum()
        {
            return partsList.Count;
        }

        public MusicPart GetPart(int partId)
        {
            if (partId < 0 || partId >= partsList.Count)
                return null;

            return partsList[partId];
        }

        public float Time
        {
            get
            {
                if (sourceList.Count > 0 && sourceList[0])
                    time = sourceList[0].time / speed;
                return time;
            }
            set
            {
                SetTime(value * speed);
            }
        }

        public float Duration
        {
            get
            {
                return duration;
            }
        }

        public int Speed
        {
            get
            {
                return Mathf.RoundToInt(60 / speed);
            }
            set
            {
                speed = 60.0f / value;
            }
        }

        void SetTime(float t)
        {
            time = Mathf.Clamp(t, 0.0f, Duration);

            foreach (var s in sourceList)
            {
                if (s)
                    s.time = Mathf.Min(time,s.clip.length - 0.05f);
            }
        }

        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
            }
        }

        public void UpdateParts()
        {
            duration = 0.0f;
            foreach (var p in partsList)
            {
                p.UpdateLength();
                duration = Mathf.Max(duration, p.Length);
            }
        }

        public bool IsPlaying
        {
            get
            {
                if (sourceList.Count > 0 && sourceList[0])
                    return sourceList[0].isPlaying;

                return false;
            }
        }
    }
}
