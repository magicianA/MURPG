using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GUIhp : MonoBehaviour
{
    public static GUIhp _instance;
    public RectTransform hp1,hp2;
    public float hp = 1;
    public float width,height;
    void Awake(){
        _instance = this;
        width = GetComponent<RectTransform>().rect.width;
        height = GetComponent<RectTransform>().rect.height;
    }

    
    public static void UpdateHP(float x){
        _instance.hp += x;
        if(_instance.hp < 0) _instance.hp = 0;
        _instance.hp1.sizeDelta =  new Vector2(_instance.hp * _instance.width, _instance.height);
        _instance.hp2.sizeDelta = new Vector2(_instance.hp * _instance.width,_instance.height);
    }
}
