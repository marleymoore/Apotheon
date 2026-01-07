using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class ButtonHandler : MonoBehaviour
{
    public delegate void ButtonAction();
    public MyButton[] buttonList;
    public int selectedButton = 0;
    ButtonHandler.MyButton[] newButtonList;

    public void MoveToNextButton()
    {
        buttonList[selectedButton].image.color = Color.black;

        selectedButton++;

        if (selectedButton >= buttonList.Length)
        {
            selectedButton = 0;
        }

        buttonList[selectedButton].image.color = Color.blue;
    }

    public void PreviousButton()
    {
        buttonList[selectedButton].image.color = Color.black;
        selectedButton--;
        if (selectedButton < 0)
        {
            selectedButton = (buttonList.Length - 1);
        }
        buttonList[selectedButton].image.color = Color.blue;
    }

    public void ButtonReset()
    {
        selectedButton = 0;
    }
    public struct MyButton
    {
        public Image image;
        public ButtonAction action;
    }

    public void MenuNavigation(ButtonHandler.MyButton[] buttonHandler)
    {
        //newButtonList = buttonHandler;

     if (Input.GetKeyDown(KeyCode.DownArrow))
      {
          MoveToNextButton();
      }
      else if (Input.GetKeyDown(KeyCode.UpArrow))
      {
         PreviousButton();
      }
 
      if (Input.GetKeyDown(KeyCode.Space))
      {
          buttonList[selectedButton].action();
      }
    }

    public void DestroyList(ButtonHandler.MyButton[] button)
    {

    }

   // private void Update()
   // {
   //     if (buttonList != null)
   //     {
   //         MenuNavigation(buttonList);
   //         Debug.Log(buttonList);
   //     }
   // }

   // public void MakeButton(List<Move> moveList, GameObject buttonName)
   // {
   //    
   //     for (int i = buttonList.Length; i < moveList.Count; i++)
   //     {
   //
   //         buttonList[i].image = GameObject.FindGameObjectWithTag("MoveImage").GetComponent<Image>();
   //         buttonList[i].image.color = Color.yellow;
   //     }
   //
   //
   // }
}
