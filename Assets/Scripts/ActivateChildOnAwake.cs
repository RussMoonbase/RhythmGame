using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateChildOnAwake : MonoBehaviour
{
    // making this because the canvas is ALWAYS IN THE **** WAY
    private void Awake()
    {
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(true);
        }
    }
}
