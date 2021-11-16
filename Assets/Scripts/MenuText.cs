using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MenuText : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI text;
    private string originalValue;

    void Awake() =>
        originalValue = text.text;

    public void SetPercentage(float value)
    {
        text.text = originalValue + Mathf.Round(value * 100) + "%";
    }
}
