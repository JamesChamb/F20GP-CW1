using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{

    private Image healthBar;
    public float barFill = 1.0f;

    void Start()
    {
        healthBar = GetComponent<Image>();
    }

    void Update()
    {
        healthBar.fillAmount = barFill;
        if (healthBar.fillAmount < 0.2f)
        {
            healthBar.color = Color.red;
        }
        else if (healthBar.fillAmount < 0.4f)
        {
            healthBar.color = Color.yellow;
        }
        else
        {
            healthBar.color = Color.green;
        }
    }
}
