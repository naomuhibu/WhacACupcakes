using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VolumeController : MonoBehaviour
{
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = gameObject.GetComponent<AudioSource>();

    }

    public void SoundSliderOnValueChange(float newSliderValue)
    {
        audioSource.volume = newSliderValue; //change the sound volume with stlider
    }
}
