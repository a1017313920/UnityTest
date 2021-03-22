using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Eagle : Enemy
{
    private Rigidbody2D rb;
    public Transform uppoint, downpoint;
    private float upy, downy;
    public float speed;
    private bool Faceup = true;


    protected override void Awake()
    {
        base.Awake();
        rb = GetComponent<Rigidbody2D>();
    }
    protected override void Start()
    {
        //transform.DetachChildren();
        upy = uppoint.position.y;
        downy = downpoint.position.y;
        Destroy(uppoint.gameObject);
        Destroy(downpoint.gameObject);
    }
    private void Update()
    {
        Movement();
    }

    private void Movement()
    {
        if (Faceup)
        {
            rb.velocity = new Vector2(rb.velocity.x, speed);
            if (transform.position.y > upy)
            {
                Faceup = false;
            }
        }
        else
        {
            rb.velocity = new Vector2(rb.velocity.x, -speed);
            if (transform.position.y < downy)
            {
                Faceup = true;
            }
        }
    }
}
