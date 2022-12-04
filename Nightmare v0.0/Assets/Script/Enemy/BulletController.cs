using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public GameObject newPrefab;

    public float throwX = 4;
    public float throwY = 0;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        CreatePrefab();
        Destroy(this.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player Attack")
            turn(collision.transform.position);
    }

    void turn(Vector2 targetPos)
    {
        gameObject.layer = 15;
        transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);

        int dir = transform.position.x - targetPos.x > 0 ? 1 : -1;
        Rigidbody2D rbody = GetComponent<Rigidbody2D>();
        rbody.velocity = new Vector2(0, rbody.velocity.y);
        rbody.AddForce(new Vector2(throwX * dir, throwY), ForceMode2D.Impulse);
    }

    void CreatePrefab()
    {
        Vector3 area = GetComponent<SpriteRenderer>().bounds.size;

        Vector3 newPos = this.transform.position;
        newPos.x += Random.Range(-area.x / 2, area.x / 2);
        newPos.y += Random.Range(-area.y / 2, area.y / 2);

        GameObject newGameObject = Instantiate(newPrefab) as GameObject;
        newGameObject.transform.position = newPos;
    }
}
