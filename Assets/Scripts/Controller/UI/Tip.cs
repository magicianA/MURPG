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

    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
            Destroy(gameObject);
    }
}
