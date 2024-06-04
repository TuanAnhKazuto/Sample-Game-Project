using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSlime : MonoBehaviour
{
    [SerializeField] private Transform bulletTransform;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private float bullletSpeed = 5f;
    private bool seenPlayer = false;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            seenPlayer = true;
            BossAttack();
        }
    }

    public void BossAttack()
    {
        if(seenPlayer)
        {
            GameObject bullet = Instantiate(bulletPrefab, bulletTransform.position, Quaternion.identity);
            bullet.GetComponent<Rigidbody2D>().velocity = bulletTransform.right * bullletSpeed * -1;

        }

    }

    /*IEnumerator FireDelay()
    {
        do
        {
            BossAttack();
            yield return new WaitForSeconds(0.3f);

        } while (true);
    }*/
}
