using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMain : MonoBehaviour
{
    public int hp;
    public float bouncPower = 3f;
    public float invulnTIme = 0.2f;

    bool isHit = false;

    Rigidbody2D rigid;
    Animator anim;
    SpriteRenderer spriteRenderer;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player Attack")
            onHit(collision.transform.position);
    }

    void onHit(Vector2 targetPos)
    {
        isHit = true;
        gameObject.layer = 9; // Super Armor
        spriteRenderer.color = new Color(1, 1, 1, 0.4f);

        int dir = transform.position.x - targetPos.x > 0 ? 1 : -1; // 피격시 튕겨나가는 방향 결정
        rigid.AddForce(new Vector2(dir, 1) * bouncPower, ForceMode2D.Impulse); // 튕겨나가기
        this.transform.Rotate(0, 0, dir * (-10)); // 회전

        HpDown();
    }

    public void HpDown()
    {
        hp--;
        if (hp == 0)
            StartCoroutine(Dead());

        Invoke("OffHit", invulnTIme);
    }

    void OffHit()
    {
        gameObject.layer = 10; // Layer 변경
        spriteRenderer.color = new Color(1, 1, 1, 1f); // 색 변경
        this.transform.rotation = Quaternion.Euler(0, 0, 0);
        isHit = false;
    }

    IEnumerator Dead()
    {
        yield return new WaitForSeconds(0.35f);
        Destroy(gameObject, 1);
    }

    public bool IsHit()
    {
        return isHit;
    }
}
