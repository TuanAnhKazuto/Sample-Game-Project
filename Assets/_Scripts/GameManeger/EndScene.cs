using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndScene : MonoBehaviour
{
    [SerializeField] private GameObject victoryPanel;

    private void Start()
    {
        Time.timeScale = 1;
        victoryPanel.SetActive(false);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            Time.timeScale = 0;
            victoryPanel.SetActive(true);
        }
    }
}
