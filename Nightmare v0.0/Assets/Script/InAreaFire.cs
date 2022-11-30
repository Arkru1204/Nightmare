using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InAreaFire : MonoBehaviour
{
    public GameObject bullet;

    public float throwX = 4;
    public float throwY = 0;
    public float offsetY = 0;

    bool isArea = false;
    int dir = 0;

    Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
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
            dir = transform.position.x - collision.transform.position.x > 0 ? -1 : 1;
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
            yield return new WaitForSeconds(3f);
            anim.SetTrigger("doAttack");
        }
    }

    void Fire()
    {
        //Vector3 area = this.GetComponentInChildren<SpriteRenderer>().bounds.size;
        Vector3 newPos = this.transform.position;
        newPos.y += offsetY;

        GameObject newGameObject = Instantiate(bullet) as GameObject;
        //newPos.z = -5;
        newGameObject.transform.position = newPos;
        newGameObject.transform.localScale = new Vector3(newGameObject.transform.localScale.y * dir, newGameObject.transform.localScale.y, newGameObject.transform.localScale.z);

        Rigidbody2D rbody = newGameObject.GetComponent<Rigidbody2D>();
        rbody.AddForce(new Vector2(throwX * dir, throwY), ForceMode2D.Impulse);
    }
}
