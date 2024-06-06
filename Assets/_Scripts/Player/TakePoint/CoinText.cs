using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CoinText : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI coinText;

    public void UpdateCoin(int curCoin) 
    {
        coinText.text = curCoin.ToString();
    }
}
