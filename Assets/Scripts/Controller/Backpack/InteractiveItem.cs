using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractiveItem : Item
{
    public InteractiveItem(string path,string description)
    {
        logo = LoadSprite(path);
        this.description = description;
    }
    public override void Func()
    {
        
    }
    private Sprite LoadSprite(string path)
    {
        Object Preb = Resources.Load(path, typeof(Sprite));
        Sprite tmpsprite = null;
        try
        {
            tmpsprite = Instantiate(Preb) as Sprite;
        }
        catch (System.Exception ex)
        {
            Debug.Log("加载图片错误" + ex.ToString());
        }
        return tmpsprite;
    }
}
