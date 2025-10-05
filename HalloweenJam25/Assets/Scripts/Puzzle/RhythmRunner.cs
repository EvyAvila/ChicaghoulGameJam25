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

    /// <summary>
    /// The steps
    /// </summary>
    [SerializeField] private NoteDivision[] intervals;

    /// <summary>
    /// Toggle to count beats
    /// </summary>
    [SerializeField]private bool countBeats;

    /// <summary>
    /// Audio source of the music
    /// </summary>
    private AudioSource source;

    private void Start()
    {
        source = GetComponent<AudioSource>();
    }

    public void StartCounting()
    {
        countBeats = true;
        source.Play();
    }

    public void StopCounting()
    {
        countBeats = false;
    }
    private void CountOnDivisions()
    {
        foreach (NoteDivision item in intervals)
        {
            float elapsed = (source.timeSamples /
                (source.clip.frequency * item.GetIntervalLength(BPM)));

            item.CheckForNewInterval(elapsed);
        }
    }

    private void Update()
    {
        if (countBeats)
        {
            CountOnDivisions();
        }
    }


    public void Quarter()
    {
        Debug.Log("Quarter");
    }

    public void Eigth()
    {
        Debug.Log("8th");
    }

    public void Sixteenth()
    {
        Debug.Log("16th");
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (!countBeats)
        {
            StartCounting();
        }
    }
}