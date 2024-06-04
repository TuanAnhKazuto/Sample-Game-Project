using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerATK : MonoBehaviour
{
    private void Start()
    {
    }
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D hit)
    {
        if (hit.CompareTag("Enemy"))
        {
            Destroy(hit.gameObject);//destroy the enemy
            Destroy(gameObject);//destroy arrow
        }
    }
}
