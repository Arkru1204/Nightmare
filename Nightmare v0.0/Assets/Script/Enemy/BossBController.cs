using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBController : MonoBehaviour
{
    // 인스펙터
    [Header ("기본 설정")]
    public float maxSpeed = 1f;
    public float raycastDistance = 5f;
    public float attackDelay = 1f;

    [Header ("발사 관련")]
    public GameObject bullet;
    public float throwX = -4f;
    public GameObject muzzle1;
    public GameObject muzzle2;

    // 체크용
    bool isAttack1 = false;
    bool isAttack2 = false;

    // 발사 위치
    Vector3 newPos1;
    Vector3 newPos2;

    // 컴포넌트
    InArea inArea;

    // 발사2 텀
    float[] attackDelayArray = new float[] {1.5f, 1.0f, 0.5f, 1.5f, 0.5f, 1.0f, 0.5f};
    int attackDelayIndex = 0;

    void Awake()
    {
        inArea = GetComponentInChildren<InArea>();

        newPos1 = muzzle1.transform.position;
        newPos2 = muzzle2.transform.position;
    }

    private void Update()
    {
        if (inArea.getAggro() && !isAttack1) // 공격
        {
            isAttack1 = true;
            Invoke("Fire1", attackDelay);
        }

        if (inArea.getAggro() && !isAttack2) // 공격
        {
            isAttack2 = true;
            Invoke("Fire2", attackDelayArray[attackDelayIndex]);
        }
    }

    // ======================== 공격 함수 ========================
    void Fire1()
    {
        GameObject newGameObject = Instantiate(bullet) as GameObject;

        newGameObject.transform.position = newPos1;

        Rigidbody2D rbody = newGameObject.GetComponent<Rigidbody2D>();
        rbody.AddForce(new Vector2(throwX, 0), ForceMode2D.Impulse);

        isAttack1 = false;
    }

    void Fire2()
    {
        if (attackDelayIndex < 6)
            attackDelayIndex++;
        else
            attackDelayIndex = 0;


        GameObject newGameObject = Instantiate(bullet) as GameObject;

        newGameObject.transform.position = newPos2;

        Rigidbody2D rbody = newGameObject.GetComponent<Rigidbody2D>();
        rbody.AddForce(new Vector2(throwX, 0), ForceMode2D.Impulse);

        isAttack2 = false;
    }
}
