using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InAreaFire : EnemyController
{
    public GameObject bullet;
    public float offsetY = 1;

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

            Debug.Log("in");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
            isArea = false;
    }

    IEnumerator OnFire(Vector2 targetPos)
    {
        while (isArea)
        {
            yield return new WaitForSeconds(2f);
            dir = transform.position.x - targetPos.x > 0 ? 1 : -1;
            anim.SetTrigger("doAttack");
        }
    }

    void Fire()
    {
        //Vector3 area = this.GetComponentInChildren<SpriteRenderer>().bounds.size;
        Vector3 newPos = this.transform.position;
        newPos.y += offsetY;

        GameObject newGameObject = Instantiate(bullet) as GameObject;
        newPos.z = -5;
        newGameObject.transform.position = newPos;
        newGameObject.transform.localScale = new Vector3(newGameObject.transform.localScale.y * dir, newGameObject.transform.localScale.y, newGameObject.transform.localScale.z);
    }
}
