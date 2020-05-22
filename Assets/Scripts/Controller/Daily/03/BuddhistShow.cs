using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuddhistShow : MonoBehaviour
{
    public GameObject[] slots;
    Backpack backpack;
    private void Awake()
    {
        backpack = GameObject.FindWithTag("Backpack").GetComponent<Backpack>();
    }
    private void OnEnable()
    {
        for(int i = 0; i < slots.Length; i++)
        {
            backpack.AddSolt(slots[i],true);
        }

    }
    private void Update()
    {
        if (DailyPlayer.movable == true)
            DailyPlayer.movable = false;
        CheckSuccess();
        
    }
    private void OnDisable()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            foreach(GameObject value in backpack.itemsGameObjects.Values)
            {
                if (GetFlatDis(value.transform.position, slots[i].transform.position) < 0.01f)
                    backpack.ResetPosition(value);
            }
            backpack.RemoveSolt(slots[i]);
        }
    }
    public float GetFlatDis(Vector3 v1, Vector3 v2)
    {

        return Mathf.Pow(v1.x - v2.x, 2) + Mathf.Pow(v1.y - v2.y, 2);
    }
    public void OnClose()
    {
        gameObject.SetActive(false);
        DailyPlayer.movable = true;
    }
    public void CheckSuccess()
    {
        if(backpack.itemsGameObjects.ContainsKey("阿")&& backpack.itemsGameObjects.ContainsKey("洋") &&
            backpack.itemsGameObjects.ContainsKey("哇") && backpack.itemsGameObjects.ContainsKey("哈") &&
            GetFlatDis(backpack.itemsGameObjects["阿"].transform.position, slots[0].transform.position) < 0.01f&&
            GetFlatDis(backpack.itemsGameObjects["洋"].transform.position, slots[1].transform.position) < 0.01f&&
            GetFlatDis(backpack.itemsGameObjects["哇"].transform.position, slots[2].transform.position) < 0.01f&&
            GetFlatDis(backpack.itemsGameObjects["哈"].transform.position, slots[3].transform.position) < 0.01f)
        {
            if (backpack.itemsGameObjects.ContainsKey("恰") == false)
            {
                InteractiveItem item = new InteractiveItem("Daily/道具/恰", "恰");
                backpack.AddItem(item);
            }
        }
    }
}
