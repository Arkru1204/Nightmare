using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonStage : MonoBehaviour
{
    public int StageNum;

    void OnMouseDown()
    {
        SceneManager.LoadScene(StageNum);
    }
}
