using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InArea : MonoBehaviour
{
    bool aggro = false;
    bool isArea = false;
    int playerDir = 0;
    Vector3 exitPos;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            isArea = true;
            aggro = true;
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
            exitPos = collision.transform.position;
        }
    }

    public bool getAggro()
    {
        return aggro;
    }

    public bool getIsArea()
    {
        return isArea;
    }

    public int getPlayerDir()
    {
        return playerDir;
    }

    public Vector3 getExitPos()
    {
        return exitPos;
    }
}
