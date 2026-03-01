using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TakeDamage : EventData
{
    public TakeDamage(Move move, Apostle attacker, Apostle defender)
    {
        float type = TypeChart.TypeEffectiveness(move.Base.Type, defender.ApostleBase.Type1) *
            TypeChart.TypeEffectiveness(move.Base.Type, defender.ApostleBase.Type2);



        int miss = UnityEngine.Random.Range(0, 100);

        float criticalHit = UnityEngine.Random.value * 100f <= 6.25f ?  2f : 1f;

        /* crit chance, hit chance
         * take the damage */
        if(move.Base.Accuracy >= miss)
        {


            float a = (2 * attacker.Level + 10) / 250f;
            float b = a * move.Base.Power * ((float)attacker.Attack / defender.Defence) + 2;
            int damage = Mathf.FloorToInt(b * type * criticalHit);

            defender.CurrentHP -= damage;
            if (defender.CurrentHP <= 0)
            {
                defender.CurrentHP = 0;
                ApostleDeath apostleDeath = new ApostleDeath(defender);
            }
        }
        else
        {
            Debug.Log("missed");
        }

         
    }
}
