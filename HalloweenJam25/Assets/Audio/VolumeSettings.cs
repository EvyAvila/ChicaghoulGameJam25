using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UIElements;

public class VolumeSettings : MonoBehaviour
{
    [SerializeField] private AudioMixer mixer;
    [SerializeField] private Slider slider;

    private void Awake()
    {

    }

    public void SetSFXVolume(float v)
    {
        mixer.SetFloat("Vlume", v);
    }
}
