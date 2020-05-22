using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Statue : MonoBehaviour
{
    InteractiveItem jue;
    Backpack backpack;
    public TextAsset text;
    public GameObject blood;
    public GameObject slot;
    private void Start()
    {
        
        backpack = GameObject.FindWithTag("Backpack").GetComponent<Backpack>();
        jue = new InteractiveItem("Daily/道具/金刚橛", "橛");
    }
    private void Update()
    {
        if (backpack.itemsGameObjects.ContainsKey("橛") && GetFlatDis(backpack.itemsGameObjects["橛"].transform.position, slot.transform.position) < 0.01f)
        {
            blood.SetActive(true);
            Destroy(slot);
            Destroy(backpack.itemsGameObjects["橛"]);
            backpack.itemsGameObjects.Remove("橛");   
            gameObject.SetActive(false);
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (MyInput.isButtonDown)
        {
            UIManager.Dialogue(text);
            backpack.AddItem(jue);
            slot.SetActive(true);
        }
    }
    public float GetFlatDis(Vector3 v1, Vector3 v2)
    {

        return Mathf.Pow(v1.x - v2.x, 2) + Mathf.Pow(v1.y - v2.y, 2);
    }
}
