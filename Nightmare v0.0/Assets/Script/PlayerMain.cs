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
        spriteRenderer = GetComponentInChildren<SpriteRenderer>(); // ���� ������Ʈ
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
            onHit(collision.transform.position); // Enemy�� ��ġ ���� �Ű�����
        if (collision.gameObject.tag == "Trap")
            onHit(collision.transform.position);
    }

    void onHit(Vector2 targetPos)
    {
        gameObject.layer = 9; // �÷��̾��� Layer ���� (Super Armor)
        spriteRenderer.color = new Color(1, 1, 1, 0.4f); // �ǰݴ����� �� �� ����
        isHit = true;

        int dir = transform.position.x - targetPos.x > 0 ? 1 : -1; // �ǰݽ� ƨ�ܳ����� ���� ����
        rigid.AddForce(new Vector2(dir, 1) * bouncPower, ForceMode2D.Impulse); // ƨ�ܳ�����

        this.transform.Rotate(0, 0, dir * (-10)); // ȸ��
        Invoke("reRotate", 0.4f);

        anim.SetTrigger("doHit"); // �ִϸ��̼� Ʈ����
        Invoke("offHit", invulnTIme); // invulnTIme �� ���� �ð� ��
    }

    void offHit()
    {
        gameObject.layer = 8; // Layer ����
        spriteRenderer.color = new Color(1, 1, 1, 1f); // �� ����
    }

    void reRotate() // ȸ�� �ʱ�ȭ, �ٽ� ���� ����
    {
        this.transform.rotation = Quaternion.Euler(0, 0, 0);
        isHit = false;
    }

    public bool IsHit()
    {
        return isHit;
    }
}
