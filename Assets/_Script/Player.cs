using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float maxSpeed;

    Rigidbody2D rigid;
    SpriteRenderer spriterenderer;
    Animator anim;
    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriterenderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }
    public void Update()
    {
        if (Input.GetButtonUp("Horizontal"))
        {
            rigid.velocity = new Vector2(rigid.velocity.normalized.x * 0.5f, rigid.velocity.y);
        }
        //방향 전환
        if (Input.GetButtonDown("Horizontal"))
        {
            spriterenderer.flipX = Input.GetAxisRaw("Horizontal") == -1;
        }
        if(rigid.velocity.normalized.x == 0)
        {
            anim.SetBool("run", false);
        }
        else
        {
            anim.SetBool("run", true);
        }
    }
    private void FixedUpdate()
    {
        float h = Input.GetAxisRaw("Horizontal");

        rigid.AddForce(Vector2.right * h, ForceMode2D.Impulse);

        if (rigid.velocity.x > maxSpeed)//오른쪽 최대 스피드
        {
            rigid.velocity = new Vector2(maxSpeed, rigid.velocity.y);
        }
        else if (rigid.velocity.x < maxSpeed * (-1))//왼쪽 최대 스피드
        {
            rigid.velocity = new Vector2(maxSpeed * (-1), rigid.velocity.y);
        }
    }
}
