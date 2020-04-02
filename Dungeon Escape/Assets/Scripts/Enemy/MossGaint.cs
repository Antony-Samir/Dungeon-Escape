using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MossGaint : Enemy, IDamageable
{
    [SerializeField]
    private AudioClip deathClip;
    [SerializeField]
    private AudioClip hitClip;


    public int Health { get; set; }

    public override void Init()
    {
        base.Init();
        Health = base.health;
    }
    public override void Movement()
    {
        base.Movement();
    }



    public void Damage()
    {
        if (isDead)
        {
            return;
        }
        Debug.Log("MossGaint::Damage()");
        anim.SetTrigger("Hit");
        isHit = true;
        anim.SetBool("InCombat", true);



        Health--;
        AudioSource.PlayClipAtPoint(hitClip, this.transform.position, 1f);
        if (Health < 1)
        {
            AudioSource.PlayClipAtPoint(deathClip, this.transform.position, 1f);
            isDead = true;
            anim.SetTrigger("Death");
            GameObject diamond = Instantiate(diamondPrefab, transform.position, Quaternion.identity) as GameObject; //casting
            diamond.GetComponent<Diamond>().gems = base.gems;
            //Destroy(this.gameObject);
        }
    }






}
