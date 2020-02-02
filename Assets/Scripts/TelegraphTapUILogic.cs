using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TelegraphTapUILogic : MonoBehaviour
{
   Image _image;
   [SerializeField] KeyCode _tapKey;
   [SerializeField] Sprite _offSprite;
   [SerializeField] Sprite _onSprite;

   // Start is called before the first frame update
   void Start()
   {
      _image = GetComponent<Image>();
   }

   // Update is called once per frame
   void Update()
   {
      if (Input.GetKeyDown(_tapKey))
      {
         _image.sprite = _onSprite;
      }

      if (Input.GetKeyUp(_tapKey))
      {
         _image.sprite = _offSprite;
      }
   }
}
