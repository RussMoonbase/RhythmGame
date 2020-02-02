using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSource : MonoBehaviour {
    private static System.Random _rand = null;
    public static System.Random rand 
    { 
        get
        {
            if (null == _rand)
            {
                // find it!!! this is so it can be found as soon as it is needed
                var source = FindObjectOfType<RandomSource>();
                if (null != source)
                {
                    _rand = new System.Random(source.seed);
                }
                else
                {
                    _rand = new System.Random();
                }
            }
            return _rand;
        } 
    }

    public int seed = 1234;

    private void OnDestroy()
    {
        _rand = null; // let the next scene make another one
    }
}
