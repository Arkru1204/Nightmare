using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public PlayerMain player;
    public int hp = 3;

    public GameObject[] hpUI;
    public Animator[] hpAnim;
    public GameObject restartButton;

    public void HpDown()
    {
        hp--;
        if (hp < 1)
        {
            player.OnDead();
            StartCoroutine(Dead());
        }
        if (hp < 0) // 인덱스 참조 오류 방지용
            return;

        hpAnim[hp].SetBool("isHpDestroy", true);
        StartCoroutine(DestroyHp(hp)); // 매개변수 전달을 위해서 코루틴 사용
    }

    public void HpUp()
    {
        if (hp < 3)
        {
            hp++;
            hpUI[hp - 1].SetActive(true);
        }
    }

    IEnumerator DestroyHp(int i)
    {
        yield return new WaitForSeconds(0.7f); // 0.8초 뒤 독립시행
        hpUI[i].SetActive(false);
    }

    IEnumerator Dead()
    {
        yield return new WaitForSeconds(0.35f);
        restartButton.SetActive(true);
        //Time.timeScale = 0;
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
