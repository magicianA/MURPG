using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turntable : MonoBehaviour
{
    GameObject inner;
    GameObject outer;
    bool clockwise = false;
    bool counterClockwise = false;
    int counter = 0;
    public List<GameObject> innerObj = new List<GameObject>();
    public List<GameObject> outerObj = new List<GameObject>();
    public GameObject ri;
    public GameObject yue;
    public GameObject huo;
    private Vector3[] outerPos=new Vector3[3];
    public GameObject wa;
    Backpack backpack;
    private void Update()
    {
        if (clockwise == true)
        {
            if (counter < 52)
            {
                inner.transform.Rotate(new Vector3(0, 0, -0.989f));
                counter++;
            }
            else
            {
                counter = 0;
                clockwise = false;
            }
        }
        else if (counterClockwise == true)
        {
            if (counter < 52)
            {
                inner.transform.Rotate(new Vector3(0, 0, 0.989f));
                counter++;
            }
            else
            {
                counter = 0;
                counterClockwise = false;
            }
        }
    }
    private void Start()
    {
        inner = transform.Find("Inner").gameObject;
        outer = transform.Find("Outer").gameObject;
        for(int i = 0; i < outerPos.Length; i++)
        {
            outerPos[i] = outerObj[i].transform.position;
        }
        backpack = GameObject.FindWithTag("Backpack").GetComponent<Backpack>();
    }
    private void Exchange()
    {
        List<GameObject> newInner = new List<GameObject>();
        List<GameObject> newOuter = new List<GameObject>();
        for(int i = 0; i < outerObj.Count; i++)
        {
            int index = GetNearIndex(outerObj[i], innerObj);
            ChangePos(outerObj[i], innerObj[index]);
            outerObj[i].transform.SetParent(inner.transform);
            innerObj[index].transform.SetParent(outer.transform);
            newInner.Add(outerObj[i]);
            newOuter.Add(innerObj[index]);
        }
        for(int i = 0; i < innerObj.Count; i++)
        {
            if (!newInner.Contains(innerObj[i]) && !newOuter.Contains(innerObj[i]))
                newInner.Add(innerObj[i]);
        }
        innerObj = newInner;
        outerObj = newOuter;
        CheckWin();
    }
    private int GetNearIndex(GameObject Obj,List<GameObject> list)
    {
        float distance = GetFlatDis(Obj.transform.position,list[0].transform.position);
        int index = 0;
        for(int i = 0; i < list.Count; i++)
        {
            if(distance>GetFlatDis(Obj.transform.position, list[i].transform.position))
            {
                distance = GetFlatDis(Obj.transform.position, list[i].transform.position);
                index = i;
            }
        }
        return index;
    }
    private void ChangePos(GameObject g1,GameObject g2)
    {
        Vector3 vector = g1.transform.position;
        g1.transform.position = g2.transform.position;
        g2.transform.position = vector;
    }
    private float GetFlatDis(Vector3 v1, Vector3 v2)
    {

        return Mathf.Pow(v1.x - v2.x, 2) + Mathf.Pow(v1.y - v2.y, 2);
    }
    public void GetMessage(string message)
    {
        if (message=="wise" && counterClockwise == false)
            clockwise = true;
        if (message=="counter" && clockwise == false)
            counterClockwise = true;
        if (message=="exchange" && counterClockwise == false && clockwise == false)
            Exchange();
    }
    public void CheckWin()
    {
        if (GetFlatDis(ri.transform.position, outerPos[0]) < 0.01f &&
            GetFlatDis(yue.transform.position, outerPos[2]) < 0.01f &&
            GetFlatDis(huo.transform.position, outerPos[1]) < 0.01f &&
            !backpack.ContainsItem("哇"))
        {
            wa.SetActive(true);
            DailyPlayer.camera.GetComponent<Camera>().orthographicSize = 5f;
            DailyPlayer.camera.GetComponent<SmartCamera>().enabled = true;
        }
    }
}
