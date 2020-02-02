using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TrackFeedbackFX : MonoBehaviour
{
    public Image failEffect;
    public Image successEffect;
    public Image holdEffect;

    public float showTime = .1f;

    private bool buttonIsDown = false;

    private void Awake()
    {
        SetImageAlpha(failEffect, 0f);
        SetImageAlpha(successEffect, 0f);
        SetImageAlpha(holdEffect, 0f);
    }

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

    private void Update()
    {
        SetImageAlpha(holdEffect, buttonIsDown ? 1f : 0f);
    }

    private void Inst_OnBeatPressed(BeatBlock block)
    {
        if (block.BeatLength > 0)
        {
            buttonIsDown = true;
        }
    }

    private void Inst_OnBeatScored(BeatBlock block, float score)
    {
        buttonIsDown = false;
        StartCoroutine(FlashImageCR(successEffect));
    }

    private void Inst_OnMissed(BeatBlock block)
    {
        StartCoroutine(FlashImageCR(failEffect));
    }

    private void SetImageAlpha(Image image, float alpha)
    {
        var c = image.color;
        c.a = Mathf.Clamp01(alpha);
        image.color = c;
    }

    private IEnumerator FlashImageCR(Image image)
    {
        SetImageAlpha(image, 1f);
        yield return new WaitForSeconds(showTime);
        while (image.color.a > 0f)
        {
            SetImageAlpha(image, image.color.a - (Time.deltaTime / showTime));
            yield return null;
        }
        // TODO REMOVE
        Debug.Log("fade done");
    }
}
