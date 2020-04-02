using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spider : Enemy, IDamageable
{
    [SerializeField]
    private AudioClip attackClip;
    [SerializeField]
    private AudioClip deathClip;


    public GameObject acidEffectPrefab;
    public int Health { get; set; }
    public override void Init()
    {
        base.Init();
        Health = base.health;

    }
    public override void Update()
    {
    }


    public void Damage()
    {
        if (isDead)
        {
            return;
        }
        Health--;

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
    public override void Movement()
    {
        //move
    }

    public void Attack()
    {
        AudioSource.PlayClipAtPoint(attackClip, this.transform.position, 0.1f);
        Instantiate(acidEffectPrefab, transform.position, Quaternion.identity);
    }
}
