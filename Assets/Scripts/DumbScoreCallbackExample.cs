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

    private void Inst_OnBeatPressed(BeatBlock block)
    {
        Debug.Log("Beat pressed " + block);
    }

    private void Inst_OnBeatScored(BeatBlock block, float score)
    {
        totalScore += score;
        Debug.Log("Score " + score + " New total score " + totalScore);
    }

    private void Inst_OnMissed(BeatBlock block)
    {
        if (null != block)
        {
            Debug.Log("Whiffed block " + block);
        }
        else
        {
            Debug.Log("Tried to hit something but missed");
        }
    }

}
