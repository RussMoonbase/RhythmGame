using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Jukebox : MonoBehaviour
{
    public static Jukebox inst { get; private set; }

    public static UnityAction<float> OnBeat = delegate { };
    public static UnityAction<float> OnBeatStartingSoon = delegate { }; // have a callback for a beat about to start, to generate the onscreen cues
    private float lastBeatCallback = float.NegativeInfinity;
    private float lastBeatStartingSoonCallback = float.NegativeInfinity;

    public AudioSource audioSource;
    public SongInfo songToPlay;
    public float beatLeadTime = .25f; // how long OnBeatStartingSoon is called before OnBeat
    // todo maybe have an offset 

    public float CurrentBeat
    {
        get
        {
            float beatsPerSecond = songToPlay.bpm / 60f;
            float seconds = (audioSource.timeSamples / (float)audioSource.clip.frequency - songToPlay.songStartTime);
            return seconds * beatsPerSecond;
        }
    }

    private void Awake()
    {
        inst = this;

        audioSource.clip = songToPlay.song;
        audioSource.Play();
    }

    private void Update()
    {
        Debug.Log("Current Beat " + CurrentBeat);
    }
}
