using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnTimeout_DestroyMe : MonoBehaviour
{
    public float limitSec = 3;

    void Start()
    {
        Destroy(this.gameObject, limitSec);
    }
}
