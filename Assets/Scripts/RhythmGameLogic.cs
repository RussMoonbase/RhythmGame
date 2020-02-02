using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RhythmGameLogic : MonoBehaviour
{
    public RectTransform beatTrack;
    public GameObject beatBlockPrefab;

    private int lastBeatIndex = -1;
    private BeatBlock lastBlock = null;

    public float beatLeadTime = 12; // how long OnBeatStartingSoon is called before OnBeat
    // todo maybe have a calibrated offset 

    public enum BeatBit { None, Single, Long };
    // the beat pattern is going to come as a series of 0s and 1s in bytes
    // probably care about it in pairs, so like 101010 is 3 beats in sequence and 101110 is one beat and one long beat
    // and broken into 2 byte sections????? i guess?

    List<BeatBit> toPlay = new List<BeatBit>();

    private void Awake()
    {
        // TODO get the beat bits from somewhere
        var choices = new BeatBit[] { BeatBit.None, BeatBit.Single, BeatBit.Long, BeatBit.Long };
        for (int i = 0; i < 100; i++)
        {
            toPlay.Add(Util.RandomChoose(choices));
        }
    }

    private void Update()
    {
        // while?
        if (Jukebox.inst.CurrentBeat + beatLeadTime > lastBeatIndex)
        {
            AdvanceBeatIndex();
        }
    }

    private void AdvanceBeatIndex()
    {
        lastBeatIndex++;
        if (lastBeatIndex < toPlay.Count)
        {
            var currentBeat = toPlay[lastBeatIndex];
            var lastBeat = (lastBeatIndex > 0) ? toPlay[lastBeatIndex - 1] : BeatBit.None;
            if (currentBeat != BeatBit.None)
            {
                if (lastBeat == BeatBit.Long)
                {
                    // make the last block longer
                    lastBlock.SetBeatLength(lastBlock.BeatLength + 1);
                }
                else
                {
                    // make a new block
                    lastBlock = Instantiate(beatBlockPrefab, beatTrack).GetComponent<BeatBlock>();
                    lastBlock.StartOnBeat = lastBeatIndex;
                    lastBlock.UpdatePosition();
                }
            }
            else
            {
                lastBlock = null;
            }
        }
        else
        {
            lastBeatIndex = toPlay.Count; // the end
        }
    }
}
