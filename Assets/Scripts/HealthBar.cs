using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider slider;
    public Gradient gradient;
    public Image fill;

    public void SetHealth(float health)
    {
        slider.value = health;
        fill.color = gradient.Evaluate(1f);
    }

    public void SetMaxHealth(float maxHealth)
    {
        slider.maxValue = maxHealth;
        slider.value = maxHealth;

        //only colours your health bar
        fill.color = gradient.Evaluate(slider.normalizedValue);
    }
}
