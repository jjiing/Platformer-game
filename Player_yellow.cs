using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_yellow : Player
{
    
    void Start()
    {
        base.Start();

        
        gameObject.transform.position = currentSavePointPos + new Vector3(1, 0.5f, 0);

        
        
    }
    
    void Update()
    {
        //������
        Move();
        Jump();
        Sit();

        //���
        StateUpDown();
        MoveAnim();

    }
    
    protected override void DieEffect()
    {
        
        
        Color color = spriteRenderer.color;
        color.a = 0;
        spriteRenderer.color = color;

        //����Ʈ

        


    }
    
    protected override void Move()
    {
        //�̵�
        float moveX = Input.GetAxisRaw("Horizontal");
        rigid2d.velocity = new Vector2(moveX * speed, rigid2d.velocity.y);
        //x�� �̵��� x*speed�� y�� �̵��� ������ �ӷ°�(����� �߷�)


        //anim �¿���⼳��
        if (moveX < 0) spriteRenderer.flipX = false;
        else if (moveX > 0) spriteRenderer.flipX = true;


        //���� ����
        if (moveX != 0) isRun = true;
        else isRun = false;

    }
    protected override void Jump() 
    {
        if (Input.GetKeyDown(KeyCode.UpArrow) && isGrounded)
        {
            //isGrounded = false;
            rigid2d.velocity = Vector2.up * jumpForce;

        }



    }
    protected override void Sit()
    {
        if (Input.GetKey(KeyCode.DownArrow))
        {
            isSit = true;
            collider.enabled = false;
            childCollider.enabled = true;

        }
        else
        {
            isSit = false;
            collider.enabled = true;
            childCollider.enabled = false;
        }
    }
    
}
