using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public PlayerMain player;
    public int hp = 3;
    public GameObject[] HpUI;
    public Animator[] HpAnim;

    public void HpDown()
    {
        hp--;
        if (hp == 0)
            StartCoroutine(Dead());

        HpAnim[hp].SetBool("isHpDestroy", true);
        StartCoroutine(DestroyHp(hp)); // 매개변수 전달을 위해서 코루틴 사용
    }

    IEnumerator DestroyHp(int i)
    {
        yield return new WaitForSeconds(0.8f); // 0.8초 뒤 독립시행
        HpUI[i].SetActive(false);
    }

    IEnumerator Dead()
    {
        yield return new WaitForSeconds(0.35f);
        Time.timeScale = 0;
    }
}
