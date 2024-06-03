using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    void Start()
    {
        Destroy(this.gameObject, 2);
    }

    // Update is called once per frame
    void Update()
    {
        IsMovingRight();
        ChangeScale();   
    }

    private void ChangeScale()
    {
        Vector2 scale = transform.localScale;
        if (isMovingRight)
        {
            scale.x = 1;
        }
        else
        {
            scale.x = -1;
        }
        transform.localScale = scale;
    }

    private bool isMovingRight = true;
    public void IsMovingRight()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            isMovingRight = true;
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            isMovingRight= false;
        }
    }
}
