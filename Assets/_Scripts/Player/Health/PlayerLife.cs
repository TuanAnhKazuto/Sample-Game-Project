using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLife : MonoBehaviour
{
    [SerializeField] int maxHealht;
    int curHealht;

    public HealhtBar healhtBar;

    private void Start()
    {
        curHealht = maxHealht;

        healhtBar.UpdateBar(curHealht, maxHealht);
    }

    public void TakeDamage(int damage)
    {
        curHealht -= damage;

        if (curHealht <= 0)
        {
            Destroy(gameObject);
        }

        healhtBar.UpdateBar(curHealht, maxHealht);
    }


    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            TakeDamage(20);
        }
    }
}
