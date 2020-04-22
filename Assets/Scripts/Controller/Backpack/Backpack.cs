using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Backpack : MonoBehaviour
{
    //private List<Item> itemList;
    public Dictionary<string, GameObject> itemsGameObjects = new Dictionary<string, GameObject>();
    private GameObject itemPrefab;
    public bool[] solt = new bool[35];
    public Vector3[] soltPos = new Vector3[35];
    private void Awake()
    {
        Init();
    }
    private void Update()
    {
        GameObject[] gameObjects = GameObject.FindGameObjectsWithTag("Slot");
        for (int i = 0; i < solt.Length; i++)
        {
            soltPos[i] = gameObjects[i].transform.position;
        }
    }
    private void Init()
    {
        for (int i = 0; i < solt.Length; i++)
        {
            solt[i] = true;
        }

        itemsGameObjects.Clear();
        itemPrefab = (GameObject)Resources.Load("Daily/Item");
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
        gameObj.transform.position = soltPos[index];
        solt[index] = false;
        //gameObj.GetComponent<Button>().onClick.AddListener(item.OnClick);
        return gameObj;
    }
    private int ReturnTrueIndex()
    {
        for(int i = 0; i < solt.Length; i++)
        {
            if (solt[i] == true)
                return i;
        }

        return -1;
    }
}
