using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CleanUpEffects : EventData
{
    public CleanUpEffects(Apostle apostle)
    {

        if (apostle.CurrentStatusEffect == Apostle.StatusEffect.poisoned)
        {
            Debug.Log("hello");
            apostle.CurrentHP -= apostle.MaxHp / 4;
            if (apostle.CurrentHP <= 0) apostle.CurrentHP = 0;
        }
    }


}
