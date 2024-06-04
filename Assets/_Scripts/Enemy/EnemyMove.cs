using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 3f;
    [SerializeField] private float jumpHight = 2.5f;
    [SerializeField] private float left;
    [SerializeField] private float right;
    private int moveDiretion = 1;

    private void Update()
    {
        transform.Translate(Vector2.up * jumpHight * Time.deltaTime);

        transform.Translate(Vector2.right * moveSpeed * moveDiretion * Time.deltaTime);
        Vector2 scale = transform.localScale;
        if(transform.position.x <= left)
        {
            moveDiretion = 1;
            scale.x = 1;
        }
        else if(transform.position.x >= right)
        {
            moveDiretion = -1;
            scale.x = -1;
        }
        transform.localScale = scale;

    }
    private void OnTriggerEnter2D(Collider2D Touch)
    {
        if (Touch.CompareTag("Player"))
        {
            Destroy(Touch.gameObject);
        }
    }
}
