using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EncounterList : MonoBehaviour
{
    [SerializeField] List<ApostleBase> wildApostles;
    [SerializeField] int minLevel;
    [SerializeField] int maxLevel;
    int finalLevel;

    public int MinLevel { get { return minLevel; } }
    public int MaxLevel { get { return maxLevel; } }

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

    public int ApostleLevelCalc(int minParam, int maxParam)
    {
        minParam = MinLevel;
        maxParam = MaxLevel;
        finalLevel = Random.Range(minParam, maxParam);
        return finalLevel;
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
