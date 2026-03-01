using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState : EventData
{

    public enum PlayerState { inBattle, onMap }
    public GameState(bool inBattle, Camera playerCamera, Camera battleCamera, PlayerController playerController)
    {
        if (inBattle == true)
        {
            playerCamera.gameObject.SetActive(false);
            battleCamera.gameObject.SetActive(true);
            playerController.gameObject.SetActive(false);
        }
        else if(inBattle == false)
        {
            playerCamera.gameObject.SetActive(true);
            battleCamera.gameObject.SetActive(false);
            playerController.gameObject.SetActive(true);
        }

  
    }

}
