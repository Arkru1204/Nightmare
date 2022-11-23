using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InAreaFire : MonoBehaviour
{
    public GameObject bullet;

    bool isArea = false;



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            isArea = true;
            StartCoroutine(OnFire(collision.transform.position));
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
            isArea = false;
    }

    IEnumerator OnFire(Vector2 targetPos)
    {
        while (!isArea)
        {
            yield return new WaitForSeconds(1f);
            int dir = transform.position.x - targetPos.x > 0 ? 1 : -1;
        }
    }
}
