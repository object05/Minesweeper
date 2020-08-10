using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace EightBitSoundGenerator
{
    public class MusicPart




    {
        private List<MusicalNote> musicalNotesList = new List<MusicalNote>();

        private List<MusicalNote> selectNotes = new List<MusicalNote>();

        int playPos = 0;
        int sample = 4000;
        string name = "default";

        float speed = 1.0f;

        private void Sort()
        {
            musicalNotesList.Sort((a, b) =>
            {
                return a.StartTime == b.StartTime ? 0 :
                    (a.StartTime < b.StartTime ? -1 : 1);
            });
        }

        public float Length
        {
            get; set;
        }

        public SoundGenType Timbre
        {
            get;set;
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

        public int Sample
        {
            get
            {
                return sample;
            }
            set
            {
                sample = value;
            }
        }

        public MusicPart Clone()
        {
            MusicPart newPart = new MusicPart();
            newPart.sample = sample;
            newPart.name = name + "(Clone)";
            newPart.Length = Length;
            newPart.Timbre = Timbre;

            foreach (var n in musicalNotesList)
            {
                newPart.musicalNotesList.Add(n.Clone());
            }

            return newPart;
        }

        void AddSelect(MusicalNote note)
        {
            if (selectNotes.Contains(note))
                return;

            selectNotes.Add(note);
        }

        public void Select(int startNote, int endNote, float startTime, float EndTime)
        {
            CleanSelected();

            foreach (var n in musicalNotesList)
            {
                Rect rect = new Rect(n.StartTime, n.Note, n.KeepTime, 1);
                Rect rect2 = new Rect(startTime, startNote, EndTime - startTime, endNote - startNote);

                if(rect.Overlaps(rect2, true))
                {
                    AddSelect(n);
                }
            }
        }

        public void Select(int note, float time)
        {
            CleanSelected();

            foreach (var n in musicalNotesList)
            {
                if (n.Note == note)
                {
                    if (n.StartTime <= time && n.StartTime + n.KeepTime > time + 0.05f)
                    {
                        AddSelect(n);
                        break;
                    }
                }
            }
        }
         
        public void CleanSelected()
        {
            selectNotes.Clear();
        }

        public void UpdateLength()
        {
            Length = 0;
            foreach (var n in musicalNotesList)
            {
                Length = Mathf.Max(n.StartTime + n.KeepTime, Length);
            }
        }

        public void Save(BinaryWriter writer)
        {

            writer.Write(Name);
            writer.Write(Length);
            writer.Write((byte)Timbre);

            Sort();

            UpdateLength();

            int count = musicalNotesList.Count;

            writer.Write(count);

            foreach(var n in musicalNotesList)
            {
                n.Save(writer);
            }
        }

        public void Load(BinaryReader reader)
        {
            Name = reader.ReadString();
            Length = reader.ReadSingle();
            Timbre = (SoundGenType)reader.ReadByte();

            musicalNotesList = new List<MusicalNote>();

            int count = reader.ReadInt32();

            for(int i = 0; i < count; i++)
            {
                MusicalNote n = new MusicalNote();
                n.Load(reader);

                musicalNotesList.Add(n);
            }
        }

        public void AddNote(int n, float startTime, float keepTime)
        {
            if (keepTime < 0)
            {
                startTime += keepTime;
                keepTime = -keepTime;
            }

            Rect rect = new Rect(startTime, n, keepTime, 1);
            foreach (var nn in musicalNotesList)
            {
                Rect nrect = new Rect(nn.StartTime, nn.Note, nn.KeepTime, 1);

                if (rect.Overlaps(nrect))
                    return;
            }

            MusicalNote note = new MusicalNote();
            note.Note = n;
            note.StartTime = startTime;
            note.KeepTime = keepTime;

            musicalNotesList.Add(note);

            Length = Mathf.Max(startTime + keepTime, Length);
        }
        public MusicalNote[] Notes
        {
            get
            {
                return musicalNotesList.ToArray();
            }
        }

        public MusicalNote[] Selected
        {
            get
            {
                return selectNotes.ToArray();
            }
        }

        public void RemoveNote(MusicalNote note)
        {
            musicalNotesList.Remove(note);
        }

        public void RemoveSelected()
        {
            foreach (var n in selectNotes)
                musicalNotesList.Remove(n);

            selectNotes.Clear();
        }

        public void MoveSelected(float startTime, float startNote, float targetTime, float targetNote, float stepSize = 0.25f)
        {
            float Xmove = targetTime - startTime;
            float Ymove = targetNote - startNote;

            Xmove = Mathf.Floor(Xmove / stepSize) * stepSize;

            bool found = false;
            foreach (var n in selectNotes)
            {
                Rect rect = new Rect(n.StartTime + Xmove, n.Note + Ymove, n.KeepTime, 1);

                if(n.StartTime + Xmove < 0)
                {
                    found = true;
                    break;
                }

                if(n.Note + Ymove < 0 || n.Note + Ymove >= MusicalNote.Notes.Length)
                {
                    found = true;
                    break;
                }

                foreach (var nn in musicalNotesList)
                {
                    if (selectNotes.Contains(nn))
                        continue;

                    Rect nrect = new Rect(nn.StartTime, nn.Note, nn.KeepTime, 1);

                    if (rect.Overlaps(nrect))
                    {
                        found = true;
                        break;
                    }
                }
            }

            if (found)
                return;

            foreach (var n in selectNotes)
            { 
                n.Note += (int)Ymove;
                n.StartTime += Xmove;
            }
        }

        public void ChangeSelectedKeepTime(float keepTime)
        {
            foreach (var n in selectNotes)
            {
                n.KeepTime = keepTime;
            }
        }
        //public void MoveNote(MusicalNote note, float targetTime, int targetHz)
        //{
        //    note.StartTime = targetTime;
        //    note.Hz = targetHz;
        //}

        //public void ChangeNoteLeaps(MusicalNote note, LeapsTypes leaps, int leapsHz)
        //{      
        //    note.LeapsType = leaps;
        //    note.LeapsHz = leapsHz;
        //}

        //public void ChangeNotePower(MusicalNote note, float power)
        //{
        //    note.Power = power;
        //}

        //public void ChangeNoteLength(MusicalNote note, float keepTime)
        //{
        //    note.KeepTime = keepTime;
        //}

        public float[] ExportData(float speed)
        {
            Sort();

            int len = (int)(Length * Sample * speed);

            float[] data = new float[len];

            foreach(var n in musicalNotesList)
            {
                if (n.Note < 0)
                    continue;

                SoundClip clip = new SoundClip(Timbre, Sample, MusicalNote.NoteHz[n.Note], MusicalNote.NoteHz[n.Note], 0, (n.KeepTime - 0.05f) * speed);

                int start = (int)(n.StartTime * Sample * speed);

                if (start >= len)
                    break;

                float[] buf = clip.Data;
                for(int i = 0; i < buf.Length; i++)
                {
                    if (start + i < len)
                        data[start + i] += buf[i];
                }
            }

            return data;
        }

        public AudioClip Play(float speed) {
            Sort();

            int len = (int)(Length * Sample * speed);
            this.speed = speed;

            playPos = 0;

            if (name == null)
                name = "default";

            AudioClip clip = AudioClip.Create(name, len, 1, Sample, true, PCMReaderCallback, PCMPositionCallback);

            return clip;
        }

        void PCMReaderCallback(float[] buffer)
        {
            float time = (float)playPos / (Sample * speed);

            int current = playPos;

            for (int i = 0; i < buffer.Length; i++)
                buffer[i] = 0.0f;

            foreach (var n in musicalNotesList)
            {
                if (n.StartTime + n.KeepTime < time)
                    continue;

                if (n.Note < 0)
                {
                    continue;
                }

                SoundClip clip = new SoundClip(Timbre, Sample, MusicalNote.NoteHz[n.Note], MusicalNote.NoteHz[n.Note], 0, (n.KeepTime - 0.05f) * speed);

                int start = (int)(n.StartTime * (Sample * speed));

                float[] buf = clip.Data;
                int ns = start - playPos;
                for (int i = 0; i < buf.Length; i++)
                {
                    if (i + ns >= buffer.Length)
                        break;

                    if(i + ns >= 0)
                    {
                        current = Mathf.Max(current, playPos + i + ns);
                        buffer[i + ns] += buf[i];
                    }
                }
            }

            playPos += buffer.Length;
        }

        void PCMPositionCallback(int pos)
        {
            playPos = pos;
        }
    }
}
