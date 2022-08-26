using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    Rigidbody2D rigid;
    public int nextMove;
    Animator anim;
    SpriteRenderer sprite;
    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        Invoke("Think", 5);
    }
    private void FixedUpdate()
    {
        //이동
        rigid.velocity = new Vector2(nextMove, rigid.velocity.y);

        //지형감지
        Vector2 frontVec = new Vector2(rigid.position.x + nextMove, rigid.position.y);
        Debug.DrawRay(frontVec, Vector3.down, new Color(0, 1, 0));
        RaycastHit2D rayHit = Physics2D.Raycast(frontVec, Vector3.down, 1, LayerMask.GetMask("rand"));

        if (rayHit.collider == null)
        {
            turn();
        }
    }
    void Think()
    {
        nextMove = Random.Range(-1, 2);
        

        Invoke("Think", 5);
        anim.SetInteger("RunSpeed", nextMove);
        if(nextMove !=0)
            sprite.flipX = nextMove == -1;
        float nextThinkTime = Random.Range(2f, 5f);
        Invoke("Think", nextThinkTime);
    }
    void turn()
    {
        nextMove *= -1;
        sprite.flipX = nextMove == 1;
        CancelInvoke();
        Invoke("Think", 2);
    }
}
