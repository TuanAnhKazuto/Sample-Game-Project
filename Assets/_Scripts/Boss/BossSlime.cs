using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossSlime : MonoBehaviour
{
    [SerializeField] private Transform bulletTransform;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private float bullletSpeed = 5f;
    private bool seenPlayer = false;
    [SerializeField] private float moveSpeed = 3f;
    [SerializeField] private float jumpHight = 3f;
    [SerializeField] private float posLeft;
    [SerializeField] private Slider hpBoss;

    private void Start()
    {
        hpBoss.value = 200;
    }
    private void Update()
    {
        BossMove();
        if (transform.position.x <= posLeft)
        {
            moveSpeed = 0;
            jumpHight = 0;
            
            BossAttack();
            
        }
        if(hpBoss.value <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Arrow")
        {
            hpBoss.value -= 10;
        }
        
    }

    public void BossMove()
    {
        transform.Translate(Vector2.up * moveSpeed * Time.deltaTime);
        transform.Translate(Vector2.left * moveSpeed * Time.deltaTime);
        
    }

    public void BossAttack()
    {
        GameObject bullet = Instantiate(bulletPrefab, bulletTransform.position, Quaternion.identity);
        bullet.GetComponent<Rigidbody2D>().velocity = bulletTransform.right * bullletSpeed * -1;
    }
}
