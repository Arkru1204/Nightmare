using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float maxSpeed = 2.5f;
    public float raycastDistance = 1f;

    // 인스팩터X
    [System.NonSerialized] public int saveDir = 1;

    int nextMove; // 행동 지표를 결정할 변수 하나 생성

    Rigidbody2D rigid;
    Animator anim;
    SpriteRenderer spriteRenderer;
    EnemyMain enemyMain;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        enemyMain = GetComponent<EnemyMain>();

        Invoke("Think", 3); // 주어진 시간이 지난뒤 지정된 함수를 실행하는 함수 
    }

    void FixedUpdate()
    {
        if (!enemyMain.IsHit())
        {
            rigid.velocity = new Vector2(nextMove * maxSpeed, rigid.velocity.y);

            //지형 체크
            Vector2 frontVec = new Vector2(rigid.position.x + nextMove * raycastDistance, rigid.position.y);

            //Debug.DrawRay(frontVec, Vector3.down, new Color(0, raycastDistance, 0)); // 에디터 상에서만 레이를 그려준다
            RaycastHit2D rayHit = Physics2D.Raycast(frontVec, Vector3.down, raycastDistance, LayerMask.GetMask("Map"));
            if (rayHit.collider == null) // 바닥 감지가 없을 때
                Turn();
        }
    }

    void Think()
    {
        if (!enemyMain.IsHit())
        {
            Move();

            float nextThinkTime = Random.Range(2f, 5f); // 2.0 ~ 5.0
            Invoke("Think", nextThinkTime);
        }
    }

    void Move() // 이동 방향 결정
    {
        nextMove = Random.Range(-1, 2); // -1, 0, 1

        if (nextMove == 1)  // 방향 저장용
            saveDir = 1;
        else if (nextMove == -1)
            saveDir = -1;

        Animate();
    }

    public void Turn() // 속도는 유지한채로 방향만 전환 -> 다음 액션까지 2초 연장
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
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x) * -nextMove, transform.localScale.y, transform.localScale.z);
        //spriteRenderer.flipX = nextMove == 1;
    }

    public void lookBack()
    {   
        transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
    }

    public int getThink()
    {
        return nextMove;
    }

    public int getDir()
    {
        return saveDir;
    }
}
