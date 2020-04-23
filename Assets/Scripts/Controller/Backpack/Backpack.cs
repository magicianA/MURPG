using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Backpack : MonoBehaviour
{
    //private List<Item> itemList;
    public Dictionary<string, GameObject> itemsGameObjects = new Dictionary<string, GameObject>();
    private GameObject itemPrefab;
    public Dictionary<GameObject, bool> soltable = new Dictionary<GameObject, bool>();
    public List<GameObject> solt;
    private void Awake()
    {
        solt.Clear();
        soltable.Clear();
        itemsGameObjects.Clear();
        itemPrefab = (GameObject)Resources.Load("Daily/Item");
        Init();
    }
    private void Update()
    {
        
    }
    private void Init()
    {
        GameObject[] gameObjects = GameObject.FindGameObjectsWithTag("Slot");
        for (int i = 0; i < gameObjects.Length; i++)
        {
            solt.Add(gameObjects[i]);
            soltable.Add(gameObjects[i], true);
        }
    }
    public void AddSolt(GameObject newSolt,bool able)
    {
        solt.Add(newSolt);
        soltable.Add(newSolt, able);
    }
    public void RemoveSolt(GameObject newSolt)
    {
        solt.Remove(newSolt);
        soltable.Remove(newSolt);
    }
    private void UpdateSolt()
    {
        GameObject[] gameObjects = GameObject.FindGameObjectsWithTag("Slot");
    }
    private void Show()
    {

    }
    public void AddItem(Item item)
    {
        itemsGameObjects.Add(item.name, CreateItem(item));
        Debug.Log("成功");
    }
    

    public GameObject CreateItem(Item item)
    {
        GameObject gameObj = (GameObject)Instantiate(itemPrefab);
        gameObj.transform.SetParent(gameObject.transform);
        gameObj.transform.localScale = new Vector3(1, 1, 1);
        gameObj.GetComponent<Image>().sprite = item.logo;
        int index = ReturnTrueIndex();
        if (index == -1)
        {
            Debug.Log("背包已满");
            return null;
        }
        gameObj.transform.position = solt[index].transform.position;
        soltable[solt[index]] = false;
        //gameObj.GetComponent<Button>().onClick.AddListener(item.OnClick);
        return gameObj;
    }
    private int ReturnTrueIndex()
    {
        for(int i = 0; i < solt.Count; i++)
        {
            if (soltable[solt[i]] == true)
                return i;
        }

        return -1;
    }
}
