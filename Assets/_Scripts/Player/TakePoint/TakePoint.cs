using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakePoint : MonoBehaviour
{
    private int curCoin;
    private int curDiamond;

    public CoinText coinText;
    public DiamondText diamondText;
    [SerializeField] private AudioSource takeCoinSound;
    [SerializeField] public AudioSource takeDiamondSound;

    private void Start()
    {
        curCoin = 0;
        coinText.UpdateCoin(curCoin);

        curDiamond = 0;
        diamondText.UpdateDiamond(curDiamond);
    }

    public void Take_Coin(int coin)
    {
        curCoin += coin;
        coinText.UpdateCoin(curCoin);
    }

    public void TakeDiamond(int diamond)
    {
        curDiamond += diamond;
        diamondText.UpdateDiamond(curDiamond);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Coin")
        {
            takeCoinSound.Play();
            Take_Coin(1);
            Destroy(other.gameObject);
        }
        if(other.gameObject.tag == "Diamond")
        {
            takeDiamondSound.Play();
            TakeDiamond(1);
            Destroy(other.gameObject);
        }
    }
}