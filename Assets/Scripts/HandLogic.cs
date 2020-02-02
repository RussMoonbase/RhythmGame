using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandLogic : MonoBehaviour
{
   Animator _animator;
   [SerializeField] KeyCode _tapKey;

   // Start is called before the first frame update
   void Start()
   {
      _animator = GetComponentInChildren<Animator>();
   }

   // Update is called once per frame
   void Update()
   {
      if (Input.GetKeyDown(_tapKey))
      {
         TapDown();
      }
   }

   public void TapDown()
   {
      if (_animator)
      {
         _animator.SetTrigger("Tapped");
      }
   }

   public void HoldDown()
   {

   }
}
