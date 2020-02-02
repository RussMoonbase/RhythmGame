using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class BeatBitCompiler {
    public static List<BeatBit> Compile(List<string> words, System.Random rand) {
        List<BeatBit> bits = new List<BeatBit>();

        foreach (string word in words) {
            bits.AddRange(Compile(word, rand));
        }

        return bits;
    }

    private static List<BeatBit> Compile(string word, System.Random rand) {
        int hash = word.GetHashCode();
        int bits = hash & 0xf;

        List<BeatBit> bitList = new List<BeatBit>();

        // Try to avoid sending no signal
        if (bits == 0) {
            bits = (hash >> 8) & 0xf;
        }

        // chris: trying more filled out-ness
        for (int i = 0; i < 7; i++) {
            if ((bits & (0x1 << i)) > 0) {
                bitList.Add(BeatBit.Long);
            } else {
                bitList.Add(BeatBit.Single);
            }
        }
        bitList.Add(BeatBit.None);

        return bitList;
    }
}
