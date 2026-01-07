using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable]



public class Apostle
{

 //[SerializeField] ApostleBase apostleBase;
 //[SerializeField] int level;

  
    public int CurrentHP {  get; set; }
    public ApostleBase ApostleBase { get; set; }
    public int Level { get; set; }

    public List<Move> Moves { get; set; }

    public Apostle(ApostleBase aBase, int aLevel)
    {
        ApostleBase = aBase;
        Level = aLevel;
        CurrentHP = MaxHp;

        Moves = new List<Move>();

        //Moves = aBase.LearnableMoves
        //     .Where(move => move.Level <= Level)
        //     .Take(4)
        //     .Select(move => new Move(move.Base))
        //     .ToList();
        foreach (var move in ApostleBase.LearnableMoves)
        {

            if (move.Level <= Level)
                Moves.Add(new Move(move.Base));

            if (Moves.Count >= 4)
                break;
        }

        /* TODO: turn above into a lambda expression */
    }



    public int MaxHp { get { return Mathf.FloorToInt((ApostleBase.MaxHP * Level) / 100f) + 10; } }
    public int Attack { get { return Mathf.FloorToInt((ApostleBase.Attack * Level) / 100f) + 5; } }
    public int SpAttack { get { return Mathf.FloorToInt((ApostleBase.SpAttack * Level) / 100f) + 5; } }
    public int Defence { get { return Mathf.FloorToInt((ApostleBase.Defence * Level) / 100f) + 10; } }
    public int SpDefence { get { return Mathf.FloorToInt((ApostleBase.SpDefence * Level) / 100f) + 10; } }
    public int Speed { get { return Mathf.FloorToInt((ApostleBase.Speed * Level) / 100f) + 10; } }

 //  public bool TakeDamage(Move move, Apostle attacker)
 //  {
 //      float a = (2 * attacker.Level + 10) / 250f;
 //      float b = a * move.Base.Power * ((float)attacker.Attack / Defence) + 2;
 //      int damage = Mathf.FloorToInt(b);
 //
 //      currentHP -= damage;
 //      if(currentHP <= 0)
 //      {
 //          currentHP = 0;
 //          return true;
 //      }
 //
 //      return false;
 //  }
 //
    public Move RandomMove()
    {
        int r = Random.Range(0, Moves.Count);
        return Moves[r];
    }

}
