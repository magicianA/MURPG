using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree : MonoBehaviour
{
    public TextAsset text;
    private GameObject slot;
    public GameObject hang01;
    public GameObject hang02;
    Backpack backpack;

    private void Start()
    {
        slot = transform.Find("Slot").gameObject;
        slot.SetActive(false);
        backpack = GameObject.FindWithTag("Backpack").GetComponent<Backpack>();
        hang02.SetActive(false);
    }
    private void Update()
    {
        if (GetFlatDis(backpack.itemsGameObjects["刀"].transform.position, slot.transform.position) < 0.01f)
        {
            hang01.SetActive(false);
            hang02.SetActive(true);
            Destroy(slot);
            Destroy(backpack.itemsGameObjects["刀"]);
            backpack.itemsGameObjects.Remove("刀");
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (MyInput.isButtonDown)
        {
            UIManager.Dialogue(text);
            slot.SetActive(true);
            backpack.AddSolt(slot, true);
        }

    }
    public float GetFlatDis(Vector3 v1, Vector3 v2)
    {

        return Mathf.Pow(v1.x - v2.x, 2) + Mathf.Pow(v1.y - v2.y, 2);
    }

}
