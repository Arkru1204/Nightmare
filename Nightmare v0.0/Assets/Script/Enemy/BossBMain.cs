using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BossBMain : MonoBehaviour
{
    public BossBManager bossBManager;
    public GameObject gird;

    public int hp = 56;
    public float bouncPower = 2f;
    public float invulnTime = 0.5f;

    bool isStart = false;

    Rigidbody2D rigid;
    Animator anim;
    Tilemap tilemap;
    InArea inArea;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        tilemap = GetComponent<Tilemap>();
        inArea = GetComponentInChildren<InArea>();
    }

    private void FixedUpdate()
    {
        if (inArea.getAggro() && !isStart)
        {
            isStart = true;
            InvokeRepeating("OnHit", 2f, 2.0f);
        }
    }

    void OnHit()
    {
        hp--;
        if (hp == 0)
            Dead();

        bossBManager.BossHpDown();
    }

    void Dead()
    {
        bossBManager.BossDead();
        Destroy(gird);
    }

    public int getHp()
    {
        return hp;
    }
}
