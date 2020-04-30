using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dialogue : MonoBehaviour
{
    public TextAsset file;
    List<string> stringList = new List<string>();
    private void Start()
    {
        DailyPlayer.movable = false;
        string[] fileData = file.ToString().Split('\n');
        stringList.Clear();
        for (int i = 0; i < fileData.Length; i++)
        {
            stringList.Add(fileData[i]);

        }
        transform.Find("Name").GetComponent<Text>().text = stringList[0];
        stringList.RemoveAt(0);
        transform.Find("Content").GetComponent<Text>().text = stringList[0];
        stringList.RemoveAt(0);
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (stringList.Count > 0)
            {
                transform.Find("Name").GetComponent<Text>().text = stringList[0];
                stringList.RemoveAt(0);
                transform.Find("Content").GetComponent<Text>().text = stringList[0];
                stringList.RemoveAt(0);
            }
            else
            {
                DailyPlayer.movable = true;
                Destroy(gameObject);
            }
        }
        //if (Input.GetMouseButtonDown(0)&&stringList.Count == 0)
        //    Destroy(gameObject);
    }
}
