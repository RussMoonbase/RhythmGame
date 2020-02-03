using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyTelegram : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (null != TelegramSource.inst)
        {
            Destroy(TelegramSource.inst.gameObject);
        }
    }

}
