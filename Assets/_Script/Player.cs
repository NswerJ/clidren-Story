using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public float maxSpeed;
    public float jumpPower;

    public AudioClip clip;

    public int Heart = 3;
    public GameObject Heart1;
    public GameObject Heart2;
    public GameObject Heart3;


    Rigidbody2D rigid;
    SpriteRenderer spriterenderer;
    Animator anim;
    public GameObject player;
    public Transform[] playerSpawnPoints;

                


    void RandomSelectSpawnPoint()
    {
        int number = Random.Range(0, playerSpawnPoints.Length);
        player.transform.position = playerSpawnPoints[number].transform.position;
    }
    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriterenderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }
    public void Update()
    {

        if (Heart == 2)
        {
            Heart3.SetActive(false);
        }
        if (Heart == 1)
        {
            Heart2.SetActive(false);
        }

        if (Heart == 0)
        {
            SceneManager.LoadScene("GameOver");
            Heart1.SetActive(false);
        }
        //점프
        if (Input.GetButtonDown("Jump") && !anim.GetBool("Jump"))
        {
            rigid.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
            anim.SetBool("Jump", true);
            SoundManager.instance.SFXPlay("Jump", clip);
        }
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
        if(rigid.velocity.y < 0)
        {
            Debug.DrawRay(rigid.position, Vector3.down, new Color(0, 1, 0));
            RaycastHit2D rayHit = Physics2D.Raycast(rigid.position, Vector3.down, 1, LayerMask.GetMask("rand"));

            if (rayHit.collider != null)
            {
                if (rayHit.distance < 0.9f)
                {
                    anim.SetBool("Jump", false);
                }

            }
        }
        
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("out"))
        {
            Heart--;
            RandomSelectSpawnPoint();
        }
        if (collision.CompareTag("Spike"))
        {
            Heart--;
            RandomSelectSpawnPoint();
        }
        if (collision.CompareTag("clear"))
        {
            SceneManager.LoadScene("stage 2");
        }
        if (collision.CompareTag("clear2"))
        {
            SceneManager.LoadScene("stage 3");
        }
        if (collision.CompareTag("clear3"))
        {
            SceneManager.LoadScene("Clear");
        }
        if (collision.CompareTag("monster"))
        {
            Heart --;
            RandomSelectSpawnPoint();
        }
        if (collision.CompareTag("loser"))
        {
            SceneManager.LoadScene("Stage");
        }
    }
    
}
