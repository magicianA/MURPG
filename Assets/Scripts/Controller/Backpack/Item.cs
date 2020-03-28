using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Item : ScriptableObject
{
    private GameObject canvas = GameObject.FindWithTag("Canvas");
    public Sprite logo;
    public string description;
    public GameObject descriptionLabel = (GameObject)Resources.Load("Daily/DescriptionLabel");
    public abstract void Func();
    public virtual void OnClick()
    {
        
        ShowDescription();

    }
    private void ShowDescription()
    {
        GameObject label = Instantiate(descriptionLabel);
        label.GetComponentInChildren<Text>().text = description;
 
        label.transform.SetParent(canvas.transform) ;
        label.transform.localScale = Vector3.one;
        label.transform.position = new Vector3(0, 0, 0);
        if (description != null)
            Debug.Log(description);
    }
}
