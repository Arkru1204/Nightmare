using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAController : MonoBehaviour
{
    public float maxSpeed = 1f;
    public float raycastDistance = 4f;
    public float attackDelay = 2f;

    int saveDir = 1;
 
    bool isAttack = false;


    int nextMove; // 행동 지표를 결정할 변수 하나 생성

    Rigidbody2D rigid;
    Animator anim;
    EnemyMain enemyMain;
    InArea inArea;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        enemyMain = GetComponent<EnemyMain>();
        inArea = GetComponentInChildren<InArea>();

        Invoke("Think", 3); // 주어진 시간이 지난뒤 지정된 함수를 실행하는 함수 
    }

    private void Update()
    {
        if (inArea.getIsArea() && !anim.GetCurrentAnimatorStateInfo(0).IsName("BossA_NomalAttack") && isAttack == false)
        {
            Invoke("OnFire", attackDelay);
        }

        if (!inArea.getIsArea())
        {
            CancelInvoke("OnFire");
            anim.SetBool("doNomalAttack", false);
        }
    }

    void FixedUpdate()
    {
        if (!enemyMain.IsHit() && !anim.GetCurrentAnimatorStateInfo(0).IsName("BossA_NomalAttack"))
        {
            rigid.velocity = new Vector2(nextMove * maxSpeed, rigid.velocity.y);

            //지형 체크
            Vector2 frontVec = new Vector2(rigid.position.x + nextMove * raycastDistance, rigid.position.y);

            //Debug.DrawRay(frontVec, Vector3.down, new Color(0, raycastDistance, 0)); // 에디터 상에서만 레이를 그려준다
            RaycastHit2D rayHit = Physics2D.Raycast(frontVec, Vector3.down, raycastDistance, LayerMask.GetMask("Map"));
            if (rayHit.collider == null) // 바닥 감지가 없을 때
                Turn();
        }

        if (anim.GetCurrentAnimatorStateInfo(0).IsName("BossA_NomalAttack")) // 공격 시 멈춤
            rigid.velocity = new Vector2(rigid.velocity.normalized.x * 0.0000001f, rigid.velocity.y);

        if (isAttack)
        {
            isAttack = false;
            Invoke("AttackEnd", 1.4f);
        }
    }

    void Think()
    {
        if (!enemyMain.IsHit() && isAttack == false)
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

        CancelInvoke("Think");
        Invoke("Think", 2);
    }

    void Animate() // 스프라이트 애니메이션 관련
    {
        anim.SetInteger("runSpeed", nextMove);

        if (nextMove != 0)
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x) * -nextMove, transform.localScale.y, transform.localScale.z);
        //spriteRenderer.flipX = nextMove == 1;
    }

    void OnFire()
    {
        if (inArea.getIsArea())
        {
            isAttack = true;
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x) * -inArea.getPlayerDir(), transform.localScale.y, transform.localScale.z);
            anim.SetBool("doNomalAttack", true);
        }
    }

    public void AttackEnd()
    {
        //Debug.Log("attack End!");
        anim.SetBool("doNomalAttack", false);

        //Invoke("OnFire", attackDelay);
    }
}
