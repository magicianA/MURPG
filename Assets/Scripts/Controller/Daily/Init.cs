using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Init : MonoBehaviour
{
    public Color HintColor;
    public GameObject obj;
    private void Start()
    {
        GameObject.FindWithTag("MainCamera").GetComponent<SmartCamera>().enabled = false;
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            SceneManager.LoadScene("01");
            GameObject.FindWithTag("MainCamera").GetComponent<SmartCamera>().enabled = true;
            Destroy(obj);
            Destroy(gameObject);
        }
        HintColor.a = Mathf.PingPong(1 * Time.time, 1F);//5*Time.time是闪烁频率，大家可以自己改，1F就是颜色的a的最大的值，意思就是从完全透明到完全不透明
        GetComponent<Image>().color = HintColor;//获取UI的image组件的颜色并把上面变化的hintcolor赋值给他
        
    }
}
