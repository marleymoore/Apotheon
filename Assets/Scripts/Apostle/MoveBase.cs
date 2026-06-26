using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Move", menuName = "Apostle/Create New Move")]

public class MoveBase : ScriptableObject
{
    // Start is called before the first frame update

    [SerializeField] string name;

    [TextArea]
    [SerializeField] string description;

    [SerializeField] ApostleType type;
    [SerializeField] int power;
    [SerializeField] int accuracy;
    [SerializeField] UniqueType uniqueMove;
    [SerializeField] bool isSpecial;

    public string Name { get { return name; } }

    public string Description {  get { return description; } }

    public ApostleType Type { get { return type; } }

    public int Power { get { return power; } }

    public int Accuracy { get { return accuracy; } }

    public UniqueType UniqueMove { get {  return uniqueMove; } }

    public bool IsSpecial { get { return isSpecial; } }
}
