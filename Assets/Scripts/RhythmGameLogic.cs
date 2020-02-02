using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RhythmGameLogic : MonoBehaviour
{
    public RhythmGameLogic inst { get; private set; }
    public RectTransform beatTrack;
    public KeyCode buttonToPress = KeyCode.Z;
    public GameObject beatBlockPrefab;

    private int lastInstantiatedBeat = -1;
    private BeatBlock lastInstantiatedBlock = null;
    private Queue<BeatBlock> blocks = new Queue<BeatBlock>();

    public int initialEmptyBeats = 8;
    public float beatLeadTime = 12; // how long OnBeatStartingSoon is called before OnBeat
    // todo maybe have a calibrated offset 
    public float deleteAfter = 20; // delete after this many beats

    private BeatBlock currentBlock = null;

    // the beat pattern is going to come as a series of 0s and 1s in bytes
    // probably care about it in pairs, so like 101010 is 3 beats in sequence and 101110 is one beat and one long beat
    // and broken into 2 byte sections????? i guess?

    List<BeatBit> toPlay = new List<BeatBit>();

    private void Awake()
    {
        inst = this;
    }

    private void Start()
    {
        // TODO get the beat bits from somewhere
        for (int i = 0; i < initialEmptyBeats; i++)
        {
            toPlay.Add(BeatBit.None);
        }
        var choices = new BeatBit[] { BeatBit.None, BeatBit.Single, BeatBit.Long, BeatBit.Long };
        for (int i = 0; i < 100; i++)
        {
            toPlay.Add(Util.RandomChoose(choices));
        }
    }

    private void Update()
    {
        // update input for the tracks
        if (null != currentBlock)
        {
            
        }
        else
        {

        }

        // instantiation
        while (Jukebox.inst.CurrentBeat + beatLeadTime > lastInstantiatedBeat)
        {
            AdvanceBeatIndex();
        }

        // cleanup
        while (blocks.Count > 0 && Jukebox.inst.CurrentBeat > blocks.Peek().EndOnBeat + deleteAfter)
        {
            var block = blocks.Dequeue();
            Destroy(block.gameObject);
        }
    }

    private void AdvanceBeatIndex()
    {
        lastInstantiatedBeat++;
        if (lastInstantiatedBeat < toPlay.Count)
        {
            var currentBeat = toPlay[lastInstantiatedBeat];
            var lastBeat = (lastInstantiatedBeat > 0) ? toPlay[lastInstantiatedBeat - 1] : BeatBit.None;
            if (currentBeat != BeatBit.None)
            {
                if (lastBeat == BeatBit.Long)
                {
                    // make the last block longer
                    lastInstantiatedBlock.SetBeatLength(lastInstantiatedBlock.BeatLength + 1);
                }
                else
                {
                    // make a new block
                    lastInstantiatedBlock = Instantiate(beatBlockPrefab, beatTrack).GetComponent<BeatBlock>();
                    lastInstantiatedBlock.StartOnBeat = lastInstantiatedBeat;
                    lastInstantiatedBlock.UpdatePosition();
                    blocks.Enqueue(lastInstantiatedBlock);
                }
            }
            else
            {
                lastInstantiatedBlock = null;
            }
        }
        else
        {
            lastInstantiatedBeat = toPlay.Count; // the end
        }
    }
}
