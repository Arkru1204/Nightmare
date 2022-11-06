using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float maxSpeed;
    public float jumpPower;

    public Vector2 boxCastSize = new Vector2(0.5f, 0.05f);
    public float boxCastMaxDistance = 1.0f;

    Rigidbody2D rigid;
    SpriteRenderer spriteRenderer;
    Animator anim;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        //Jump
        if (Input.GetButtonDown("Jump") && !anim.GetBool("isJumping"))
        {
            rigid.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
            anim.SetBool("isJumping", true);
        }

        // Stop Speed 
        if (Input.GetButtonUp("Horizontal"))
            rigid.velocity = new Vector2(rigid.velocity.normalized.x * 0.0000001f, rigid.velocity.y);

        // change Direction
        if (Input.GetButton("Horizontal"))
            spriteRenderer.flipX = Input.GetAxisRaw("Horizontal") == -1;

        // work animation
        if (Mathf.Abs(rigid.velocity.x) < 0.3) // 절댓값 설정
            anim.SetBool("isRunning", false);
        else
            anim.SetBool("isRunning", true);

        // 미끄러짐 방지
        if (!(Input.GetButton("Horizontal")))
            rigid.velocity = new Vector2(0, rigid.velocity.y);
    }

    void FixedUpdate()
    {
        // Move by Control
        float h = Input.GetAxisRaw("Horizontal"); // 횡으로 키를 누르면
        rigid.AddForce(Vector2.right * h, ForceMode2D.Impulse); // 이동한다

        // MaxSpeed Limit
        if (rigid.velocity.x > maxSpeed)// right
            rigid.velocity = new Vector2(maxSpeed, rigid.velocity.y);
        else if (rigid.velocity.x < maxSpeed * (-1)) // Left Maxspeed
            rigid.velocity = new Vector2(maxSpeed * (-1), rigid.velocity.y);

        // Lending Platform
        if (rigid.velocity.y < 0)
        {
            anim.SetBool("isFalling", true); // 점프 없이 낙하

            //Debug.DrawRay(rigid.position, Vector3.down*2, new Color(0, 1, 0)); //에디터 상에서만 레이를 그려준다
            //RaycastHit2D rayHit = Physics2D.Raycast(rigid.position, Vector3.down, 1, LayerMask.GetMask("Map"));
            RaycastHit2D rayHit = Physics2D.BoxCast(transform.position, boxCastSize, 0f, Vector2.down, boxCastMaxDistance, LayerMask.GetMask("Map"));
            if (rayHit.collider != null) // 바닥 감지를 위해서 레이저
            {
                if (rayHit.distance < 0.9f)
                {
                    anim.SetBool("isJumping", false);
                    anim.SetBool("isFalling", false);
                }
            }
        }
    }

    //void OnDrawGizmos()
    //{
    //    RaycastHit2D raycastHit = Physics2D.BoxCast(transform.position, boxCastSize, 0f, Vector2.down, boxCastMaxDistance, LayerMask.GetMask("Map"));

    //    Gizmos.color = Color.red;
    //    if (raycastHit.collider != null)
    //    {
    //        Gizmos.DrawRay(transform.position, Vector2.down * raycastHit.distance);
    //        Gizmos.DrawWireCube(transform.position + Vector3.down * raycastHit.distance, boxCastSize);
    //    }
    //    else
    //    {
    //        Gizmos.DrawRay(transform.position, Vector2.down * boxCastMaxDistance);
    //    }
    //}
}
