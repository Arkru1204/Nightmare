using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public int nextMove; // 행동 지표를 결정할 변수 하나 생성
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

        Invoke("Think", 3); // 주어진 시간이 지난뒤 지정된 함수를 실행하는 함수 
    }

    void FixedUpdate()
    {
        if (!isHit)
        {
            rigid.velocity = new Vector2(nextMove * maxSpeed, rigid.velocity.y);

            //지형 체크
            Vector2 frontVec = new Vector2(rigid.position.x + nextMove * 0.4f, rigid.position.y);

            //Debug.DrawRay(frontVec, Vector3.down, new Color(0, 1, 0)); // 에디터 상에서만 레이를 그려준다
            RaycastHit2D rayHit = Physics2D.Raycast(frontVec, Vector3.down, 1, LayerMask.GetMask("Map"));
            if (rayHit.collider == null) // 바닥 감지가 없을 때
                Turn();
        }
    }

    void Think()
    {
        Move();

        float nextThinkTime = Random.Range(2f, 5f); // 2.0 ~ 5.0
        Invoke("Think", nextThinkTime);
    }

    void Move() // 이동 방향 결정
    {
        nextMove = Random.Range(-1, 2); // -1, 0, 1
        Animate();
    }

    void Turn() // 속도는 유지한채로 방향만 전환 -> 다음 액션까지 2초 연장
    {
        nextMove *= -1;
        Animate();

        CancelInvoke();
        Invoke("Think", 2);
    }

    void Animate() // 스프라이트 애니메이션 관련
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

        int dir = transform.position.x - targetPos.x > 0 ? 1 : -1; // 피격시 튕겨나가는 방향 결정
        rigid.AddForce(new Vector2(dir, 1) * bouncPower, ForceMode2D.Impulse); // 튕겨나가기
        this.transform.Rotate(0, 0, dir * (-10)); // 회전

        Destroy(gameObject, 1);
    }
}
