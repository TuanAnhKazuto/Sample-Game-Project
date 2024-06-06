using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class Coin : MonoBehaviour
{
    private int curCoins;
    public CoinText cointext;

    private void Start()
    {
        curCoins = 0;
        cointext.UpdateCoin(curCoins);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Coin")
        {
            /*PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.CollectCoin(gameObject);
            }*/

            TakeCoin(1);

            Destroy(other.gameObject);

        }
    }

    public void TakeCoin(int numCoin)
    {
        curCoins += numCoin;
        cointext.UpdateCoin(curCoins);
    } 
    
}
