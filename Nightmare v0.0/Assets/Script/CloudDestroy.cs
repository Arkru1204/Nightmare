using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudDestroy : MonoBehaviour
{
    Rigidbody2D cloud;

    public float maxTime = 3f;

    void Start()
    {
        cloud = GetComponent<Rigidbody2D>();
        cloud.gravityScale = 0;
    }

    void FixedUpdate()
    {
        Invoke("OnCollisionEnter2D", maxTime);
    }

    void OnCollisionEnter2D(Collision2D collision)  // 충돌했을 때
    {
        // 만약 충돌한 것의 이름이 목표 오브젝트라면
        if (collision.gameObject.name == "PlayerB")
        {
            cloud.gravityScale = 1;
            // Destroy(this.gameObject);
        }
    }
}