using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterLogic : MonoBehaviour
{
   float _happinessFactor = 0;
   public KeyCode increaseKey;
   public KeyCode decreaseKey;

   Animator _animator;


   // Start is called before the first frame update
   void Start()
   {
      _animator = this.gameObject.GetComponent<Animator>();
   }

   // Update is called once per frame
   void Update()
   {
      if (Input.GetKeyDown(increaseKey))
      {
         IncreaseHappiness();
      }

      if (Input.GetKeyDown(decreaseKey))
      {
         DecreaseHappiness();
      }
   }

   void IncreaseHappiness()
   {
      _happinessFactor += 0.1f;
      Debug.Log("HappinessFactor = " + _happinessFactor);

      if (_animator)
      {
         Debug.Log("there is an animator");
         _animator.SetFloat("HappinessFactor", _happinessFactor);
      }
   }

   void DecreaseHappiness()
   {
      _happinessFactor -= 0.1f;

      if (_animator)
      {
         _animator.SetFloat("HappinessFactor", _happinessFactor);
      }
   }
}
