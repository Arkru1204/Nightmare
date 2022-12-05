using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudDestroy : MonoBehaviour
{
    Rigidbody2D cloud;

    void Start()
    {
        cloud = GetComponent<Rigidbody2D>();
        cloud.gravityScale = 0;
    }

    void OnCollisionEnter2D(Collision2D collision)  // 충돌했을 때
    {
        if (collision.gameObject.name == "PlayerB")
            cloud.gravityScale = 1;

        else if (collision.gameObject.name != "PlayerB")
            Destroy(this.gameObject);
    }
}