using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEncounter : MonoBehaviour
{
    public GameObject bossUI;
    public GameObject lineText;
    public AudioSource audioSource;

    private void Awake()
    {
        AudioSource audioSource = gameObject.AddComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            bossUI.SetActive(true);
            lineText.SetActive(true);
            audioSource.Play();
            Debug.Log("보스 인카운터!");

            Destroy(this.gameObject);
        }
    }
}
