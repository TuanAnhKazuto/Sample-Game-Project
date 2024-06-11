using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DiamondText : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI diamondText;

    public void UpdateDiamond(int curDiamond)
    {
        diamondText.text = curDiamond.ToString();
    }
}