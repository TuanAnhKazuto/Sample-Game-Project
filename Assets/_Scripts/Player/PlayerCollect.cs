using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayerCollect : MonoBehaviour
{
    [Header("Coin Settings")]
    public int coinScore = 0;
    public TextMeshProUGUI coinScoreText; // UI Text điểm của xu

    [Header("Gem Settings")]
    public int gemScore = 0;
    public TextMeshProUGUI gemScoreText; // UI Text điểm của kim cương

    [Header("Player Settings")]
    public Transform startingPosition;

    void Start()
    {
        UpdateCoinScoreText();
        UpdateGemScoreText();

        if (startingPosition == null)
        {
            startingPosition = transform;
        }
    }

    void Update()
    {
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

    public void CollectCoin(GameObject coin)
    {
        coinScore += 1;
        UpdateCoinScoreText();
        Destroy(coin);
    }

    public void CollectGem(GameObject gem)
    {
        gemScore += 1;
        UpdateGemScoreText();
        Destroy(gem);
    }

    private void UpdateCoinScoreText()
    {
        coinScoreText.text = $"<size=58> {coinScore}";
    }

    private void UpdateGemScoreText()
    {
        gemScoreText.text = $"<size=58> {gemScore}";
    }

    private void Die()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void ResetPosition()
    {
        transform.position = startingPosition.position;
    }

    public void ChangeScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}