using System.Collections;
using System.Collections.Generic;
using DragonBones;
using UnityEngine;

public class Boss : MonoBehaviour
{
    private UnityArmatureComponent unityArmature;
    void Start()
    {
        unityArmature = GetComponent<UnityArmatureComponent>();
        unityArmature.animation.Play("抬手");
    }

    void Update()
    {
        
    }
}
