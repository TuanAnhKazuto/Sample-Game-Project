using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] int maxHealth;
    int curHealth;
    [SerializeField] private GameObject gameOverPanel;
    public HealthBar healthBar;
    [SerializeField] private AudioSource gameOverSound;
    [SerializeField] private AudioSource mainSound;

    private void Start()
    {
        gameOverPanel.SetActive(false);
        Time.timeScale = 1;
        curHealth = maxHealth;

        healthBar.UpdateBar(curHealth, maxHealth);
    }

    public void TakeDamage(int damage)
    {
        curHealth -= damage;

        if(curHealth <= 0)
        {
            curHealth = 0;
            mainSound.Stop();
            gameOverSound.Play();
            gameOverPanel.SetActive(true);
            Destroy(this.gameObject);
            Time.timeScale = 0;
        }
        healthBar.UpdateBar(curHealth, maxHealth);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            TakeDamage(20);
        }
        if(collision.gameObject.tag == "Boss")
        {
            TakeDamage(50);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Trap")
        {
            TakeDamage(25);
        }
        if (collision.gameObject.tag == "Water")
        {
            TakeDamage(100);
        }
        if(collision.gameObject.tag == "BulletBoss")
        {
            TakeDamage(15);
            Destroy(collision.gameObject);
        }
    }
}