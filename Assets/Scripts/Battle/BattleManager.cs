using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
public enum BattleState { Start, PlayerMove, EnemyMove, PlayerAction, Busy, End }

public class BattleManager : MonoBehaviour
{

    [SerializeField] BattleUnit playerUnit;
    [SerializeField] BattleUnit enemyUnit;
    [SerializeField] BattleHud playerHud;
    [SerializeField] BattleHud enemyHud;
    [SerializeField] BattleDialogue battleDialogue;
    [SerializeField] Camera playerCamera;
    [SerializeField] Camera battleCamera;

    ButtonHandler buttonHandler;
    EncounterList encounterList;
    [SerializeField] PlayerController playerController;

    bool inBattle = false;


    BattleState state;
    // Start is called before the first frame update
    void Start()
    {
        

        EventBus.Subscribe<Encounter>(SetupBattle);
        battleCamera.gameObject.SetActive(false);
       
    }

    void SetupBattle(Encounter eventData)
    {

        inBattle = true;

        buttonHandler = new ButtonHandler();
        encounterList = playerController.currentEncounterList; //retrieves the current encounter box that player has collided with

        GameState gameState = new GameState(inBattle, playerCamera, battleCamera, playerController);
        EventBus.Raise(gameState);

        ApostleBase wildApostle = eventData.WildApostle;
       
        //change this to players party apostle!!! when its done)
        playerUnit.SetUp(wildApostle, 5, true);
        playerHud.SetHudData(playerUnit.Apostle);

       
        enemyUnit.SetUp(wildApostle, encounterList.ApostleLevelCalc(encounterList.MinLevel, encounterList.MaxLevel), false);
        enemyHud.SetHudData(enemyUnit.Apostle);
        


        Debug.Log(playerUnit.Apostle.Moves);

        StartCoroutine(BattleStart());
      

        

        //state = playerUnit.Apostle.Speed >= enemyUnit.Apostle.Speed ? state = BattleState.PlayerMove : state = BattleState.EnemyMove;
    }

    IEnumerator BattleStart()
    {
        yield return (battleDialogue.TypeDialogue($"{enemyUnit.Apostle.ApostleBase.name} confronts you..."));
        
        
       
        
        PlayerAction();

        
       
    }


  

    void PlayerAction()
    {
        state = BattleState.PlayerAction;
        StartCoroutine(battleDialogue.TypeDialogue("Select an Action: "));
        battleDialogue.EnableActionSelector(true);

        buttonHandler.buttonList = new ButtonHandler.MyButton[2];

        buttonHandler.buttonList[0].image = GameObject.Find("FightAction").GetComponent<Image>();
        buttonHandler.buttonList[0].image.color = Color.yellow;
        buttonHandler.buttonList[0].action = PlayerMove;

        buttonHandler.buttonList[1].image = GameObject.Find("Flee").GetComponent<Image>();
        buttonHandler.buttonList[1].image.color = Color.yellow;
        buttonHandler.buttonList[1].action = Flee;
       


    }


    void PlayerMove()
    {
        state = BattleState.PlayerMove;
        battleDialogue.EnableActionSelector(false);
        battleDialogue.EnableDialogueText(false);
        battleDialogue.EnableMoveSelector(true);


        buttonHandler.buttonList = new ButtonHandler.MyButton[4];
        
        buttonHandler.buttonList[0].image = GameObject.Find("Move1").GetComponent<Image>();
        buttonHandler.buttonList[0].image.color = Color.yellow;
        buttonHandler.buttonList[0].action = SelectMove;
        
        buttonHandler.buttonList[1].image = GameObject.Find("Move2").GetComponent<Image>();
        buttonHandler.buttonList[1].image.color = Color.yellow;
        buttonHandler.buttonList[1].action = SelectMove;

        buttonHandler.buttonList[2].image = GameObject.Find("Move3").GetComponent<Image>();
        buttonHandler.buttonList[2].image.color = Color.yellow;
        buttonHandler.buttonList[2].action = SelectMove;

        buttonHandler.buttonList[3].image = GameObject.Find("Move4").GetComponent<Image>();
        buttonHandler.buttonList[3].image.color = Color.yellow;
        buttonHandler.buttonList[3].action = SelectMove;

        //buttonHandler.MakeButton(playerUnit.apostle.Moves, GameObject.Find("Move1"));

        battleDialogue.SetMoveNames(playerUnit.Apostle.Moves);


    }

    void SelectMove()
    {
        StartCoroutine(PerformPlayerMove());
    }

    IEnumerator PerformPlayerMove()
    {
        state = BattleState.Busy;

        battleDialogue.EnableMoveSelector(false);
        battleDialogue.EnableDialogueText(true);


        var move = playerUnit.Apostle.Moves[buttonHandler.selectedButton];
        yield return battleDialogue.TypeDialogue($"{playerUnit.Apostle.ApostleBase.Name} used {move.Base.Name}");

        

        //bool isFainted = enemyUnit.apostle.TakeDamage(move, playerUnit.apostle);
        //enemyHud.UpdateHPBar();

        TakeDamage damageTaken = new TakeDamage(move, playerUnit.Apostle, enemyUnit.Apostle);
        EventBus.Raise(damageTaken);

        if (enemyUnit.Apostle.CurrentHP <= 0)
        {
            yield return battleDialogue.TypeDialogue($"{enemyUnit.Apostle.ApostleBase.Name} FUCKING DIED GRAHHHHHH");
        }
        else
        {
            StartCoroutine(PerformEnemyMove());
            buttonHandler.ButtonReset();
        }
    }

    IEnumerator PerformEnemyMove()
    {
        state = BattleState.EnemyMove;

        var move = enemyUnit.Apostle.RandomMove();
        yield return battleDialogue.TypeDialogue($"{enemyUnit.Apostle.ApostleBase.Name} used {move.Base.Name}");

       

        //bool isFainted = playerUnit.apostle.TakeDamage(move, playerUnit.apostle);

        TakeDamage damageTaken = new TakeDamage(move, enemyUnit.Apostle, playerUnit.Apostle);
        EventBus.Raise(damageTaken);

        //playerHud.UpdateHPBar();

        if (playerUnit.Apostle.CurrentHP <= 0)
        {
            yield return battleDialogue.TypeDialogue($"{playerUnit.Apostle.ApostleBase.Name} FUCKING DIED GRAHHHHHH");
        }
        else
        {
            PlayerAction();
            buttonHandler.ButtonReset();
        }
    }
    void Flee()
    {
        Debug.Log("hello");
        inBattle = false;
        GameState gameState = new GameState(inBattle, playerCamera, battleCamera, playerController);
        EventBus.Raise(gameState);
    }

    void EndBattle()
    {

    }

    private void Update()
    {
        if (inBattle == true)
        {
           
            buttonHandler.MenuNavigation(buttonHandler.buttonList);
            //Debug.Log(buttonHandler.buttonList.ToString());
            
           // battleDialogue.MoveDescription(buttonHandler.selectedButton, playerUnit.Apostle.Moves[buttonHandler.selectedButton]);

        }

    }

  // private void MenuNavigation()
  // {
  //     if (Input.GetKeyDown(KeyCode.DownArrow))
  //     {
  //         buttonHandler.MoveToNextButton();
  //     }
  //     else if (Input.GetKeyDown(KeyCode.UpArrow))
  //     {
  //         buttonHandler.PreviousButton();
  //     }
  //
  //     if (Input.GetKeyDown(KeyCode.Space))
  //     {
  //         buttonHandler.buttonList[buttonHandler.selectedButton].action();
  //     }
  // }
}