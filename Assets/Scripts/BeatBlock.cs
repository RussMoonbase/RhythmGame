using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatBlock : MonoBehaviour
{
    public float screenSize = 100;
    public float distanceBetweenBeats = 200;

    public int BeatLength { get; private set; }
    public float StartOnBeat { get; set; } = 0f;
    public float EndOnBeat
    {
        get
        {
            return StartOnBeat + BeatLength;
        }
    }
    private RectTransform rt;

    private void Awake()
    {
        rt = transform as RectTransform;
        SetBeatLength(0);
    }

    private void Update()
    {
        UpdatePosition();
    }

    public void UpdatePosition()
    {
        var pos = rt.anchoredPosition;
        pos.x = (StartOnBeat - Jukebox.inst.CurrentBeat) * distanceBetweenBeats;
        rt.anchoredPosition = pos;
    }

    public void SetBeatLength(int length)
    {
        BeatLength = length;
        rt.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, Mathf.Max(screenSize, distanceBetweenBeats * BeatLength));
    }
}
