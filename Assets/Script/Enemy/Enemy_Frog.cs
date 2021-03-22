using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Frog : Enemy
{
    private Rigidbody2D rb;
    //private Animator anim;
    private Collider2D coll;
    private float leftx, rightx;
    private bool Faceleft = true;

    public float speed, jumpForce;
    public Transform leftpoint, rightpoint;
    public LayerMask ground;


    protected override void Awake()
    {
        base.Awake();
        rb = GetComponent<Rigidbody2D>();
       // anim = GetComponent<Animator>();
        coll = GetComponent<Collider2D>();
    }
    protected override void Start()
    {
        //transform.DetachChildren();
        leftx = leftpoint.position.x;
        rightx = rightpoint.position.x;
        Destroy(leftpoint.gameObject);
        Destroy(rightpoint.gameObject);
    }
    private void Update()
    {
        SwitchAnim();
    }

    private void Movement()
    {
        if(Faceleft)
        {
            if (coll.IsTouchingLayers(ground))
            {
                anim.SetBool("jumping", true);
                rb.velocity = new Vector2(-speed, jumpForce);
            }
            if (transform.position.x < leftx)
            {
                rb.velocity = new Vector2(0, 0);
                transform.localScale = new Vector3(-1, 1, 1);
                Faceleft = false;
            }
        }
        else
        {
            if (coll.IsTouchingLayers(ground))
            {
                anim.SetBool("jumping", true);
                rb.velocity = new Vector2(speed, jumpForce);
            }
            if (transform.position.x > rightx)
            {
                rb.velocity = new Vector2(0, 0);
                transform.localScale = new Vector3(1, 1, 1);
                Faceleft = true;
            }
        }
    }
    private void SwitchAnim()
    {
        if(anim.GetBool("jumping"))
        {
            if(rb.velocity.y < 0.1f)
            {
                anim.SetBool("jumping", false);
                anim.SetBool("falling", true);
            }
        }
        if(coll.IsTouchingLayers(ground)&&anim.GetBool("falling"))
        {
            anim.SetBool("falling", false);
        }
    }
}
