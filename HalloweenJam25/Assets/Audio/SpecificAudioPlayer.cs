using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecificAudioPlayer : MonoBehaviour
{
    [Header("Buttons")]
    [SerializeField] private RotatingButtonObjects leftBtn;
    [SerializeField] private RotatingButtonObjects rightBtn;
    [SerializeField] private PourButtonObject pourBtn;

    [Header("Audio Clips")]
    [SerializeField] private AudioClip gearClip;
    [SerializeField] private AudioClip pourClip;

    private AudioSource source;
    private void Start()
    {
        source = GetComponent<AudioSource>();

        leftBtn.OnButtonTriggerd += PlayGearSound;
        rightBtn.OnButtonTriggerd += PlayGearSound;
        pourBtn.OnButtonTriggerd += PlayPourSound;
    }
    private void OnDisable()
    {
        leftBtn.OnButtonTriggerd -= PlayGearSound;
        rightBtn.OnButtonTriggerd -= PlayGearSound;
        pourBtn.OnButtonTriggerd -= PlayPourSound;
    }

    private void PlayPourSound()
    {
        if (source == null || pourClip == null)
            return;

        source.clip = pourClip;
        source.Play();
    }

    private void PlayGearSound()
    {
        if (source == null || gearClip == null)
            return;

        source.PlayOneShot(gearClip);
    }
}
