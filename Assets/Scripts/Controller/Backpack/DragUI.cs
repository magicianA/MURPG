using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragUI : MonoBehaviour, IDragHandler, IPointerDownHandler, IEndDragHandler
{
    Vector3 originPos;
    Backpack backpack;
    private void Start()
    {
        backpack = GetComponentInParent<Backpack>();
    }
    public void OnDrag(PointerEventData eventData)
    {
        Vector3 pos = new Vector3(eventData.position.x, eventData.position.y, 0f);
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(pos);
        worldPos = new Vector3(worldPos.x, worldPos.y, 0f);
        transform.position = worldPos;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        originPos = transform.position;
        backpack.soltable[backpack.solt[GetNearPosIndex()]] = true;
    }
    private int GetNearPosIndex()
    {
        int index=0;
        float distance = GetFlatDis(transform.position, backpack.solt[0].transform.position);
        for(int i = 0; i < backpack.solt.Count; i++)
        {
            
            if (distance > GetFlatDis(transform.position, backpack.solt[i].transform.position))
            {
                distance = GetFlatDis(transform.position, backpack.solt[i].transform.position);
                index = i;
            }
        }
        Debug.Log(index);
        return index;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        int index = GetNearPosIndex();
        if (backpack.soltable[backpack.solt[index]] == true)
        {
            transform.position = backpack.solt[index].transform.position;
            backpack.soltable[backpack.solt[index]] = false;
        }
        else
            transform.position = originPos;

    }
    public float GetFlatDis(Vector3 v1,Vector3 v2)
    {
        
        return Mathf.Pow(v1.x - v2.x,2) + Mathf.Pow(v1.y - v2.y,2);
    }
    //private bool OutOfRange()
    //{
    //    if (backpack.gameObject.GetComponent<Mask>().IsActive() == false)
    //    {

    //    }
    //    else
    //    {

    //    }

    //}
}
