using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossCController : MonoBehaviour
{
    // 인스펙터
    [Header ("기본 설정")]
    public float maxSpeed = 1f;
    public float raycastDistance = 5f;
    public float attackDelay = 2f;
    public float attackDistance = 3.5f;

    [Header ("발사 관련")]
    public GameObject bullet;
    public float throwX = 4;
    public float throwY = 0;
    public float offsetY = 0;

    // 체크용
    int saveDir = 1;
    bool isAttack = false;

    // AI 관련
    int nextMove; // 행동 지표를 결정 변수
    int nextAttack;

    // 컴포넌트
    Rigidbody2D rigid;
    Animator anim;
    BossMain bossMain;
    InArea inArea;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        bossMain = GetComponent<BossMain>();
        inArea = GetComponentInChildren<InArea>();

        Invoke("Think", 3); // 주어진 시간이 지난뒤 지정된 함수를 실행하는 함수 
    }

    private void Update()
    {
        if (inArea.getIsArea() && !IsInvoking("AttackEnd") && !isAttack) // 공격
        {
            isAttack = true;
            nextAttack = Random.Range(0, 2); // 0, 1
            if (nextAttack == 0)
            {
                Invoke("OnAttackA", attackDelay);
            }
            else
            {
                Invoke("OnAttackB", attackDelay);
            }
        }

        if (!inArea.getIsArea() && inArea.getAggro() && !IsInvoking("AttackEnd")) // 영역 밖이지만 어글이 있을 때
        {
            Invoke("OnFIre", attackDelay);
            //CancelInvoke("OnAttackA");
            //CancelInvoke("OnAttackB");
        }
    }

    void FixedUpdate()
    {
        if (!bossMain.IsHit() && !IsInvoking("AttackEnd"))
        {
            rigid.velocity = new Vector2(nextMove * maxSpeed, rigid.velocity.y);

            //지형 체크
            Vector2 frontVec = new Vector2(rigid.position.x + nextMove * raycastDistance, rigid.position.y);

            //Debug.DrawRay(frontVec, Vector3.down, new Color(0, raycastDistance, 0)); // 에디터 상에서만 레이를 그려준다
            RaycastHit2D rayHit = Physics2D.Raycast(frontVec, Vector3.down, raycastDistance, LayerMask.GetMask("Map"));
            if (rayHit.collider == null) // 바닥 감지가 없을 때
                Turn();
        }

        if (IsInvoking("AttackEnd")) // 공격 시 이동 멈춤
            rigid.velocity = new Vector2(rigid.velocity.normalized.x * 0.0000001f, rigid.velocity.y);
    }

    // ======================== 기본 이동 AI =======================
    void Think()
    {
        if (!bossMain.IsHit())
        {
            Move();

            float nextThinkTime = Random.Range(2f, 5f); // 2.0 ~ 5.0
            Invoke("Think", nextThinkTime);
        }
    }

    void Move() // 이동 방향 결정
    {
        nextMove = Random.Range(-1, 2); // -1, 0, 1

        //if (nextMove == 1)  // 방향 저장용
        //    saveDir = 1;
        //else if (nextMove == -1)
        //    saveDir = -1;

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
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x) * nextMove, transform.localScale.y, transform.localScale.z);
        //spriteRenderer.flipX = nextMove == 1;
    }

    // ======================== 공격 함수 ========================
    void OnAttackA()
    {
        transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x) * inArea.getPlayerDir(), transform.localScale.y, transform.localScale.z);
        anim.SetBool("isAttackA", true);
        Invoke("AttackEnd", 1f); // 애니메이션 끝나는 시간
    }

    void OnAttackB()
    {
        transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x) * inArea.getPlayerDir(), transform.localScale.y, transform.localScale.z);
        anim.SetBool("isAttackB", true);
        Invoke("AttackEnd", 1.75f);
    }

    void OnFIre()
    {
        transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x) * inArea.getPlayerDir(), transform.localScale.y, transform.localScale.z);
        anim.SetBool("isAttackC", true);
        Invoke("AttackEnd", 1.17f);
    }

    public void AttackEnd()
    {
        anim.SetBool("isAttackA", false);
        anim.SetBool("isAttackB", false);
        anim.SetBool("isAttackC", false);
        isAttack = false;
    }

    // ======================== 호출 함수 =========================

    public void movePlayerPos() // 공격 시 호출, 플레이어 앞에 출현
    {
        transform.position = new Vector3(inArea.getPlayerPos().x + (-inArea.getPlayerDir() * attackDistance), transform.position.y, transform.position.z);
    }

    public void Fire()
    {
        Vector3 newPos = this.transform.position;
        newPos.y += offsetY;

        GameObject newGameObject = Instantiate(bullet) as GameObject;
        //newPos.z = -5;
        newGameObject.transform.position = newPos;
        newGameObject.transform.localScale = new Vector3(newGameObject.transform.localScale.y * inArea.getPlayerDir(), newGameObject.transform.localScale.y, newGameObject.transform.localScale.z);

        Rigidbody2D rbody = newGameObject.GetComponent<Rigidbody2D>();
        rbody.AddForce(new Vector2(throwX * inArea.getPlayerDir(), throwY), ForceMode2D.Impulse);
    }
}
