using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Player : MonoBehaviour
{

    public ObjectPool objectPool;
    public Transform footPos;
    GameObject landingEffect;

    protected Rigidbody2D rigid2d;
    protected SpriteRenderer spriteRenderer;
    protected Animator animator;


    protected float speed;
    protected float jumpForce;

   

    protected bool isSit;
    public bool isRun;
    protected bool isUp;
    public bool isDown;
    public bool isGrounded;


    protected GameObject currentSavePoint;
    protected Vector3 currentSavePointPos;



    protected CapsuleCollider2D collider;
    protected CapsuleCollider2D childCollider;

    public GameObject dieEffect;
    protected SpriteRenderer dieEffectSR;
    protected Animator dieEffectAnim;

    protected Dictionary<string, Color> colorDic;

    
    
    


    public void Start()
    {
 
        rigid2d = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();

        collider = GetComponent<CapsuleCollider2D>();
        childCollider = transform.GetChild(0).gameObject.GetComponent<CapsuleCollider2D>();

        
        dieEffectAnim = dieEffect.GetComponent<Animator>();


        

        speed = 8;
        jumpForce = 17;


        isSit = false;
        isRun = false;
        isUp = false;
        isDown = false;
        isGrounded = true;



        currentSavePoint = GameObject.Find("savePoint" + GameManager.Instance.savePointNow.ToString());
        currentSavePointPos = currentSavePoint.transform.position;

        colorDic = new Dictionary<string, Color>();
        colorDic.Add("purple", new Color32(150, 93, 198, 255));
        colorDic.Add("yellow", new Color32(255, 192, 99, 255));

    }

    

    protected void StateUpDown()
    {
        if (rigid2d.velocity.y > 1.5) { isUp = true; isDown = false; }
        else if (rigid2d.velocity.y < -1.5) { isUp = false; isDown = true; }
        else { isUp = false; isDown = false; }
    }

    protected void MoveAnim()
    {
        if (!GameManager.Instance.isPaused && !GameManager.Instance.isClear)
        {
            animator.SetBool("isSit", isSit);
            if (!isUp && !isDown) animator.SetBool("isRun", isRun); //?????????? ?????? ??????????X
            else animator.SetBool("isRun", false);
            animator.SetBool("isUp", isUp);
            animator.SetBool("isDown", isDown);
        }
    }

    protected abstract void Move();
    protected abstract void Jump();
    protected abstract void Sit();
    protected abstract void DieEffectColor();
    protected abstract void PlayLandingSound();

    protected void DieEffect()
    {

        var dieEffect_=Instantiate(dieEffect, transform.position, transform.rotation);
        dieEffect_.SetActive(true);
        dieEffectSR = dieEffect_.GetComponent<SpriteRenderer>();
        DieEffectColor();
        gameObject.SetActive(false);
        
    }



    protected void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Obstacle")
        {
            GameManager.Instance.isDead = true;
            GameManager.Instance.GameOver();
            DieEffect();

        }
        else if (collision.gameObject.tag == "Ground")
        {
            isGrounded = true;
            if (!GameManager.Instance.isDead)
            {
                GameObject landingEffect = objectPool.UsePrefab();
                landingEffect.transform.position = footPos.position;
                StartCoroutine(GetBackPrefabCo(landingEffect));
                PlayLandingSound();
            }

        }
    }
    
    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if (collision.gameObject.tag == "Ground")
    //    {
    //        isGrounded = true;  //?????? ?????? ?????? ???? ????
    //    }

       
    //}
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            isGrounded = false;
            
           
        }
    }
    IEnumerator GetBackPrefabCo(GameObject gameObject)
    {
        yield return new WaitForSeconds(0.6f);
        objectPool.GetBackPrefab(gameObject);
    }
   




}
