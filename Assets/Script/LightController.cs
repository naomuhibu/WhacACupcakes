using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LightController : MonoBehaviour
{
    public Light sceneLight;
    public Slider slider;

    void Update()
    {
        sceneLight.intensity = slider.value; //change the screen intensity with stlider
    }
}
