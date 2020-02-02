using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterLogic : MonoBehaviour
{
   public KeyCode increaseKey;
   public KeyCode decreaseKey;

   float _happinessFactor = 0;
   float _currentHappiness;

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
         _currentHappiness = _happinessFactor;
         StartCoroutine(UpdateHappinessRoutine(_currentHappiness + 0.1f));
      }

      if (Input.GetKeyDown(decreaseKey))
      {
         _currentHappiness = _happinessFactor;
         StartCoroutine(UpdateHappinessRoutine(_currentHappiness - 0.1f));
      }
   }

   IEnumerator UpdateHappinessRoutine(float happinessLevel)
   {
      const float FINISH_TIME = 0.5f;
      float timer = 0f;

      while(timer < FINISH_TIME)
      {
         timer += Time.deltaTime;
         if (_animator)
         {
            _happinessFactor = Mathf.Lerp(_currentHappiness, happinessLevel, timer);
            _animator.SetFloat("HappinessFactor", _happinessFactor);
            yield return null;
         }
      }
   }

   void ChangeHappiness(float happinessNum)
   {
      if (_animator)
      {
         _animator.SetFloat("HappinessFactor", Mathf.Lerp(_currentHappiness, happinessNum, Time.deltaTime * 0.5f));
      }
   }
}
