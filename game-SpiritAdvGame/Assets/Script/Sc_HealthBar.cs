using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Sc_HealthBar : MonoBehaviour
{
    public float currentHP;
    public int maxHP = 200;
    public Image sliderHP;

    void Start()
    {
        currentHP = maxHP;
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            currentHP -= 5;
        }
        UpdateSlider();
    }

    void UpdateSlider()
    {
        sliderHP.fillAmount = currentHP / maxHP;
    }
}
