using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;

[CreateAssetMenu(fileName = "Apostle", menuName = "Apostle/Create new")]

public class ApostleBase : ScriptableObject
{

    [SerializeField] string name;

    [TextArea]
    [SerializeField] string description;

    [SerializeField] ApostleType type1;
    [SerializeField] ApostleType type2;

    [SerializeField] Sprite frontSprite;
    [SerializeField] Sprite backSprite;

    [SerializeField] int maxHp;
    [SerializeField] int attack;
    [SerializeField] int defence;
    [SerializeField] int spAttack;
    [SerializeField] int spDefence;
    [SerializeField] int speed;

    [SerializeField] List<LearnableMoves> learnableMoves;

    public string Name { get { return name; } }

    public string Description { get { return description; } }

    public ApostleType Type1 { get { return type1; } }

    public ApostleType Type2 { get { return type2; } }

    public Sprite FrontSprite { get { return frontSprite; } }

    public Sprite BackSprite { get { return backSprite; } }

    public int MaxHP { get { return maxHp; } }

    public int Attack { get { return attack; } }

    public int Defence { get { return defence; } }

    public int SpAttack { get { return spAttack; } }

    public int SpDefence { get { return spDefence; } }

    public int Speed { get { return speed; } }

    public List<LearnableMoves> LearnableMoves {  get { return learnableMoves; } }

}

[System.Serializable]
public class LearnableMoves
{
    [SerializeField] MoveBase moveBase;
    [SerializeField] int level;

    public int Level { get { return level; } }

    public MoveBase Base { get { return moveBase; } }

}
public enum ApostleType
{
    Bone,
    Primal,
    Iron,
    Mind,
    Colour
}

public class TypeChart
{
    static float[][] chart =
    {
        //                    BON PRM IRN MND CLR 
        /*BON*/ new float [] { 1f, 1f, 1f, 1f, 1f },
        /*PRM*/ new float [] { 2f, 0.5f, 2f, 0.5f, 0.5f },
        /*IRN*/ new float [] { 2f, 1f, 0.5f, 1f, 1f },
        /*MND*/ new float [] { 1f, 2f, 1f, 1f, 0.5f },
        /*CLR*/ new float [] { 1f, 0.5f, 1f, 2f, 2f },
    };

    public static float TypeEffectiveness(ApostleType attackType, ApostleType defenseType)
    {
        int row = (int)attackType;
        int col = (int)defenseType;

        return chart[row][col];
    }
} 




