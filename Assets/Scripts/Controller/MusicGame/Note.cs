using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Note
{
    public int id;
    public float pos;
    public float time;
    public float size;
    public int flickdir;
    public NoteResultType result;
    public NoteStyleType type;
    public GameObject _gameobject;


    public Note(int id,float pos,float time,float size,NoteStyleType type,int flickdir,NoteResultType resultType = NoteResultType.Unknown){
        this.id = id;
        this.pos = pos;
        this.time = time;
        this.size = size;
        this.result = resultType;
        this.type = type;
        this.flickdir = flickdir;
    }
    public GameObject ToInstance(GameObject prefab,Vector3 pos,GameObject parent){
        GameObject t = GameObject.Instantiate(prefab,pos,Quaternion.identity);
        t.transform.parent = parent.transform;
        _gameobject = t;
        return t;
    }
}


