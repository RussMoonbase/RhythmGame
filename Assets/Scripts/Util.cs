﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Util {
    public static T RandomChoose<T>(T[] array) {
        return array[RandomSource.rand.Next(array.Length)];
    }

    public static bool RandomBool() {
        return (RandomSource.rand.Next() & 1) > 0;
    }
}
