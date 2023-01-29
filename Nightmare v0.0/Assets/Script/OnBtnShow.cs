using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnBtnShow : MonoBehaviour
{
    public GameObject showObject;

    void Awake()
    {
        showObject.SetActive(false);
    }

    private void OnMouseDown()
    {
        showObject.SetActive(true);
    }
}
