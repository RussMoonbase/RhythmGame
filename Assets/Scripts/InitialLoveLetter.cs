using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public sealed class InitialLoveLetter : MonoBehaviour {
    public TextMeshPro textMesh;

    // Start is called before the first frame update
    void Start() {
        textMesh.text = string.Join(" ", TelegramSource.inst.GetLovePoem());
    }
}
