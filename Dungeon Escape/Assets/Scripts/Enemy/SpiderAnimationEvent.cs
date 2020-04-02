using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderAnimationEvent : MonoBehaviour
{
  
    private Spider _spider;
    public void Start()
    {
        _spider = transform.parent.GetComponent<Spider>();
    }

    public void Fire()
    {
        //Debug.Log("Spider should fire");
        _spider.Attack();
    }


}
