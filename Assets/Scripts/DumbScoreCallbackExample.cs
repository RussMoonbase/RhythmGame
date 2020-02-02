using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DumbScoreCallbackExample : MonoBehaviour
{
    private float totalScore = 0f;

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

    private void Inst_OnBeatPressed(BeatBlock beat)
    {
        Debug.Log("Beat pressed yo " + beat);
    }

    private void Inst_OnBeatScored(BeatBlock beat, float score)
    {
        totalScore += score;
        Debug.Log(" New total score " + totalScore);
    }

    private void Inst_OnMissed()
    {
        Debug.LogError("You missed, ding dong! :O");
    }

}
