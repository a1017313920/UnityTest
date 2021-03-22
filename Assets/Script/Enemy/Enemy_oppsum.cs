using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_oppsum : Enemy
{
    private Rigidbody2D rb;
    private float leftx, rightx;
    private bool Faceleft = true;

    public float speed;
    public Transform leftpoint, rightpoint;
    protected override void Awake()
    {
        base.Awake();
        rb = GetComponent<Rigidbody2D>();
        
    }
    protected override void Start()
    {
        leftx = leftpoint.position.x;
        rightx = rightpoint.position.x;
        Destroy(leftpoint.gameObject);
        Destroy(rightpoint.gameObject);
    }
    private void Update()
    {
        Movement();
    }
    private void Movement()
    {
        if (Faceleft)
        {
            if (transform.position.x > leftx)
                rb.velocity = new Vector2(-speed, rb.velocity.y);
            else
            {
                Faceleft = !Faceleft;
                transform.localScale = new Vector3(-1, 1, 1);
            }
        }
        else
        {
            if (transform.position.x < rightx)
                rb.velocity = new Vector2(speed, rb.velocity.y);
            else
            {
                Faceleft = !Faceleft;
                transform.localScale = new Vector3(1, 1, 1);
            }
        }

    }
}
