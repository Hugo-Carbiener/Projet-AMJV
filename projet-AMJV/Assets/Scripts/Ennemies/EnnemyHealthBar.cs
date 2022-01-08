using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class EnnemyHealthBar : MonoBehaviour
{
    [SerializeField]
    private Health health;
    [SerializeField]
    private Slider slider;

    private void Awake()
    {
        if(!health) health = transform.parent.GetComponent<Health>();
        if (!slider) slider = GameObject.Find("Slider").GetComponent<Slider>();
    }

    private void Start()
    {
        health.OnHealthChange += UpdateSlider;
        slider.maxValue = health.getMaxHealth();
        slider.value = health.getHealth();
        slider.minValue = 0;
    }

    private void UpdateSlider()
    {
        slider.value = health.getHealth();
        //Debug.Log("Health was updated");
    }

}
