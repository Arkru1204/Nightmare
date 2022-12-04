using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEncounter : MonoBehaviour
{
    public GameObject bossUI;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            bossUI.SetActive(true);
            Debug.Log("보스 인카운터!");

            Destroy(this.gameObject);
        }
    }
}
