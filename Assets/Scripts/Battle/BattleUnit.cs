using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BattleUnit : MonoBehaviour
{
    //[SerializeField] ApostleBase ApostleBase;
    //[SerializeField] int level;
    //[SerializeField] bool isPlayerUnit;

    public Apostle Apostle { get; set; }

    //EncounterList encounterList;

    public void SetUp(ApostleBase encounteredApostle, int level, bool isPlayerUnit)
    {


        //encounterList = GetComponent<EncounterList>();


       // encounteredApostle = encounterList.wildApostle;
        Apostle = new Apostle(encounteredApostle, level);
       // isPlayerUnit = true ? GetComponent<Image>().sprite = apostle.ApostleBase.BackSprite : GetComponent<Image>().sprite = apostle.ApostleBase.FrontSprite;

        if(isPlayerUnit == true)
        {
            GetComponent<Image>().sprite = Apostle.ApostleBase.BackSprite;
        }
        else
        {
            GetComponent<Image>().sprite = Apostle.ApostleBase.FrontSprite;

        }

        // if (isPlayerUnit)
        //     GetComponent<Image>().sprite = apostle.ApostleBase.BackSprite;
        // else
        //     GetComponent<Image>().sprite = apostle.ApostleBase.FrontSprite;

        //isPlayerUnit = true ? apostle.ApostleBase.BackSprite : apostle.ApostleBase.FrontSprite;
    }
}
