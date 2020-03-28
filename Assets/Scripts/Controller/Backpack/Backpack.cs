using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Backpack : MonoBehaviour
{

    static Backpack instance;

    public static Backpack Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new Backpack();
            }
            return instance;
        }
    }

    //private List<Item> itemList;
    private Dictionary<Item, GameObject> itemsGameObjects = new Dictionary<Item, GameObject>();
    private GameObject itemPrefab;
    public bool[] solt = new bool[35];
    public Vector3[] soltPos = new Vector3[35];
    private void Awake()
    {
        Init();
    }
    private void Init()
    {
        for (int i = 0; i < solt.Length; i++)
        {
            solt[i] = true;
        }
        GameObject[] gameObjects = GameObject.FindGameObjectsWithTag("Slot");
        for(int i = 0; i < solt.Length; i++)
        {
            soltPos[i] = gameObjects[i].transform.position;
        }
        itemsGameObjects.Clear();
        itemPrefab = (GameObject)Resources.Load("Daily/item");
    }
    private void Show()
    {

    }
    public void AddItem(Item item)
    {
        itemsGameObjects.Add(item, CreateItem(item));
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
        gameObj.GetComponent<Button>().onClick.AddListener(item.OnClick);
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
