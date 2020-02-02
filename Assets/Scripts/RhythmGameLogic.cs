using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RhythmGameLogic : MonoBehaviour
{
    public static RhythmGameLogic inst { get; private set; }

    public event UnityAction<BeatBlock> OnBeatPressed = delegate { }; // Player pressed downn on a beat
    public event UnityAction<BeatBlock, float> OnBeatScored = delegate { }; // Player released on a beat and received some points
    public event UnityAction OnMissed = delegate { };

    public RectTransform beatTrack;
    public KeyCode buttonToPress = KeyCode.Z;
    public GameObject beatBlockPrefab;

    private int lastInstantiatedBeat = -1;
    private BeatBlock lastInstantiatedBlock = null;
    private List<BeatBlock> instantiatedBlocks = new List<BeatBlock>();
    private List<BeatBlock> alreadyScoredBlocks = new List<BeatBlock>();
    private BeatBlock pressingOnBlock = null;
    private float pressStartTime = 0f;

    public int initialEmptyBeats = 8;
    public float beatLeadTime = 12; // how long OnBeatStartingSoon is called before OnBeat
    // todo maybe have a calibrated offset
    public float pressButtonWithinTime = .2f; // press the button within .2 beats of the actual start/end time
    public float deleteAfter = 20; // delete after this many beats


    // the beat pattern is going to come as a series of 0s and 1s in bytes
    // probably care about it in pairs, so like 101010 is 3 beats in sequence and 101110 is one beat and one long beat
    // and broken into 2 byte sections????? i guess?

    private List<BeatBit> toPlay = new List<BeatBit>();

    private void Awake()
    {
        inst = this;
    }

    private void Start()
    {
        List<string> poem = TelegramSource.inst.GetLovePoem();
        this.toPlay = BeatBitCompiler.Compile(poem, RandomSource.rand);
    }

    private void Update()
    {
        // update input for the tracks

        if (null != pressingOnBlock)
        {
            if (Input.GetKeyUp(buttonToPress))
            {
                float score = ScoreBeat(pressingOnBlock, pressStartTime, Jukebox.inst.CurrentBeat);
                OnBeatScored(pressingOnBlock, score);
                alreadyScoredBlocks.Add(pressingOnBlock);
                pressingOnBlock = null;
            }
        }
        else
        {
            if (Input.GetKeyDown(buttonToPress))
            {
                BeatBlock closest = null;
                float closestDelta = 0f;
                // look for a block I might be pressing on
                for (int i = 0; i < instantiatedBlocks.Count; i++)
                {
                    var block = instantiatedBlocks[i];
                    float startDelta = Mathf.Abs(block.StartOnBeat - Jukebox.inst.CurrentBeat);
                    if (startDelta < pressButtonWithinTime && !alreadyScoredBlocks.Contains(block)
                            && (null == closest || startDelta < closestDelta))
                    {
                        closest = block;
                        closestDelta = startDelta;
                        break;
                    }
                }

                if (null != closest)
                {
                    Debug.Log("Hitting block " + closest + " current time " + Jukebox.inst.CurrentBeat);
                    pressingOnBlock = closest;
                    pressStartTime = Jukebox.inst.CurrentBeat;
                    OnBeatPressed(closest);
                }
                else
                {
                    OnMissed(); // you missed, you fool!!!
                }
            }
        }

        
        // instantiation
        while (Jukebox.inst.CurrentBeat + beatLeadTime > lastInstantiatedBeat && lastInstantiatedBeat < toPlay.Count)
        {
            AdvanceBeatIndex();
        }
        

        // cleanup
        for (int i = 0; i < instantiatedBlocks.Count; i++)
        {
            var block = instantiatedBlocks[i];
            if (Jukebox.inst.CurrentBeat > block.EndOnBeat + deleteAfter)
            {
                Destroy(block.gameObject);
                instantiatedBlocks.RemoveAt(i);
                i--;
            }
        }
        for (int i = 0; i < alreadyScoredBlocks.Count; i++)
        {
            if (null == alreadyScoredBlocks[i])
            {
                alreadyScoredBlocks.RemoveAt(i);
                i--;
            }
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
                    instantiatedBlocks.Add(lastInstantiatedBlock);
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

    public float ScoreBeat(BeatBlock beat, float beatStartedPress, float beatEndedPress)
    {
        Debug.Log("Scored beat: " + beat + " started press " + beatStartedPress + " ended press " + beatEndedPress);
        // TODO score logic!!!!!!!!!!!!!!!!
        return 1f;
    }
}
