using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move
{
    // Start is called before the first frame update
    public MoveBase Base { get; set; }

    public UniqueType type;
    public UniqueType UniqueType { get { return type; } }
    public Move(MoveBase aBase)
    {
        Base = aBase;
    }

    void CUM()
    {
        type = new UniqueType();

        if(type == UniqueType.Heal)
        {
            
        }
    }
}

public enum UniqueType
{
    None,
    Heal
}