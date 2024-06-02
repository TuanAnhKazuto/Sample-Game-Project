using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Up_Down : MonoBehaviour
{
    [SerializeField] private float speed = 3f;
    [SerializeField] private float top;
    [SerializeField] private float back = 1f;
    private int moveDiretion = 1;

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.up * moveDiretion * speed * Time.deltaTime);
        if(transform.position.y >= top)
        {
            moveDiretion = -1;
        }
        else if(transform.position.y <= top - back)
        {
            moveDiretion = 1; 
        }
    }
}
