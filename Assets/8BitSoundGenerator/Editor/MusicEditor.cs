using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace EightBitSoundGenerator
{
    public class MusicEditor : EditorWindow
    {

        Vector2 viewPos = new Vector2();

        public float scale = 1.0f;

        public Vector2Int selectStart;
        public Vector2Int selectEnd;

        float sizeA = 0.25f;
        int sizeGird = 1;

        public float playTime = 0.0f;

        int noteSpeed = 100;

        public SoundGenType type = SoundGenType.PULSE;

        AudioClip testClip;
        AudioSource testSource;

        Music music;
        MusicPart currentPart;

        List<MusicalNote> copyList = new List<MusicalNote>();

        GameObject testObj;

        [MenuItem("Tools/8BitSoundGenerator/MusicEditor")]
        static public void ShowWindow()
        {
            EditorWindow window = EditorWindow.GetWindow<MusicEditor>();
            window.minSize = new Vector2(640, 380);
            window.Show();
        }

        public MusicEditor()
        {
            music = new Music();
            currentPart = music.GetPart(0);
        }

        public void OnGUI()
        {
            EditorGUILayout.BeginVertical();

            MainView();

            ToolsLine();

            PartInfoLine();

            EditorGUILayout.EndVertical();
        }

        void Update()
        {
            //开启窗口的重绘，不然窗口信息不会刷新
            Repaint();

            if(music.IsPlaying)
                playTime = music.Time;
        }

        private void OnDestroy()
        {
            if (testObj)
                DestroyImmediate(testObj);
            testObj = null;

            if (testClip)
            {
                DestroyImmediate(testSource.gameObject);
                DestroyImmediate(testClip);
                testClip = null;
            }
        }

        void PartInfoLine()
        {
            EditorGUILayout.BeginHorizontal();

            if (GUILayout.Button("Add Part"))
            {
                currentPart = music.NewPart();
            }

            if(music.GetPartsNum() > 1)
            {
                if (GUILayout.Button("Remove Part"))
                {
                    music.RemovePart(currentPart);
                    currentPart = music.GetPart(0);
                }
            }

            string[] Timbres = new string[] { "PULSE", "TRIANGLE", "SINE", "SQUARE", "SAWTOOTH", "NOISE" };
            int newtype = EditorGUILayout.Popup((int)type, Timbres);

            if (newtype != (int)type)
            {
                type = (SoundGenType)newtype;
                currentPart.Timbre = type;
            }

            int n = 0;
            string[] names = new string[music.GetPartsNum()];
            for(int i = 0; i < music.GetPartsNum(); i++)
            {
                names[i] = "part " + (i + 1);
                if (music.GetPart(i) == currentPart)
                {
                    n = i;
                }
            }

            int sel = GUILayout.Toolbar(n, names);
            currentPart = music.GetPart(sel);
            type = currentPart.Timbre;

            EditorGUILayout.EndHorizontal();
        }
        void ToolsLine()
        {
            EditorGUI.DrawRect(new Rect(0, 0, position.width, 60), new Color(0, 0, 0, 1));

            EditorGUILayout.BeginHorizontal();

            if (GUILayout.Button("New"))
            {
                music = new Music();
                currentPart = music.GetPart(0);
                noteSpeed = music.Speed;
            }

            if (GUILayout.Button("Load"))
            {
                string fn = EditorUtility.OpenFilePanel("open file", Application.dataPath, "bytes");
                if (!string.IsNullOrEmpty(fn))
                {
                    using (FileStream stream = new FileStream(fn, FileMode.Open))
                    {
                        BinaryReader reader = new BinaryReader(stream);

                        music = new Music();
                        music.Load(reader);
                        noteSpeed = music.Speed;

                        currentPart = music.GetPart(0);
                    }
                }
            }

            if (music != null)
            {
                if (GUILayout.Button("Save"))
                {
                    string fn = EditorUtility.SaveFilePanel("save file", Application.dataPath, "new music", "bytes");
                    if (!string.IsNullOrEmpty(fn))
                    {
                        using (FileStream stream = new FileStream(fn, FileMode.Create))
                        {
                            BinaryWriter writer = new BinaryWriter(stream);

                            music.Save(writer);
                        }
                    }
                }
            }

            GUILayout.Space(10);

            string[] girdSize = {"1\\8", "1\\4", "1\\2", "1\\3", "1"};
            float[] girds = { 0.125f, 0.25f, 0.5f, 0.33333f, 1.0f };

            sizeGird = EditorGUILayout.Popup(sizeGird, girdSize);
            sizeA = girds[sizeGird];

            string newns = GUILayout.TextArea(noteSpeed.ToString());
            int spd = int.Parse(newns);
                noteSpeed = spd;
            if(spd >50 && spd < 200)
            {
                music.Speed = noteSpeed;
            }

            if (GUILayout.Button("Play"))
            {
                if (!testObj)
                {
                    testObj = new GameObject();
                    testObj.name = "Editor Audio Object";
                }
                music.Play(testObj);
                music.Time = 0;
            }

            if (GUILayout.Button("Pause/Resume"))
            {
                if (music.IsPlaying)
                {
                    playTime = music.Time;
                    music.Stop();
                }
                else
                {
                    if (!testObj)
                    {
                        testObj = new GameObject();
                        testObj.name = "Editor Audio Object";
                    }
                    music.Play(testObj);
                    music.Time = playTime;
                }
            }

            if (GUILayout.Button("Stop"))
            {
                music.Stop();
                playTime = 0;
            }


            EditorGUILayout.EndHorizontal();

            if(Event.current.type == EventType.KeyUp)
            {
                if (Event.current.control)
                {
                    if (Event.current.keyCode == KeyCode.C)
                    {
                        Copy();
                    }else if(Event.current.keyCode == KeyCode.X)
                    {
                        Copy();
                        currentPart.RemoveSelected();
                        currentPart.CleanSelected();

                        music.UpdateParts();
                    }
                    else if(Event.current.keyCode == KeyCode.V)
                    {
                        Paste();
                    }
                }
            }

            //if(Event.current.type == EventType.MouseDown)
            //{
            //    if (music.IsPlaying)
            //    {
            //        music.Stop();
            //    }
            //}
        }

        void MainView()
        {
            EditorGUILayout.BeginHorizontal();

            Rect pos = position;
            pos.x = 0;
            pos.y = 60;
            EditorGUI.DrawRect(pos, new Color(0, 0, 0, 1));

            Rect leftPos = pos;
            Rect rightPos = pos;

            leftPos.y -= viewPos.y;
            rightPos.min -= viewPos;
            rightPos.max -= viewPos;

            DrawRight(rightPos, new Rect(125, 0, pos.width - 125, 1210));

            DrawLeft(leftPos, new Rect(0, 0, 120, 1210));

            EditorGUILayout.EndHorizontal();
        }

        void DrawLeft(Rect screen, Rect size)
        {
            Rect rect = size;
            rect.min += screen.min;
            rect.max += screen.min;

            EditorGUI.DrawRect(rect, new Color(0.5f, 0.5f, 0.5f));

            for (int i = 0; i < MusicalNote.Notes.Length; i++)
            {
                Rect bt = new Rect(2, 2, 116, 18);
                bt.min += rect.min + new Vector2(0, i * 20);
                bt.max += rect.min + new Vector2(0, i * 20);
                EditorGUI.DrawRect(bt, new Color(0.7f, 0.7f, 0.7f));
                EditorGUI.LabelField(bt, MusicalNote.Notes[i]);

                if (Event.current.isMouse && Event.current.button == 0)
                {
                    Vector2 pos = Event.current.mousePosition;
                    if (bt.Contains(pos))
                    {
                        if (testClip)
                        {
                            DestroyImmediate(testClip);
                            testClip = null;
                        }

                        if (testSource && testSource.gameObject)
                            DestroyImmediate(testSource.gameObject);

                        if (Event.current.type == EventType.MouseDown)
                        {
                            SoundClip soundClip = new SoundClip(currentPart.Timbre, 4000, MusicalNote.NoteHz[i], MusicalNote.NoteHz[i], 0, 1.5f, true);
                            testClip = AudioClip.Create("test", soundClip.Data.Length, 1, 4000, false);
                            testClip.SetData(soundClip.Data, 0);

                            GameObject test = new GameObject();
                            test.name = "audio clip on shot";
                            testSource = test.AddComponent<AudioSource>();
                            testSource.clip = testClip;
                            testSource.Play();
                        }
                    }
                }
            }
        }

        void DrawRight(Rect screen, Rect size)
        {
            Rect rect = size;
            rect.min += screen.min;
            rect.max += screen.min;
            Rect rect2 = rect;

            float width = 400.0f * sizeA * scale;

            SelectPos(screen, size, rect2);

            rect.xMin += viewPos.x;
            rect.xMax += viewPos.x;
            EditorGUI.DrawRect(rect, new Color(0.5f, 0.5f, 0.5f));

            for (int i = 0; i < MusicalNote.Notes.Length; i++)
            {
                if (i % 2 == 0)
                {
                    Rect lineRect = new Rect(rect.xMin, rect.yMin + 20 * i, rect.width, 20);
                    EditorGUI.DrawRect(lineRect, new Color(0.7f, 0.7f, 0.7f));
                }

                for (float x = viewPos.x + rect.width - rect.x; x > -rect.x; x -= width)
                {
                    EditorGUI.DrawRect(new Rect(rect.width - x, rect.yMin + 20 * i, 1, 20), new Color(0, 0, 0));
                }

                for (float x = viewPos.x + rect.width - rect.x; x > -rect.x; x -= width / sizeA)
                {
                    EditorGUI.DrawRect(new Rect(rect.width - x + 1, rect.yMin + 20 * i, 1, 20), new Color(0, 0, 0));
                }
            }

            float timeLine = playTime * width / sizeA;
            EditorGUI.DrawRect(new Rect(-viewPos.x + rect.xMin + timeLine, rect.yMin, 1, rect.height), new Color(1, 0, 0));

            if (music.Time >= music.Duration)
            {
                music.Stop();
                playTime = 0;
            }

            MusicalNote[] musicalNotes = currentPart.Notes;
            foreach(var n in musicalNotes)
            {
                Rect noteRect = new Rect(n.StartTime * width / sizeA, n.Note * 20, n.KeepTime * width / sizeA, 20);
                noteRect.min += rect2.min;
                noteRect.max += rect2.min;

                if (rect.Overlaps(noteRect))
                {
                    EditorGUI.DrawRect(noteRect, new Color(0, 0.85f, 0, 0.5f));

                    Rect noteRectF = noteRect;
                    noteRectF.width = 2;
                    EditorGUI.DrawRect(noteRectF, new Color(0, 0, 0.85f, 1));
                }
            }

            MusicalNote[] selected = currentPart.Selected;
            foreach (var n in selected)
            {
                Rect noteRect = new Rect(n.StartTime * width / sizeA, n.Note * 20, n.KeepTime * width / sizeA, 20);
                noteRect.min += rect2.min;
                noteRect.max += rect2.min;

                Vector2 mov = new Vector2((selectEnd.x - selectStart.x) * width, (selectEnd.y - selectStart.y) * 20);
                noteRect.min += mov;
                noteRect.max += mov;

                if (rect.Overlaps(noteRect))
                {
                    EditorGUI.DrawRect(noteRect, new Color(0.9f, 0.85f, 0, 0.5f));
                }
            }


            if (Event.current.shift)
            {
                float time = selectStart.x * width;
                float timeW = (selectEnd.x - selectStart.x) * width;
                Rect multiSel = new Rect(rect2.xMin + time, rect2.yMin + 20 * selectStart.y, timeW, 20 * (selectEnd.y - selectStart.y));
                EditorGUI.DrawRect(multiSel, new Color(0, 0.85f, 0, 0.25f));
            }
            else
            {
                float time = selectStart.x * width;
                Rect sel = new Rect(rect2.xMin + time, rect2.yMin + 20 * selectStart.y, width, 20);
                EditorGUI.DrawRect(sel, new Color(0.85f, 0, 0, 0.5f));
            }

            CtrlNote();
        }

        void SelectPos(Rect screen, Rect size, Rect rect2)
        {
            float width = 400.0f * sizeA * scale;

            Rect area = rect2;
            area.min += viewPos;
            area.max += viewPos;

            if (Event.current.isMouse)
            {
                if (Event.current.button == 2)
                {
                    Vector2 mov = Event.current.delta;

                    viewPos.x -= mov.x;
                    viewPos.y -= mov.y;

                    viewPos.y = Mathf.Clamp(viewPos.y, 0.0f, Mathf.Max(20 * (MusicalNote.Notes.Length + 4) - (screen.height), 0.0f));
                    viewPos.x = Mathf.Clamp(viewPos.x, 0.0f, Mathf.Max(width * (music.Duration + 4.0f) / sizeA - size.width, 0.0f));
                }

                if (Event.current.button == 0 || Event.current.button == 1)
                {
                    if (area.Contains(Event.current.mousePosition))
                    {
                        if (Event.current.type == EventType.MouseDown)
                        {
                            Vector2 selPos = -rect2.min + Event.current.mousePosition;

                            selectStart.x = (int)(selPos.x / width);
                            selectStart.y = (int)(selPos.y / 20);

                            selectEnd = selectStart;

                        }
                        else if (Event.current.type == EventType.MouseUp)
                        {
                        }
                        else if (Event.current.type == EventType.MouseDrag)
                        {
                            Vector2 selPos = -rect2.min + Event.current.mousePosition;

                            selectEnd.x = (int)(selPos.x / width);
                            selectEnd.y = (int)(selPos.y / 20);
                        }
                    }
                    else
                    {
                        selectStart.x = -1;
                        selectStart.y = -1;
                        selectEnd.x = -1;
                        selectEnd.y = -1;
                    }
                }
            }
            if (Event.current.isScrollWheel)
            {
                scale -= Event.current.delta.y / 20.0f;
                scale = Mathf.Clamp(scale, 0.1f, 2.0f);
            }

            if (music.IsPlaying)
            {
                viewPos.x = Mathf.Max(playTime * width / sizeA - size.width / 2.0f, 0.0f);
            }
        }

        void CtrlNote()
        {
            if (Event.current.type == EventType.KeyUp)
            {
                if(Event.current.keyCode == KeyCode.Delete)
                {
                    currentPart.RemoveSelected();
                    currentPart.CleanSelected();
                }
            }

            if (music.IsPlaying)
            {
                selectStart.x = selectStart.y = -1;
                selectEnd = selectStart;
                return;
            }

            if (selectStart.x >= 0 && selectStart.y >= 0 && selectStart.y < MusicalNote.Notes.Length)
            {
                if (currentPart.Selected.Length == 0)
                {
                    if (Event.current.type == EventType.MouseUp)
                    {
                        if (Event.current.shift)
                        {
                            if (Event.current.button == 0)
                            {
                                currentPart.Select(selectStart.y, selectEnd.y, selectStart.x * sizeA, selectEnd.x * sizeA);
                            }
                        }
                        else if (Event.current.control)
                        {
                            if (Event.current.button == 0)
                            {
                                playTime = selectStart.x * sizeA;
                            }
                        }
                        else
                        {
                            if (Event.current.button == 0)
                            {
                                if (testClip)
                                {
                                    DestroyImmediate(testClip);
                                    testClip = null;
                                }

                                if(testSource && testSource.gameObject)
                                    DestroyImmediate(testSource.gameObject);

                                currentPart.AddNote(selectStart.y, selectStart.x * sizeA, (selectEnd.x - selectStart.x + 1) * sizeA);

                                SoundClip soundClip = new SoundClip(currentPart.Timbre, 4000, MusicalNote.NoteHz[selectStart.y], MusicalNote.NoteHz[selectStart.y], 0, 0.5f, true);
                                testClip = AudioClip.Create("test", soundClip.Data.Length, 1, 4000, false);
                                testClip.SetData(soundClip.Data, 0);

                                GameObject test = new GameObject();
                                test.name = "audio clip on shot";
                                testSource = test.AddComponent<AudioSource>();
                                testSource.clip = testClip;
                                testSource.Play();
                            }
                            else if (Event.current.button == 1)
                            {
                                currentPart.Select(selectStart.y, selectStart.x * sizeA);
                                currentPart.RemoveSelected();
                                currentPart.CleanSelected();
                            }

                            music.UpdateParts();
                        }


                        selectStart.x = selectStart.y = -1;
                        selectEnd = selectStart;
                    }
                }
                else
                {
                    if (Event.current.type == EventType.MouseUp)
                    {
                        if(selectEnd == selectStart)
                        {
                            currentPart.CleanSelected();
                        }
                        else
                        {
                            currentPart.MoveSelected(selectStart.x * sizeA, selectStart.y, selectEnd.x * sizeA, selectEnd.y);
                            selectStart.x = selectStart.y = -1;
                            selectEnd = selectStart;

                            music.UpdateParts();
                        }
                    }
                }
            }
        }

        void Copy()
        {
            copyList.Clear();
            foreach (var n in currentPart.Selected)
            {
                copyList.Add(n.Clone());
            }

            float start = float.MaxValue;
            foreach(var n in copyList)
            {
                start = Mathf.Min(start, n.StartTime);
            }

            foreach (var n in copyList)
            {
                n.StartTime -= start;
            }
        }

        void Paste()
        {

            float len = currentPart.Length;
            foreach (var n in copyList)
            {
                currentPart.AddNote(n.Note, n.StartTime + len, n.KeepTime);
            }

            music.UpdateParts();
        }
    }
}