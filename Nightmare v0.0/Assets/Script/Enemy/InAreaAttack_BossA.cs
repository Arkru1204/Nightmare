using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InAreaAttack_BossA : MonoBehaviour
{
    public float attackDelay = 1.5f;

    bool isArea = false;
    int playerDir = 0;

    Animator anim;
    EnemyController enemyController;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        enemyController = GetComponent<EnemyController>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            isArea = true;
            OnFire(collision.transform.position);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player") // 플레이어 위치가 변경되면 방향 확인
            playerDir = transform.position.x - collision.transform.position.x > 0 ? -1 : 1; // -1 왼쪽, 1 오른쪽
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player") // 플레이어가 나가면 발사 중지
        {
            isArea = false;
            CancelInvoke("OnFire");
        }
    }

    void OnFire(Vector2 targetPos)
    {
        if (isArea)
        {
            if (playerDir != enemyController.getDir()) // 플레이어 위치와 에너미 진행 방향이랑 다르면
            {
                enemyController.Turn();

                if (enemyController.getThink() == 0) // 멈춰 있다면
                    enemyController.lookBack();
            }

            anim.SetTrigger("doNomalAttack");

            Invoke("OnFire", attackDelay);
        }
    }
}
