using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalNoteAct : MonoBehaviour
{
    bool pass = false;
    void Update()
    {
        if(Mathf.Abs(transform.position.z) <= 1 && (!pass)){
            line.CheckEnergy(transform.position.x);
            pass = true;
        }        
    }
}
