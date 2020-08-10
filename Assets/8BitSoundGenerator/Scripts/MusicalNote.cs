using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EightBitSoundGenerator
{
    public class MusicalNote
    {
        public static string[] Notes = new string[] {"B5", "A#5", "A5", "G#5","G5", "F#5", "F5", "E5", "D#5", "D5", "C#5", "C5",
    "B4", "A#4", "A4", "G#4","G4", "F#4", "F4", "E4", "D#4", "D4", "C#4", "C4",
    "B3", "A#3", "A3", "G#3","G3", "F#3", "F3", "E3", "D#3", "D3", "C#3", "C3"};

        public static int[] NoteHz = new int[] { 988,932,880,830,784,740,699,660,622,588,554,524,
            494,466,440,415,392,370,349,330,311,294,277,262,
            247,233,220,208,196,185,175,165,156,147,139,131
        };

        public int Note;
        public float StartTime;
        public float KeepTime;

        public MusicalNote Clone()
        {
            MusicalNote newNote = new MusicalNote();
            newNote.Note = Note;
            newNote.StartTime = StartTime;
            newNote.KeepTime = KeepTime;

            return newNote;
        }

        public void Save(BinaryWriter writer)
        {
            writer.Write(Note);
            writer.Write(StartTime);
            writer.Write(KeepTime);
        }

        public void Load(BinaryReader reader)
        {
            Note = reader.ReadInt32();
            StartTime = reader.ReadSingle();
            KeepTime = reader.ReadSingle();
        }
    }
}
