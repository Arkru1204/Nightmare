using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMain : MonoBehaviour
{
    public GameManager gameManager;

    public float bouncPower = 10f;
    public float invulnTIme = 0.8f;

    bool isHit = false;

    Rigidbody2D rigid;
    Animator anim;
    SpriteRenderer spriteRenderer;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>(); // 하위 오브젝트
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
            onHit(collision.transform.position); // Enemy의 위치 정보 매개변수
        if (collision.gameObject.tag == "Trap")
            onHit(collision.transform.position);
    }

    void onHit(Vector2 targetPos)
    {
        gameObject.layer = 9; // 플레이어의 Layer 변경 (Super Armor)
        spriteRenderer.color = new Color(1, 1, 1, 0.4f); // 피격당했을 때 색 변경
        isHit = true;

        int dir = transform.position.x - targetPos.x > 0 ? 1 : -1; // 피격시 튕겨나가는 방향 결정
        rigid.AddForce(new Vector2(dir, 1) * bouncPower, ForceMode2D.Impulse); // 튕겨나가기

        this.transform.Rotate(0, 0, dir * (-10)); // 회전
        Invoke("reRotate", 0.4f);

        anim.SetTrigger("doHit"); // 애니메이션 트리거
        Invoke("offHit", invulnTIme); // invulnTIme 후 무적 시간 끝
    }

    void offHit()
    {
        gameObject.layer = 8; // Layer 변경
        spriteRenderer.color = new Color(1, 1, 1, 1f); // 색 변경
    }

    void reRotate() // 회전 초기화, 다시 조작 가능
    {
        this.transform.rotation = Quaternion.Euler(0, 0, 0);
        isHit = false;
    }

    public bool IsHit()
    {
        return isHit;
    }
}
