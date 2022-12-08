using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeSetactiveF : MonoBehaviour
{
    void Start()
    {
        Invoke("setFalse", 2.0f);
    }

    void setFalse()
    {
        this.gameObject.SetActive(false);
    }
}
