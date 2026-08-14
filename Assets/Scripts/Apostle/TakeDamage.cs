using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VersionControl.Git;
using UnityEngine;
using UnityEngine.UI;


public class TakeDamage : EventData
{
    GameObject dialogueBox = GameObject.FindGameObjectWithTag("BattleDialogue");
    GameObject hpBar = GameObject.FindGameObjectWithTag("HUD");

  

    public TakeDamage(Move move, Apostle attacker, Apostle defender)
    {
        CalculateDamage(move, attacker, defender);
        BattleDialogue battleDialogue = dialogueBox.GetComponent<BattleDialogue>();
        
       // if (dialogueBox == null) Debug.LogError("BattleDialogue tag not found or object inactive!");


        if (move.Base.Haseffect == true)
        {


            switch (move.type)
            {
                case Effect.Heal:
                {
                        int heal = attacker.MaxHp / 4;

                        attacker.CurrentHP += heal;
                        if (attacker.CurrentHP > attacker.MaxHp) attacker.CurrentHP = attacker.MaxHp;
                        break;
                }
                case Effect.DoubleHit:
                    {

                        battleDialogue.StartCoroutine(HandleMultiAttack(attacker, defender, move, 1));
                        
                        break;
                    }
                case Effect.Poison:
                    {
                        defender.SetStatusEffects(defender, Effect.Poison);
                        break;
                    }
            }
            // if(move.type == Effect.Heal)
            // {
            //     int heal = attacker.MaxHp / 4;
            //
            //     attacker.CurrentHP += heal;
            //
            //     if (attacker.CurrentHP > attacker.MaxHp) attacker.CurrentHP = attacker.MaxHp;
            //    
            // }
        }
         
    }

    void CalculateDamage(Move move, Apostle attacker, Apostle defender)
    {
        float type = TypeChart.TypeEffectiveness(move.Base.Type, defender.ApostleBase.Type1) *
            TypeChart.TypeEffectiveness(move.Base.Type, defender.ApostleBase.Type2);



        int miss = UnityEngine.Random.Range(0, 100);

        float criticalHit = UnityEngine.Random.value * 100f <= 6.25f ? 2f : 1f;

        /* crit chance, hit chance
         * take the damage */
        float attack = (move.Base.IsSpecial) ? attacker.SpAttack : attacker.Attack;
        float defence = (move.Base.IsSpecial) ? defender.SpDefence : defender.Defence;

        if (move.Base.Accuracy >= miss)
        {


            float a = (2 * attacker.Level + 10) / 250f;
            float b = a * move.Base.Power * ((float)attack / defence) + 2;
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
        EventBus.Raise(this);
    }


    IEnumerator HandleMultiAttack(Apostle attacker, Apostle defender, Move move, int numberOfAttacks)
    {
        BattleDialogue battleDialogue = dialogueBox.GetComponent<BattleDialogue>();
        

        for (int i = 0; i < numberOfAttacks; i++)
        {
            yield return battleDialogue.StartCoroutine(battleDialogue.TypeDialogue($"{attacker} Strikes again!!!"));
            //Debug.Log(battleDialogue);
            CalculateDamage(move, attacker, defender);
            if(defender.CurrentHP <= 0) break;
            
        }
       
    }
}
