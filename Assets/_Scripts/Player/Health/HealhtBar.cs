using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HealhtBar : MonoBehaviour
{
    [SerializeField] private Image fillBar;
    [SerializeField] private TextMeshProUGUI valueText;

    public void UpdateBar(int curValue, int maxValue)
    {
        fillBar.fillAmount = (float)curValue / (float)maxValue;
        valueText.text = curValue.ToString() + "/" + maxValue.ToString();
    }
}
