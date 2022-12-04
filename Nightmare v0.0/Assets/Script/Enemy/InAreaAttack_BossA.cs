using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InAreaAttack_BossA : MonoBehaviour
{
    public float attackDelay = 2f;

    bool isArea = false;
    int playerDir = 0;

    Animator anim;
    EnemyController enemyController;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        enemyController = GetComponent<EnemyController>();
    }

    private void Update()
    {
        if (isArea && !anim.GetCurrentAnimatorStateInfo(0).IsName("BossA_NomalAttack"))
        {
            Invoke("OnFire", attackDelay);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            isArea = true;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player") // 플레이어 위치가 변경되면 방향 확인
        {
            playerDir = transform.position.x - collision.transform.position.x > 0 ? -1 : 1; // -1 왼쪽, 1 오른쪽
            isArea = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player") // 플레이어가 나가면 발사 중지
        {
            isArea = false;
            CancelInvoke("OnFire");
            anim.SetBool("doNomalAttack", false);
        }
    }

    void OnFire()
    {
        if (isArea)
        {
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x) * -playerDir, transform.localScale.y, transform.localScale.z);
            anim.SetBool("doNomalAttack", true);
        }
    }

    public void AttackEnd()
    {
        Debug.Log("attack End!");
        anim.SetBool("doNomalAttack", false);

        Invoke("OnFire", attackDelay);
    }
}
