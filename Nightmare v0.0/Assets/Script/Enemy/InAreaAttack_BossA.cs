using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InAreaAttack_BossA : MonoBehaviour
{
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
            StartCoroutine(OnFire(collision.transform.position));
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
            isArea = false;
    }

    IEnumerator OnFire(Vector2 targetPos)
    {
        while (isArea)
        {
            yield return new WaitForSeconds(1.7f);
            anim.SetTrigger("doNomalAttack");
        }
    }

    void Fire()
    {
        Debug.Log("플레이어 dir은 " + playerDir + ", 에너미 dir은" + enemyController.getDir());
        if (playerDir != enemyController.getDir()) // 플레이어 위치와 에너미 진행 방향이랑 다르면
        {
            enemyController.Turn();

            if (enemyController.getThink() == 0) // 멈춰 있다면
                enemyController.lookBack();
        }

        ////Vector3 area = this.GetComponentInChildren<SpriteRenderer>().bounds.size;
        //Vector3 newPos = this.transform.position;
        //newPos.y += offsetY;

        //GameObject newGameObject = Instantiate(bullet) as GameObject;
        ////newPos.z = -5;
        //newGameObject.transform.position = newPos;
        //newGameObject.transform.localScale = new Vector3(newGameObject.transform.localScale.y * playerDir, newGameObject.transform.localScale.y, newGameObject.transform.localScale.z);

        //Rigidbody2D rbody = newGameObject.GetComponent<Rigidbody2D>();
        //rbody.AddForce(new Vector2(throwX * playerDir, throwY), ForceMode2D.Impulse);
    }
}
