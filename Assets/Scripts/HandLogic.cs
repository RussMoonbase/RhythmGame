using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandLogic : MonoBehaviour
{
   Animator _animator;
   [SerializeField] KeyCode _tapKey;
   [SerializeField] KeyCode _leftMoveKey;
   [SerializeField] KeyCode _rightMoveKey;

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
         HoldDown();
      }
      
      if (Input.GetKeyUp(_tapKey))
      {
         ReleaseHold();
      }

      if (Input.GetKeyDown(_rightMoveKey))
      {
         if (this.transform.position.x > 0.029f && this.transform.position.x < 0.032f)
         {
            StartCoroutine(MoveHand(0.270f));
         }  
      }

      if (Input.GetKeyDown(_leftMoveKey))
      {
         if (this.transform.position.x >= 0.301f)
         {
            StartCoroutine(MoveHand(-0.270f));
         }
      }


   }

   public void HoldDown()
   {
      if (_animator)
      {
         _animator.SetBool("isHeld", true);
      }
   }

   public void ReleaseHold()
   {
      if (_animator)
      {
         _animator.SetBool("isHeld", false);
      }
   }

   public void MoveLeft()
   {

   }

   IEnumerator MoveHand(float newXPos)
   {
      const float FINISH_TIME = 0.3f;
      float timer = 0f;
      Vector3 currentPos = this.transform.position;
      Vector3 newPos = new Vector3(this.transform.position.x + newXPos, this.transform.position.y, this.transform.position.z);

      while (timer < FINISH_TIME)
      {
         timer += Time.deltaTime;
         if (_animator)
         {
            this.transform.position = Vector3.Lerp(currentPos, newPos, timer * 5);
            yield return null;
         }
      }
   }
}
