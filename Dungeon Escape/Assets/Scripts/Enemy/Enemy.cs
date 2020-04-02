using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    public GameObject diamondPrefab;

    [SerializeField]
    private AudioClip idleClip;
    [SerializeField]
    protected int health;
    [SerializeField]
    protected float speed;
    [SerializeField]
    protected int gems;
    [SerializeField]
    protected Transform pointA, pointB;

    protected Vector3 currentTarget;
    protected Animator anim;
    protected SpriteRenderer sprite;

    protected bool isHit = false;
    protected Player player;
    protected bool isDead = false;


    void Start()
    {
        Init();
    }

    public virtual void Init()
    {
        anim = GetComponentInChildren<Animator>();
        sprite = GetComponentInChildren<SpriteRenderer>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();

    }

    public virtual void Update()
    {
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Idle") && anim.GetBool("InCombat") == false)
        {
            return;
        }
        if (isDead == false)
        {
            
            Movement();
        }
        
    }





    public virtual void Movement()
    {
        if (currentTarget == pointA.position)
        {
            sprite.flipX = true;
        }
        else
        {
            sprite.flipX = false;
        }



        if (transform.position.x == pointA.position.x)
        {
            //Debug.Log("PointA");
            currentTarget = pointB.position;
            anim.SetTrigger("Idle");
            AudioSource.PlayClipAtPoint(idleClip, this.transform.position, 0.1f);
        }
        else if (transform.position.x == pointB.position.x)
        {
            //Debug.Log("PointB");
            currentTarget = pointA.position;
            anim.SetTrigger("Idle");
            AudioSource.PlayClipAtPoint(idleClip, this.transform.position, 0.1f);
        }

        if (isHit == false)
        {
            transform.position = Vector3.MoveTowards(transform.position, currentTarget, speed * Time.deltaTime);
        }

        float distance = Vector3.Distance(transform.localPosition, player.transform.localPosition);
        //Debug.Log("Distance: " + distance + " for: " + transform.name);
        if (distance > 2.0f)
        {
            isHit = false;
            anim.SetBool("InCombat", false);
        }


        Vector3 direction = player.transform.localPosition - transform.localPosition;

        if (direction.x > 0 && anim.GetBool("InCombat") == true)
        {//face right
            sprite.flipX = false;
        }
        else if (direction.x < 0 && anim.GetBool("InCombat") == true)
        {//face left
            sprite.flipX = true;
        }
    }
    
}
