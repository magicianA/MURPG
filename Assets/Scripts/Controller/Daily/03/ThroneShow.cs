using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThroneShow : MonoBehaviour
{
    public List<GameObject> objects = new List<GameObject>();
    private GameObject slot;
    Backpack backpack;
    public GameObject throne;
    private void Awake()
    {
        slot = transform.Find("Slot").gameObject;
        backpack = GameObject.FindWithTag("Backpack").GetComponent<Backpack>();
    }
    
    private void OnEnable()
    {
        backpack.AddSolt(slot, true);
        UpdateItems();
    }
    private void OnDisable()
    {
        backpack.RemoveSolt(slot);
    }
    private void Update()
    {
        PutItem();
        if (DailyPlayer.movable == true)
            DailyPlayer.movable = false;
    }
    public void UpdateItems()
    {
        for(int i = 0; i < objects.Count; i++)
        {
            if (ThroneData.throneItemList.Contains(objects[i].name))
                objects[i].SetActive(true);
            else
                objects[i].SetActive(false);
        }
    }
    private void PutItem()
    {
        if (backpack.soltable[slot] == false)
        {
            foreach(string key in backpack.itemsGameObjects.Keys)
            {
                if (GetFlatDis(backpack.itemsGameObjects[key].transform.position, slot.transform.position) < 0.01)
                {
                    ThroneData.throneItemList.Add(key);
                    UpdateItems();
                    backpack.soltable.Remove(backpack.itemsGameObjects[key]);
                    Destroy(backpack.itemsGameObjects[key]);
                    backpack.itemsGameObjects.Remove(key);
                    if (ThroneData.story.ContainsKey(key))
                    {
                        UIManager.TextTip("故事");
                        ThroneData.story.Remove(key);
                    }
                    return;
                }
            }
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
    public void OnClick(string name)
    {
        InteractiveItem item = new InteractiveItem("Daily/道具/" + name, name);
        backpack.AddItem(item);
        ThroneData.throneItemList.Remove(name);
        UpdateItems();
    }
}
