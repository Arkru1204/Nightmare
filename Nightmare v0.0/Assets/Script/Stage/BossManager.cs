using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossManager : MonoBehaviour
{
    public BossMain boss;
    public GameObject bossUI;
    public Scrollbar bossHpBar;

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
    }
}
