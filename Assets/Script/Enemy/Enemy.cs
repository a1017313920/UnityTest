using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    protected Animator anim;
    protected AudioSource deathAudio;
    protected virtual void Awake()
    {
        anim = GetComponent<Animator>();
        deathAudio = GetComponent<AudioSource>();
    }
    protected virtual void Start()
    {

    }
    public void Death()
    {
        Destroy(gameObject);
    }
    public void JumpOn()
    {
        GetComponent<Collider2D>().enabled = false;
        anim.SetTrigger("death");
        deathAudio.Play();
    }
}
