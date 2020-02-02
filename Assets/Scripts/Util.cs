﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Util
{
    public static T RandomChoose<T>(T[] array)
    {
        return array[Random.Range(0, array.Length)];
    }
}
