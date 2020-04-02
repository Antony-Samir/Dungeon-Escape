using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{

    private Animator _anim;
    private Animator _swordAnimation;
    Shop shop;

    void Start()
    {
        shop = GetComponent<Shop>();
        _anim = GetComponentInChildren<Animator>();
        _swordAnimation = transform.GetChild(1).GetComponentInChildren<Animator>();

    }


    public void Move(float move)
    {
        _anim.SetFloat("Move", Mathf.Abs(move));
    }

    public void Jump(bool jumping)
    {
        _anim.SetBool("Jumping", jumping);
    }
    
    public void Attack()
    {
        if (GameManager.Instance.player.fireAttack == true)
        {
            _anim.SetTrigger("FireAttack");
        }
        else
        {
            _anim.SetTrigger("Attack");
            _swordAnimation.SetTrigger("SwordAnimation");
        }
    }

    public void Death()
    {
        _anim.SetTrigger("Death");
    }
}
