using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player_purple : Player
{

    void Start()
    {
        base.Start();
        
        gameObject.transform.position = currentSavePointPos + new Vector3(-1, 0.5f, 0);


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

   

    protected override void Move()
    {
        //�̵�
        float moveX = Input.GetAxisRaw("Horizontal2");
        rigid2d.velocity = new Vector2(moveX * speed, rigid2d.velocity.y);
        //x�� �̵��� x*speed�� y�� �̵��� ������ �ӷ°�(����� �߷�)

        //�̲����� ����
        if (moveX == 0) rigid2d.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
        else rigid2d.constraints = RigidbodyConstraints2D.FreezeRotation;

        //anim �¿���⼳��
        if (!GameManager.Instance.isPaused && !GameManager.Instance.isClear)
        {
            if (moveX < 0) spriteRenderer.flipX = false;
            else if (moveX > 0) spriteRenderer.flipX = true;
        }


        //���� ����
        if (moveX != 0) isRun = true;
        else isRun = false;

        //����
        if (isRun && !AudioManager.Instance.audioSources[constant.PLAYER_P_AUDIO_SOURCE].isPlaying)
        {
            if(SceneManager.GetActiveScene().name == "stage1")
                AudioManager.Instance.PlaySE("s1Footstep", constant.PLAYER_P_AUDIO_SOURCE);
            else if(SceneManager.GetActiveScene().name == "stage2")
                AudioManager.Instance.PlaySE("s2Footstep", constant.PLAYER_P_AUDIO_SOURCE);

        }
        
    }
    protected override void Jump()
    {
        if (Input.GetKeyDown(KeyCode.W) && isGrounded)
        {
            isGrounded = false;
            rigid2d.velocity = Vector2.up * jumpForce;

        }



    }
    protected override void Sit()
    {

            if (Input.GetKey(KeyCode.S))
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
    protected override void DieEffectColor()
    {

        dieEffectSR.color = colorDic["purple"];
    }
    protected override void PlayLandingSound()
    {

        
            if (SceneManager.GetActiveScene().name == "stage1")
                AudioManager.Instance.PlaySE("s1Landing", constant.PLAYER_P_AUDIO_SOURCE);
            else if (SceneManager.GetActiveScene().name == "stage2")
                AudioManager.Instance.PlaySE("s2Landing", constant.PLAYER_P_AUDIO_SOURCE);
        
    }
}
