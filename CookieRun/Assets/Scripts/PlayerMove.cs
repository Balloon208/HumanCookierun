using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMove : MonoBehaviour
{
    [SerializeField]
    private Animator anim;
    [SerializeField]
    private Slider hp;
    [SerializeField]
    private GameObject die;

    public float JumpPower;

    private Rigidbody2D rigid;

    private int jumpCount = 0;
    private bool jump = true;

    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        anim.SetInteger("isJumping", 0);
        anim.SetBool("isSliding", false);
        anim.SetBool("isDying", false);
    }

    public IEnumerator J()
    {
        if (jump == true)
        {
            StartCoroutine("Jump");
            rigid.AddForce(Vector2.up * JumpPower, ForceMode2D.Impulse);
            jumpCount++;
            GameManager.instance.jump = false;
            anim.SetInteger("isJumping", jumpCount);
        }

        yield return null;
    }

    void Update()
    {
        jump = GameManager.instance.jump;

        transform.position = new Vector3(-5f, transform.position.y, transform.position.z);

        if ((Input.GetKeyDown(KeyCode.UpArrow)||GameManager.instance.pyjump==true)&&jump==true)
        {
            StartCoroutine("Jump");
            rigid.AddForce(Vector2.up * JumpPower, ForceMode2D.Impulse);
            jumpCount++;
            GameManager.instance.jump = false;
            GameManager.instance.pyjump = false;
            anim.SetInteger("isJumping", jumpCount);
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            StartCoroutine("Slide");
        }
        else
        {
            anim.SetBool("isSliding", false);
        }
        if (hp.value <= 0)
        {
            GameManager.instance.gameOver = true;
            anim.SetBool("isDying", true);
        }
        if (transform.position.y < -10)
        {
            GameManager.instance.gameOver = true;
            hp.value = 0;
        }

        if (GameManager.instance.gameOver)
        {
            Invoke("StopAnim", 1f);
            die.SetActive(true);
        }
    }

    void StopAnim()
    {
        anim.speed = 0.0f;
    }

    IEnumerator Jump()
    {
        anim.SetInteger("isJumping", 1);
        yield return new WaitForSeconds(0.1f);
    }

    IEnumerator Slide()
    {
        anim.SetBool("isSliding", true);
        yield return new WaitForSeconds(0.1f);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Platform"))
        {
            jumpCount = 0;
            anim.SetInteger("isJumping", jumpCount);
            GameManager.instance.jump = true;
        }
    }
}