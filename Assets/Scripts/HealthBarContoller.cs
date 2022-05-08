using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarContoller : MonoBehaviour
{
    public Slider slider;
    
    public void InitializeHealthBar(float hp) {
        slider.maxValue = hp;
        slider.value = hp;
    }

    public void updateHealthBar(float hp) {
        slider.value = hp;
    }
}
