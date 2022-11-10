using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    public int nextMove; // 행동 지표를 결정할 변수 하나 생성

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
        rigid.velocity = new Vector2(nextMove, rigid.velocity.y);

        //지형 체크
        Vector2 frontVec = new Vector2(rigid.position.x + nextMove * 0.4f, rigid.position.y);

        Debug.DrawRay(frontVec, Vector3.down, new Color(0, 1, 0)); // 에디터 상에서만 레이를 그려준다
        RaycastHit2D rayHit = Physics2D.Raycast(frontVec, Vector3.down, 1, LayerMask.GetMask("Map"));
        if (rayHit.collider == null) // 바닥 감지가 없을 때
            Turn();
    }

    void Think()
    {
        Move();

        float nextThinkTime = Random.Range(2f, 5f);
        Invoke("Think", nextThinkTime);
    }

    void Move() // 이동속도 결정
    {
        nextMove = Random.Range(-1, 2);
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
        anim.SetInteger("WalkSpeed", nextMove);

        if (nextMove != 0)
        {
            spriteRenderer.flipX = nextMove == 1;
        }
    }
}
