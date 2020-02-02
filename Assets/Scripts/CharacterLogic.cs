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
   [SerializeField] MeasureHype _measureHype;


   // Start is called before the first frame update
   void Start()
   {
      _animator = this.gameObject.GetComponent<Animator>();
   }

   // Update is called once per frame
   void Update()
   {
      UpdateHappiness();
   }

   private void UpdateHappiness()
   {
      _animator.SetFloat("HappinessFactor", _measureHype.Hype);
   }
}
