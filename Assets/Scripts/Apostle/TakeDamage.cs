using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TakeDamage : EventData
{
    public TakeDamage(Move move, Apostle attacker, Apostle defender)
    {
        int miss = UnityEngine.Random.Range(0, 100);
        /* crit chance, hit chance */
        if(move.Base.Accuracy >= miss)
        {
            float a = (2 * attacker.Level + 10) / 250f;
            float b = a * move.Base.Power * ((float)attacker.Attack / defender.Defence) + 2;
            int damage = Mathf.FloorToInt(b);

            defender.CurrentHP -= damage;
            if (defender.CurrentHP <= 0)
            {
                defender.CurrentHP = 0;

            }
        }
        else
        {
            Debug.Log("missed");
        }

         
    }
}
