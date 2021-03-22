using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;
    [SerializeField] private Collider2D coll;
    private Collider2D discoll;
    //public AudioSource jumpAudio, hurtAudio, collectAudio, overAudio;
    public Transform CeilingCheck, GroundCheck;
    private bool isGround,isJump;
    [Space]
    public float speed;
    public float jumpforce;
    [Space]
    public LayerMask ground;
    public int Cherry;
    public int Gem;
    private bool isHurt;

    public Text CherryNum;
    public Text GemNum;
    public GameObject dialog;
    public GameObject heath1;
    public GameObject heath2;
    public GameObject heath3;

    private float HPNum = 3;
    private bool isDamage = true;
    private int extraJump;
    [Space]
    private bool jumpPressed;
    private int jumpCount;


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        coll = GetComponent<CircleCollider2D>();
        discoll = GetComponent<BoxCollider2D>();
    }
    private void FixedUpdate()
    {
        isGround = Physics2D.OverlapCircle(GroundCheck.position, 0.1f, ground);
        if (!isHurt)
        {
            Movement();
        }
        FinallyJump();
        SwitchAnim();
    }
    private void Update()
    {
        if (Input.GetButtonDown("Jump") && jumpCount > 0)
        {
            jumpPressed = true;
        }
        //Jump();
        //NewJump();
        Crouch();
        CherryNum.text = Cherry.ToString();
        GemNum.text = Gem.ToString();
    }
    void Movement()
    {
        float horizontalMove = Input.GetAxis("Horizontal");
        float facedirection = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(horizontalMove * speed * Time.fixedDeltaTime, rb.velocity.y);
        //因为用的是刚体的X的值，稍微遇到碰撞的时候就会停下。可以改成
        //角色移动
        //transform.Translate(horizontalMove * speed * Time.deltaTime, 0, 0);
        anim.SetFloat("running", Mathf.Abs(facedirection));
        if (facedirection != 0)
        {
            transform.localScale = new Vector3(facedirection, 1, 1);
        }
    }
    void Crouch()
    {
        if (!Physics2D.OverlapCircle(CeilingCheck.position, 0.2f, ground))
        {
            if (Input.GetButton("Crouch"))
            {
                anim.SetBool("crouching", true);
                discoll.enabled = false;
            }
            else
            {
                anim.SetBool("crouching", false);
                discoll.enabled = true;
            }
        }
    }
    //切换动画效果
    void SwitchAnim()
    {
        //anim.SetBool("idle", false);
        if (rb.velocity.y < 0.1f && !coll.IsTouchingLayers(ground))
        {
            anim.SetBool("falling", true);
        }
        if (anim.GetBool("jumping"))
        {
            if (rb.velocity.y < 0)
            {
                anim.SetBool("jumping", false);
                anim.SetBool("falling", true);
            }
        }
        else if (isHurt)
        {
            if(isDamage)
            {
                GetDamage();
                isDamage = false;
            }
            anim.SetBool("hurt", true);
            if (Mathf.Abs(rb.velocity.x) < 0.1f)
            {
                anim.SetBool("hurt", false);
                //anim.SetBool("idle", true); 
                isHurt = false;
                isDamage = true;
            }
        }
        else if (coll.IsTouchingLayers(ground))
        {
            anim.SetBool("falling", false);
            //anim.SetBool("idle", true);
        }
    }
    //旧的角色跳跃
    /*void Jump()
    {
        if (Input.GetButton("Jump") && coll.IsTouchingLayers(ground))
        {
            rb.velocity = new Vector2(rb.velocity.x, 0);
            rb.velocity = new Vector2(rb.velocity.x, jumpforce);
            jumpAudio.Play();
            anim.SetBool("jumping", true);
        }
    }*/
    // 第二版角色跳跃
    /*void NewJump()
    {
        if(isGround)
        {
            extraJump = 1;
        }
        if(Input.GetButtonDown("Jump")&&extraJump > 0)
        {
            rb.velocity = Vector2.up * jumpforce;
            extraJump--;
            SoundManager.instance.JumpAudio();
            //jumpAudio.Play();
            anim.SetBool("jumping", true);
        }
        if (Input.GetButtonDown("Jump") && extraJump == 0 && isGround)
        {
            rb.velocity = Vector2.up * jumpforce;
            SoundManager.instance.JumpAudio();
            //jumpAudio.Play();
            anim.SetBool("jumping", true);
        }
    }*/
    // 最终版跳跃
    private void FinallyJump()
    {
        if (isGround)
        {
            jumpCount = 1;
            isJump = false;
        }
        if(jumpPressed && isGround)
        {
            isJump = true;
            rb.velocity = new Vector2(rb.velocity.x, jumpforce);
            jumpCount--;
            jumpPressed = false;
            SoundManager.instance.JumpAudio();
            anim.SetBool("jumping", true);
        }
        //这里选用!isGround而不是isJump原因
        //是因为使我在天空中落下的时候也可以跳跃
        else if(jumpPressed && jumpCount>0 && !isGround)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpforce);
            jumpCount--;
            jumpPressed = false;
            SoundManager.instance.JumpAudio();
            anim.SetBool("jumping", true);
        }
    }
    //收集物品
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Cherry")
        {
            //防止音效播放两遍
            collision.tag = "null";
            SoundManager.instance.CollectAudio();
            //collectAudio.Play();
            collision.GetComponent<Animator>().Play("isGot");
            //Destroy(collision.gameObject);
            //注意cherry的碰撞器不能碰到地面，不然会+2
            //Cherry += 1;
            //CherryNum.text = Cherry.ToString();
        }
        if (collision.tag == "Gem")
        {
            //防止音效播放两遍
            collision.tag = "null";
            SoundManager.instance.CollectAudio();
            //collectAudio.Play();
            collision.GetComponent<Animator>().Play("isGot");
            //Destroy(collision.gameObject);
            //Gem += 1;
            //GemNum.text = Gem.ToString();
        }
        if (collision.tag == "House")
        {
            dialog.SetActive(true);
        }
        if (collision.tag == "DeadLine")
        {
            GameOver();
        }

    }
    private void GameOver()
    {
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
        //GetComponent<AudioSource>().enabled = false;
        anim.enabled = false;
        coll.enabled = false;
        SoundManager.instance.OverAudio();
        //overAudio.Play();
        Invoke(nameof(RestStrat), 2f);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "House")
        {
            dialog.SetActive(false);
        }
    }
    //击杀怪物
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();
            if (anim.GetBool("falling") && transform.position.y > (collision.gameObject.transform.position.y + 0.5))
            {
                enemy.JumpOn();
                rb.velocity = new Vector2(rb.velocity.x, jumpforce);
                anim.SetBool("jumping", true);
            }
            else if (transform.position.x < collision.gameObject.transform.position.x)
            {
                SoundManager.instance.HurtAudio();
                //hurtAudio.Play();
                rb.velocity = new Vector2(-8, rb.velocity.y);
                isHurt = true;
            }
            else if (transform.position.x > collision.gameObject.transform.position.x)
            {
                SoundManager.instance.HurtAudio();
                //hurtAudio.Play();
                rb.velocity = new Vector2(8, rb.velocity.y);
                isHurt = true;
            }
        }
    }
    private void RestStrat()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void CherryCount()
    {
        Cherry++;
    }
    public void GemCount()
    {
        Gem++;
    }
    private void GetDamage()
    {
        if (HPNum == 3)
        {
            HPNum--;
            heath3.SetActive(false);
            //Debug.Log(HPNum);
        }
        else if (HPNum == 2)
        {
            HPNum--;
            heath2.SetActive(false);
            //Debug.Log(HPNum);
        }
        else if (HPNum == 1)
        {
            HPNum--;
            heath1.SetActive(false);
            //Debug.Log(HPNum);
        }
        if (HPNum <= 0)
        {
            GameOver();
        }
    }
}
