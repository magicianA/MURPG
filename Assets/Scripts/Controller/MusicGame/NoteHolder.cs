using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleJSON;

public class NoteHolder : MonoBehaviour
{

    float curtime = 0f;
    JSONNode node;
    public List<Note> notes = new List<Note>();
    private Queue<Note> pendingnotes = new Queue<Note>();
    public float bpm;
    public GameObject clicknoteprefab,holdnoteprefab,flicknoteprefab;
    public int notecount;
    void Start()
    {
        string jsonTest = ((TextAsset)Resources.Load("Music/test")).text;
        node = JSON.Parse(jsonTest);
        parse();
    }

    void Update()
    {
        curtime += Time.deltaTime;
        while(pendingnotes.Count != 0 && pendingnotes.Peek().time <= curtime){
            Note t = pendingnotes.Peek();
            switch(t.type){
                case NoteStyleType.Click:
                    t._gameobject = t.ToInstance(clicknoteprefab,new Vector3(20 * t.pos,0,400f),gameObject);
                    break;
                case NoteStyleType.Hold:
                    t._gameobject = t.ToInstance(holdnoteprefab,new Vector3(20 * t.pos,0,400f),gameObject);
                    break;
                case NoteStyleType.Flick:
                    t._gameobject = t.ToInstance(flicknoteprefab,new Vector3(20 * t.pos,0,400f),gameObject);
                    break;
            }
            pendingnotes.Dequeue();
        }
    }
    void parse(){
        bpm = node["bpm"].AsFloat;
        notecount = node["notecount"].AsInt;
        for(int i = 0;i < notecount;i++){
            int id = node["notes"][i]["id"].AsInt;
            float pos = node["notes"][i]["pos"].AsFloat;
            float time = node["notes"][i]["time"].AsFloat;
            float size = node["notes"][i]["size"].AsFloat;
            //NoteStyleType styleType = NoteStyleType.Click;
            NoteStyleType styleType = (NoteStyleType) System.Enum.Parse(typeof(NoteStyleType),node["notes"][i]["type"].Value,true);
            notes.Add(new Note(id,pos,time,size,styleType));
            pendingnotes.Enqueue(notes[notes.Count - 1]);
        }
    }
}
