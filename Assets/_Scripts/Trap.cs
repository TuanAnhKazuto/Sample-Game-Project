using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
    [SerializeField] private float speed = 3;
    [SerializeField] private float top;
    [SerializeField] private float bottom;
    private int moveDiretion = 1;

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.up * moveDiretion * speed * Time.deltaTime);
        if(transform.position.y >= top)
        {
            moveDiretion = -1;
        }
        else if(transform.position.y <= bottom)
        {
            moveDiretion = 1; 
        }
    }
}
