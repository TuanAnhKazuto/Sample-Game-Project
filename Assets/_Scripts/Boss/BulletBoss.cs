using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBoss : MonoBehaviour
{
    void Start()
    {
        Destroy(this.gameObject, 5); 
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Arrow")
        {
            Destroy(this.gameObject);
            Destroy(collision.gameObject);
        }
    }
}
