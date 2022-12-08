using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossBManager : MonoBehaviour
{
    public BossBMain boss;
    public GameObject bossUI;
    public Scrollbar bossHpBar;
    public GameObject[] bossArea;

    float setHp = 0;

    private void Start()
    {
        setHp = 100f / boss.getHp() / 100f;
    }

    public void BossHpDown()
    {
        Debug.Log(bossHpBar.size + " - " + setHp);
        bossHpBar.size -= setHp;
        Debug.Log(bossHpBar.size);
    }

    public void BossDead()
    {
        bossUI.SetActive(false);
        bossArea[0].SetActive(true);
        bossArea[1].SetActive(true);
    }
}
