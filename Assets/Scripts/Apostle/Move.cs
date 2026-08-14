using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Move
{
    public MoveBase Base { get; set; }

    public Effect type;
    //public Effect UniqueType { get { return type; } }
    public Move(MoveBase aBase)
    {
        Base = aBase;
        type = aBase.EffectType;
    }

}

public enum Effect
{
    None,
    Heal,
    DoubleHit,
    Poison
}