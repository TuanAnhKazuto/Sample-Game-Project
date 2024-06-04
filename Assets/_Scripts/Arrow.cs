using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    [SerializeField] private float arrowSpeed = 25f;
    private bool isMovingRight;

    void Start()
    {
        Destroy(this.gameObject, 2); // Destroy the arrow after 2 seconds
    }

    void Update()
    {
        MoveArrow();
    }

    public void Initialize(bool movingRight)
    {
        isMovingRight = movingRight;
        ChangeScale();
    }

    private void MoveArrow()
    {
        float direction = isMovingRight ? 1 : -1;
        transform.Translate(Vector2.right * arrowSpeed * Time.deltaTime * direction);
    }

    private void ChangeScale()
    {
        Vector3 scale = transform.localScale;
        scale.x = isMovingRight ? Mathf.Abs(scale.x) : -Mathf.Abs(scale.x);
        transform.localScale = scale;
    }

    private void OnTriggerEnter2D(Collider2D hit)
    {
        if (hit.CompareTag("Enemy"))
        {
            Destroy(hit.gameObject); // Destroy the enemy
            Destroy(gameObject); // Destroy the arrow
        }
    }
}