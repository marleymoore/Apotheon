using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VersionControl.Git;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;


public class TakeDamage : EventData
{
    GameObject dialogueBox = GameObject.FindGameObjectWithTag("BattleDialogue");
    GameObject greenRadialUI = GameObject.FindGameObjectWithTag("GreenHit");
    GameObject radialCheck = GameObject.FindGameObjectWithTag("RadialCheck");

  //  RectTransform greenHitBox;
   // RectTransform tickHitBox;

    BattleDialogue battleDialogue;
    //Image greenDial;
    Image redTick;
    FilledDialImage greenDial;
    


    //GameObject hpBar = GameObject.FindGameObjectWithTag("HUD");

    public Move move;
    public Apostle attacker;
    public Apostle defender;

    float greenResult;
    float dialResult = 0;


    public float GreenResult { get { return greenResult; } }

    public TakeDamage(Move move, Apostle attacker, Apostle defender)
    {
        this.move = move;
        this.attacker = attacker;
        this.defender = defender;
        battleDialogue = dialogueBox.GetComponent<BattleDialogue>();
        redTick = radialCheck.GetComponent<Image>();

        // greenHitBox = greenDial.GetComponent<RectTransform>();
        // tickHitBox = radialCheck.GetComponent<RectTransform>();
        

    
    }

    //CREATE ANOTHER COROUTINE HERE TO DETERMINETHE RADIAL INPUT!!!

    public IEnumerator AttackQTE()
    {
        battleDialogue.EnableHitRadial(true);
        greenDial = greenRadialUI.GetComponent<FilledDialImage>();
        greenResult = Normalize(move, 5f, 100f);
        greenDial.fillAmount = greenResult;
        
        greenDial.alphaHitTestMinimumThreshold = 0.05f;
        redTick.alphaHitTestMinimumThreshold = 0.05f;

        for(; ; )
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                // Debug.Log($"{greenHitBox.position}, {tickHitBox.position}");
                // dialResult = tickHitBox.Overlaps(greenHitBox) ? 1f : 0f;
                // Debug.Log(dialResult);
                dialResult = HitRadial.ImagesOverlap(greenDial, redTick) ? 1 : 0;
                
                break;
            }
            else dialResult = 0f;

                yield return null;
        }
        if (dialResult == 1)
        {
            yield return battleDialogue.TypeDialogue("SUCCESS");
        }
        else
        {
            yield return battleDialogue.TypeDialogue("FAILURE");
            
        }

       

        battleDialogue.EnableHitRadial(false);


        CalculateDamage(move, attacker, defender);

        VoidEvent healthUpdate = new VoidEvent();
        if (move.Base.Haseffect == true)
        {


            switch (move.type)
            {
                case Effect.Heal:
                    {
                        int heal = attacker.MaxHp / 4;

                        attacker.CurrentHP += heal;
                        if (attacker.CurrentHP > attacker.MaxHp) attacker.CurrentHP = attacker.MaxHp;
                        EventBus.Raise(healthUpdate);
                        break;
                    }
                case Effect.DoubleHit:
                    {

                        //battleDialogue.StartCoroutine(HandleMultiAttack(attacker, defender, move, 1));
                        yield return battleDialogue.StartCoroutine(HandleMultiAttack(attacker, defender, move, 1));

                        break;
                    }
                case Effect.Poison:
                    {
                        Debug.Log("IM CALLED");
                        defender.SetStatusEffects(defender, Effect.Poison);
                        break;
                    }
            }
        }
    }

    //combine the two coroutines
    public IEnumerator Execute()
    {

        

        CalculateDamage(move, attacker, defender);

        VoidEvent healthUpdate = new VoidEvent();
        if (move.Base.Haseffect == true)
        {


            switch (move.type)
            {
                case Effect.Heal:
                    {
                        int heal = attacker.MaxHp / 4;

                        attacker.CurrentHP += heal;
                        if (attacker.CurrentHP > attacker.MaxHp) attacker.CurrentHP = attacker.MaxHp;
                        EventBus.Raise(healthUpdate);
                        break;
                    }
                case Effect.DoubleHit:
                    {

                        //battleDialogue.StartCoroutine(HandleMultiAttack(attacker, defender, move, 1));
                        yield return battleDialogue.StartCoroutine(HandleMultiAttack(attacker, defender, move, 1));

                        break;
                    }
                case Effect.Poison:
                    {
                        Debug.Log("IM CALLED");
                        defender.SetStatusEffects(defender, Effect.Poison);
                        break;
                    }
            }
        }
    }



    // public TakeDamage(Move move, Apostle attacker, Apostle defender)
    // {
    //     CalculateDamage(move, attacker, defender);
    //     BattleDialogue battleDialogue = dialogueBox.GetComponent<BattleDialogue>();
    //     
    //    // if (dialogueBox == null) Debug.LogError("BattleDialogue tag not found or object inactive!");
    //
    //
    //     if (move.Base.Haseffect == true)
    //     {
    //
    //
    //         switch (move.type)
    //         {
    //             case Effect.Heal:
    //             {
    //                     int heal = attacker.MaxHp / 4;
    //
    //                     attacker.CurrentHP += heal;
    //                     if (attacker.CurrentHP > attacker.MaxHp) attacker.CurrentHP = attacker.MaxHp;
    //                     break;
    //             }
    //             case Effect.DoubleHit:
    //                 {
    //
    //                     //battleDialogue.StartCoroutine(HandleMultiAttack(attacker, defender, move, 1));
    //                     battleDialogue.StartCoroutine(HandleMultiAttack(attacker, defender, move, 1));
    //             
    //                     break;
    //                 }
    //             case Effect.Poison:
    //                 {
    //                     defender.SetStatusEffects(defender, Effect.Poison);
    //                     break;
    //                 }
    //         }
    //         // if(move.type == Effect.Heal)
    //         // {
    //         //     int heal = attacker.MaxHp / 4;
    //         //
    //         //     attacker.CurrentHP += heal;
    //         //
    //         //     if (attacker.CurrentHP > attacker.MaxHp) attacker.CurrentHP = attacker.MaxHp;
    //         //    
    //         // }
    //     }
    //      
    // }

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

        if (dialResult == 1f)
        {


            float a = (2 * attacker.Level + 10) / 250f;
            float b = a * move.Base.Power * ((float)attack / defence) + 2;
            int damage = Mathf.FloorToInt(b * type * criticalHit * dialResult);

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

    public static float Normalize(Move accuracy, float minAcc, float maxAcc)
    {
        float result = Mathf.Clamp01((accuracy.Base.Accuracy - minAcc) / (maxAcc - minAcc));
        return result;

    }

  // public bool Overlaps(this RectTransform rectTransform1, RectTransform rectTransform2)
  // {
  //     Rect rect1 = new Rect(rectTransform1.localPosition.x, rectTransform1.localPosition.y, rectTransform1.rect.width, rectTransform1.rect.height);
  //     Rect rect2 = new Rect(rectTransform2.localPosition.x, rectTransform2.localPosition.y, rectTransform2.rect.width, rectTransform2.rect.height);
  //
  //     return rect1.Overlaps(rect2);
  // }



}

public static class ExtensionMethod
{
    public static bool Overlaps(this RectTransform rectTransform1, RectTransform rectTransform2)
    {
        //Rect rect1 = new Rect(rectTransform1.localPosition.x, rectTransform1.localPosition.y, rectTransform1.rect.width, rectTransform1.rect.height);
        //Rect rect2 = new Rect(rectTransform2.localPosition.x, rectTransform2.localPosition.y, rectTransform2.rect.width, rectTransform2.rect.height);
        Rect rect1 = GetWorldRect(rectTransform1);
        Rect rect2 = GetWorldRect(rectTransform2);

        return rect1.Overlaps(rect2);
    }

   // public static bool Overlapping(Image green, Image tick)
   // {
   //    
   // }

    static Rect GetWorldRect(RectTransform rectTransform)
    {
        Vector3[] corners = new Vector3[4];
        rectTransform.GetWorldCorners(corners);

        float xMin = Mathf.Min(corners[0].x, corners[1].x, corners[2].x, corners[3].x);
        float xMax = Mathf.Max(corners[0].x, corners[1].x, corners[2].x, corners[3].x);
        float yMin = Mathf.Min(corners[0].y, corners[1].y, corners[2].y, corners[3].y);
        float yMax = Mathf.Max(corners[0].y, corners[1].y, corners[2].y, corners[3].y);

        return new Rect(xMin, yMin, xMax - xMin, yMax - yMin);
    }
}
