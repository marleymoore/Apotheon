using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Encounter : EventData
{


    EncounterList EncounterList;

    public ApostleBase WildApostle { get; private set; }
    // Start is called before the first frame update


    /*ENCOUNTER EVENT CLASS
     *
     * Should return a random Apostle from a given list
     * 
     * TODO: DOUBLE CHECK THIS SHI ACTYUALLY RETURNS A RANDOM APOSTLE (done ;) )
     */
    public Encounter(int eRate, EncounterList encounterList)
    {
        this.EncounterList = encounterList;
        List<ApostleBase> encounters = new List<ApostleBase>();


        if (eRate <= 10)
        {
          
          Debug.Log("encounterList:", encounterList);
          WildApostle = this.EncounterList.GetRandomApostle(encounters);
            
        
        }
       
    }

 // T GetRApostle<T>(List<T> list)
 // {
 //     int randomIndex = Random.Range(0, list.Count);
 //     return list[randomIndex];
 // }
}
