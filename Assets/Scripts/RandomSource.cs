using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSource : MonoBehaviour {
    public static System.Random rand { get; private set; }

    public int seed = 1234;

    private void Awake() {
        rand = new System.Random(this.seed);
    }
}
