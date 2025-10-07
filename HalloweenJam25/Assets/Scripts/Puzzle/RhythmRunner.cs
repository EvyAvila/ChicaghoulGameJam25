using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class RhythmRunner : MonoBehaviour
{
    /// <summary>
    /// BPM of the track, manually
    /// </summary>
    [SerializeField] private int BPM;
    private double dspTime;
    private double songPos;
    private float beatInterval;

    [SerializeField] private AudioClip CountInClip;
    [SerializeField] private AudioClip Song;

    /// <summary>
    /// Note divisions to track: Quarter, Eighth, etc.
    /// </summary>
    [SerializeField] private NoteDivision intervals;


    /// <summary>
    /// Toggle to count beats
    /// </summary>
    [SerializeField]private bool countBeats;

    /// <summary>
    /// Current beat counted on Eigths
    /// </summary>
    private double dspBeat;
    private double currentBeat;
    private float totalBeats;

    /// <summary>
    /// Audio source of the music
    /// </summary>
    private AudioSource source;


    //Events
    public event Action OnBeatIncrement;

    private void Start()
    {
        currentBeat = 0.5f;
        totalBeats = Song.length * (BPM / 60.0f);

        source = GetComponent<AudioSource>();
        source.clip = CountInClip;

        beatInterval = intervals.GetIntervalLength(BPM) * 2;
    }

    public void StartIntro()
    {
        //countBeats = true;
        //source.clip = CountInClip;
        //source.PlayOneShot(CountInClip);
        //source.clip = Song;
        //source.PlayDelayed(CountInClip.length);

        StartCoroutine(IntroSequence());
    }

    private IEnumerator IntroSequence()
    {
        source.clip = Song;
        source.PlayOneShot(CountInClip);
        source.PlayDelayed(CountInClip.length);
        yield return new WaitForSeconds(CountInClip.length);
        countBeats = true;
        dspTime = AudioSettings.dspTime;
        yield return null;
    }
    public void StopCounting()
    {
        countBeats = false;
    }
    private void CountOnDivisions()
    {
        songPos = (AudioSettings.dspTime - dspTime);
        dspBeat = (songPos / beatInterval)+1;

        float elapsed = (source.timeSamples /
                (source.clip.frequency * intervals.GetIntervalLength(BPM)));

        intervals.CheckForNewInterval(elapsed);
    }

    //Is triggered from an Event
    public void IncrementBeat()
    {
        currentBeat += 0.5;
        OnBeatIncrement?.Invoke();
    }

    public void ResetBeat()
    {
        if (currentBeat <= 1)
            return;

        currentBeat = .5f;

        Debug.Log("Song restarted");
    }

    public double GetCurrentDspBeat()
    {
        return dspBeat;
    }
    public double GetCurrentBeat()
    {
        return currentBeat;
    }
    private void Update()
    {
        if (countBeats)
        {
            CountOnDivisions();

            if (!source.isPlaying)
                countBeats = false;
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (!countBeats)
        {
            StartIntro();
        }
    }
}