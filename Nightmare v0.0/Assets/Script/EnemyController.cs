using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public int nextMove; // �ൿ ��ǥ�� ������ ���� �ϳ� ����
    public float maxSpeed = 2.5f;
    public float bouncPower = 10f;

    bool isHit = false;

    Rigidbody2D rigid;
    Animator anim;
    SpriteRenderer spriteRenderer;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        Invoke("Think", 3); // �־��� �ð��� ������ ������ �Լ��� �����ϴ� �Լ� 
    }

    void FixedUpdate()
    {
        if (!isHit)
        {
            rigid.velocity = new Vector2(nextMove * maxSpeed, rigid.velocity.y);

            //���� üũ
            Vector2 frontVec = new Vector2(rigid.position.x + nextMove * 0.4f, rigid.position.y);

            //Debug.DrawRay(frontVec, Vector3.down, new Color(0, 1, 0)); // ������ �󿡼��� ���̸� �׷��ش�
            RaycastHit2D rayHit = Physics2D.Raycast(frontVec, Vector3.down, 1, LayerMask.GetMask("Map"));
            if (rayHit.collider == null) // �ٴ� ������ ���� ��
                Turn();
        }
    }

    void Think()
    {
        Move();

        float nextThinkTime = Random.Range(2f, 5f); // 2.0 ~ 5.0
        Invoke("Think", nextThinkTime);
    }

    void Move() // �̵� ���� ����
    {
        nextMove = Random.Range(-1, 2); // -1, 0, 1
        Animate();
    }

    void Turn() // �ӵ��� ������ä�� ���⸸ ��ȯ -> ���� �׼Ǳ��� 2�� ����
    {
        nextMove *= -1;
        Animate();

        CancelInvoke();
        Invoke("Think", 2);
    }

    void Animate() // ��������Ʈ �ִϸ��̼� ����
    {
        anim.SetInteger("runSpeed", nextMove);

        if (nextMove != 0)
            spriteRenderer.flipX = nextMove == 1;
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

        int dir = transform.position.x - targetPos.x > 0 ? 1 : -1; // �ǰݽ� ƨ�ܳ����� ���� ����
        rigid.AddForce(new Vector2(dir, 1) * bouncPower, ForceMode2D.Impulse); // ƨ�ܳ�����
        this.transform.Rotate(0, 0, dir * (-10)); // ȸ��

        Destroy(gameObject, 1);
    }
}