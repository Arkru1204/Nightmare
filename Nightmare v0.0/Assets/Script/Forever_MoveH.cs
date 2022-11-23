using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Forever_MoveH : MonoBehaviour
{
    public float speed = 1;
    public bool up = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        if (up)
            this.transform.Translate(0, speed / 50, 0);
        else
            this.transform.Translate(speed / 50, 0, 0);
    }
}
