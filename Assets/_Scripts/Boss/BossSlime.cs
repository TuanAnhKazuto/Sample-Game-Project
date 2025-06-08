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
    [SerializeField] private float jumpSpeed = 1f;
    [SerializeField] private float posLeft;
    [SerializeField] private Slider hpBoss;
    [SerializeField] private AudioSource arrowHit;

    private bool canAttack = true;
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
            jumpSpeed = 0;
            if (canAttack)
            {
                StartCoroutine(BossAttackRoutine());
            }
        }
        if (hpBoss.value <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Arrow")
        {
            arrowHit.Play();
            hpBoss.value -= 5;
            Destroy(collision.gameObject);
        }

    }

    public void BossMove()
    {
        transform.Translate(Vector2.up * jumpSpeed * Time.deltaTime);
        transform.Translate(Vector2.left * moveSpeed * Time.deltaTime);
    }

    public void BossAttack()
    {
        GameObject bullet = Instantiate(bulletPrefab, bulletTransform.position, Quaternion.identity);
        bullet.GetComponent<Rigidbody2D>().linearVelocity = bulletTransform.right * bullletSpeed * -1;
    }
    private IEnumerator BossAttackRoutine()
    {
        canAttack = false;
        for (int i = 0; i < 5; i++)
        {
            BossAttack();
            yield return new WaitForSeconds(0.5f); // Thời gian giữa các lần bắn, bạn có thể điều chỉnh theo ý muốn
        }
        yield return new WaitForSeconds(1f); // Chờ 3 giây trước khi tiếp tục bắn
        canAttack = true;
    }
}
