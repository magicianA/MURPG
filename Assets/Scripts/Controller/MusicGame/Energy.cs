using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Energy : MonoBehaviour
{
    public float energy = 0;
    private static Energy _instance;
    void Awake(){
        _instance = this;
    }
    public static void addenergy(float x){
        Debug.Log(123);
        _instance.energy += x;
    }
    public static void setenergy(float x){
        _instance.energy = x;
    }
}
