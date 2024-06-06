using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeCoin : MonoBehaviour
{
    private int curCoin;

    public CoinText coinText;

    private void Start()
    {
        curCoin = 0;
        coinText.UpdateCoin(curCoin);
    }

    public void Take_Coin(int coin)
    {
        curCoin += coin;
        coinText.UpdateCoin(curCoin);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Coin")
        {
            Take_Coin(1);
            Destroy(other.gameObject);
        }
    }
}
