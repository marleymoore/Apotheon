using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EncounterList : MonoBehaviour
{
    [SerializeField] List<ApostleBase> wildApostles;



    /*APOSTLE LIST:
     * 
     * Returns a random apostle from within the list given in the inspector
     */
   public ApostleBase GetRandomApostle(List<ApostleBase> list)
   {

        list = wildApostles;
        int randomIndex = Random.Range(0, list.Count);
        ApostleBase chosenApostle = wildApostles[randomIndex];
        Debug.Log(chosenApostle);
        
        return chosenApostle;
        
    }

  // public ApostleBase EncounteredApostle()
  // {
  //     ApostleBase encounteredApostle = wildApostle;
  //     return encounteredApostle;
  // }
   // public void EnemyImage()
   // {
   //     GetComponent<Image>().sprite = wildApostle.FrontSprite;
   // }

    

 //  T GetRApostle<T>(List<T> list)
 // {
 //
 //      if (list == null || list.Count == 0)
 //      {
 //          return default(T);
 //      }
 //      int randomIndex = Random.Range(0, list.Count);
 //      //Debug.Log(randomIndex);
 //     ApostleBase chosenApostle = wildApostles[randomIndex];
 //     return list[randomIndex];
 //     
 // }
}
