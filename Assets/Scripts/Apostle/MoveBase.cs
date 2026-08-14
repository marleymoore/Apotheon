using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
    [SerializeField] Effect effectType;
    [SerializeField] bool isSpecial;
    [SerializeField] bool hasEffect;


    public string Name { get { return name; } }

    public string Description {  get { return description; } }

    public ApostleType Type { get { return type; } }

    public int Power { get { return power; } }

    public int Accuracy { get { return accuracy; } }

    public Effect EffectType { get {  return effectType; } }

    public bool IsSpecial { get { return isSpecial; } }

    public bool Haseffect { get { return hasEffect; } }

}
