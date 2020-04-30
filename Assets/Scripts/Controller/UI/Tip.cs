using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tip : MonoBehaviour
{
    public string str;
    private void Start()
    {
        gameObject.GetComponentInChildren<Text>().text = str;
        DailyPlayer.movable = false;
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            DailyPlayer.movable = true;
            Destroy(gameObject);
        }

    }
}
