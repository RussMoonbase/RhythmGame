using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeasureHype : MonoBehaviour
{
    public float Hype { get; private set; } = 0f;
   float minHype = -1f;
   float maxHype = 1f;

    void Start()
    {
        RhythmGameLogic.inst.OnBeatPressed += Inst_OnBeatPressed;
        RhythmGameLogic.inst.OnBeatScored += Inst_OnBeatScored;
        RhythmGameLogic.inst.OnMissed += Inst_OnMissed;
    }

    void OnDestroy()
    {
        if (null != RhythmGameLogic.inst)
        {
            RhythmGameLogic.inst.OnBeatPressed -= Inst_OnBeatPressed;
            RhythmGameLogic.inst.OnBeatScored -= Inst_OnBeatScored;
            RhythmGameLogic.inst.OnMissed -= Inst_OnMissed;
        }
    }

    private void Inst_OnBeatPressed(BeatBlock block)
    {
        
    }

    private void Inst_OnBeatScored(BeatBlock block, float score)
    {
        if (score == 2f)
        {
            Hype += .05f;
        }
        else if (score > 2f)
        {
            Hype += .1f;
        }
        else
        {
            Hype -= .1f;
        }

        Hype = Mathf.Clamp(Hype, minHype, maxHype);

        //Hype = Mathf.Clamp01(Hype);
    }

    private void Inst_OnMissed(BeatBlock block)
    {
        Hype -= .1f;
        Hype = Mathf.Clamp(Hype, minHype, maxHype);
        //Hype = Mathf.Clamp01(Hype);
    }

}
