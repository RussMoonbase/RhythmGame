using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Jukebox : MonoBehaviour
{
    public static Jukebox inst { get; private set; }

    private float lastMeasuredBeat = 0f;
    public static UnityAction<float> OnBeat = delegate { };
    //public static UnityAction<float> OnBeatStartingSoon = delegate { }; // have a callback for a beat about to start, to generate the onscreen cues
    private float lastBeatCallback = float.NegativeInfinity;

    public AudioSource audioSource;
    public SongInfo[] possibleSongs;
    private SongInfo songToPlay;

    public float CurrentBeat { get; private set; } = 0f;

    private void Awake()
    {
        inst = this;

        songToPlay = possibleSongs[Random.Range(0, possibleSongs.Length)];
        audioSource.clip = songToPlay.song;
        audioSource.Play();
    }

    private void Update()
    {
        float measured = MeasureBeat();
        if (measured != lastMeasuredBeat)
        {
            lastMeasuredBeat = measured;
            CurrentBeat = measured;
        }
        else
        {
            float beatsPerSecond = songToPlay.bpm / 60f;
            float deltaBeat = Time.deltaTime * beatsPerSecond;
            CurrentBeat += deltaBeat;
        }
        //Debug.Log("Current Beat " + CurrentBeat);
        var floorBeat = Mathf.Floor(CurrentBeat);
        if (floorBeat > lastBeatCallback)
        {
            OnBeat(floorBeat);
            lastBeatCallback = floorBeat;
        }
    }

    private float MeasureBeat()
    {
        float beatsPerSecond = songToPlay.bpm / 60f;
        float seconds = (audioSource.timeSamples / (float)audioSource.clip.frequency - songToPlay.songStartTime);
        return seconds * beatsPerSecond;
    }
}
