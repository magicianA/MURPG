using System.Collections.Generic;
using UnityEngine;

public class ThroneData : MonoBehaviour
{
    public static Dictionary<string, TextAsset> story = new Dictionary<string, TextAsset>();
    public static List<string> throneItemList = new List<string>();
    public static List<string> buddhistItemList = new List<string>();
    private void Start()
    {
        story.Add("玛", new TextAsset());
        story.Add("哇", new TextAsset());
        story.Add("哈", new TextAsset());
        story.Add("洋", new TextAsset());
        story.Add("恰", new TextAsset());
        story.Add("拉", new TextAsset());
        story.Add("阿", new TextAsset());
    }
}
