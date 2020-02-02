using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public sealed class BeatText : MonoBehaviour {
    public float StartOnBeat { get; set; } = 0f;
    public float distanceBetweenBeats = 200;
    public TextMeshProUGUI textMesh;
    public RectTransform rectTransform;

    private void Start() {
        this.rectTransform = (RectTransform)transform;
    }

    private void Update() {
        this.UpdatePosition();
    }

    public void SetText(string text) {
        this.textMesh.SetText(text);
    }

    public void UpdatePosition() {
        var pos = rectTransform.anchoredPosition;
        pos.x = (StartOnBeat - Jukebox.inst.CurrentBeat) * distanceBetweenBeats;
        rectTransform.anchoredPosition = pos;
    }
}
