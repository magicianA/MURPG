using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static GameObject canvas;
    public static GameObject dialoguePrefab;
    public static GameObject tipPrefab;
    private void Awake()
    {
        canvas = GameObject.FindWithTag("Canvas");
        dialoguePrefab = (GameObject)Resources.Load("Daily/DialoguePanel");
        tipPrefab = (GameObject)Resources.Load("Daily/TipPrefab");
    }
    public static void Dialogue(TextAsset file)
    {
        GameObject panel = Instantiate(dialoguePrefab);
        panel.transform.SetParent(canvas.transform);
        panel.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
        panel.transform.localPosition = new Vector3(0, -300, 0);
        panel.GetComponent<Dialogue>().file = file;

    }
    public static void TextTips(TextAsset file)
    {

    }
    public static void TextTip(string tip)
    {
        GameObject panel = Instantiate(tipPrefab);
        panel.transform.SetParent(canvas.transform);
        panel.transform.localScale = new Vector3(1, 1, 1);
        panel.transform.localPosition = new Vector3(0, 0, 0);
        panel.GetComponent<Tip>().str = tip;
    }
}
