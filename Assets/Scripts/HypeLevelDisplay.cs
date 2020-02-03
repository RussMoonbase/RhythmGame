using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HypeLevelDisplay : MonoBehaviour
{
   [SerializeField] Text _incidentText;
   [SerializeField] Text _satisfactionText;
   [SerializeField] MeasureHype _measureHype;

   int displayNumber;

   // Start is called before the first frame update
   void Start()
   {
      _incidentText.text = "0";
      _satisfactionText.text = "0";
   }

   // Update is called once per frame
   void Update()
   {
      UpdateText();
   }

   void UpdateText()
   {


      if (_measureHype.Hype > 0)
      {
         displayNumber = (int)(_measureHype.Hype * 10);
         _satisfactionText.text = displayNumber.ToString();
      }
      
      if (_measureHype.Hype < 0)
      {
         displayNumber = (int)(_measureHype.Hype * 10);
         _incidentText.text = displayNumber.ToString();
      }

      if (_measureHype.Hype == 0)
      {
         _satisfactionText.text = "0";
         _incidentText.text = "0";
      }
   }
}
