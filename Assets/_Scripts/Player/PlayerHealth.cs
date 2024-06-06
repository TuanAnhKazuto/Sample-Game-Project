using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    [Header("Health Settings")]
    public int maxHealth = 100;
    private int currentHealth;
    public Slider healthSlider;  // Để bỏ UI Slider cho thanh máu

    [Header("Coin Settings")]
    public int score = 0;
    public Text scoreText;  // Để bỏ Ui Text điểm cho đồng xu

    [Header("Gem Settings")]
    public int Gemscore = 0;
    public Text GscoreText; // Để bỏ Ui Text điểm cho đá quý

    [Header("Player Settings")]
    public Transform startingPosition;

    void Start()
    {
        currentHealth = maxHealth;
        healthSlider.maxValue = maxHealth;
        healthSlider.value = currentHealth;
        UpdateScoreText();
        if (startingPosition == null)
        {
            startingPosition = transform; 
        }
    }

    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            TakeDamage(20); //nhận sát thương khi chạm enemy
        }

        if (collision.gameObject.CompareTag("Trap"))
        {
            TakeDamage(50); //nhận sát thương khi chạm bẫy
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Coin"))
        {
            CollectCoin(other.gameObject);
        }
        if (other.gameObject.CompareTag("Gem"))
        {
            CollectGem(other.gameObject);
        }
    }

    private void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthSlider.value = currentHealth;

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void CollectCoin(GameObject coin)
    {
        score += 10; // chỉnh sửa điểm của đồng xu
        UpdateScoreText();
        Destroy(coin); // Destroy đồng xu object
    }

    public void CollectGem(GameObject gem)
    {
        score += 10;
        UpdateScoreText();
        Destroy(gem);
    }

    private void UpdateScoreText()
    {
        scoreText.text = "Score: " + score;
    }

    private void Die()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    
}
