using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI valueText;
    [SerializeField] private Image healthFill;

    public void UpdateBar(int curHealth, int maxHealth)
    {
        healthFill.fillAmount = (float)curHealth / (float) maxHealth;
        valueText.text = curHealth.ToString() + "/" + maxHealth;
    }
}
