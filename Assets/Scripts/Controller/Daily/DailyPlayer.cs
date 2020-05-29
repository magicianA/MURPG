using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DailyPlayer : MonoBehaviour
{
    public static bool movable=true;
    public static GameObject throne;
    public static GameObject buddhist;
    public static GameObject map;
    public static GameObject camera;
    public float speed = 5f;
    private Animator anim;
    public static bool isBuddhist = false;
    private void Start()
    {
        throne = GameObject.FindWithTag("Throne");
        throne.SetActive(false);
        buddhist = GameObject.FindWithTag("Buddhist");
        buddhist.SetActive(false);
        map = GameObject.FindWithTag("Map");
        map.SetActive(false);
        camera = GameObject.FindWithTag("MainCamera");
        anim = gameObject.GetComponent<Animator>();
    }
    private void LateUpdate()
    {
        AnimatorStateInfo animatorInfo;
        animatorInfo = anim.GetCurrentAnimatorStateInfo(0);
        if ((animatorInfo.normalizedTime > 1.0f) && (animatorInfo.IsName("static01")))//normalizedTime：0-1在播放、0开始、1结束 MyPlay为状态机动画的名字  
        {
            anim.SetBool("goleft", false);
            anim.SetBool("goright", false);
            anim.SetBool("static01", false);
            anim.SetBool("static02", true);
        }
        if (movable)
        {
            if (MyInput.w)
                Move(Vector2.up);
            else if (MyInput.a)
                Move(Vector2.left);
            else if (MyInput.s)
                Move(Vector2.down);
            else if (MyInput.d)
                Move(Vector2.right);
            else
                Move(new Vector2(0, 0.0001f));
        }


    }
    private void Move(Vector2 target)
    {
        gameObject.GetComponent<Rigidbody2D>().velocity = target * speed;
        if (target.x < 0||target.y<-0.01f)
        {
            anim.SetBool("goleft", true);
            anim.SetBool("goright", false);
            anim.SetBool("static01", false);
            anim.SetBool("static02", false);
        }
        else if (target.x > 0||target.y > 0.01f)
        {
            anim.SetBool("goleft", false);
            anim.SetBool("goright", true);
            anim.SetBool("static01", false);
            anim.SetBool("static02", false);
        }
        else if (target.sqrMagnitude < 0.001f)
        {
            anim.SetBool("goleft", false);
            anim.SetBool("goright", false);
            anim.SetBool("static01", true);
            anim.SetBool("static02", false);
        }

    }
}
