using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class BattleDialogue : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI dialogueText;
    [SerializeField] float lettersPerSecond;

    [SerializeField] GameObject actionSelector;
    [SerializeField] GameObject moveSelector;
    [SerializeField] GameObject moveDetails;

    [SerializeField] List<TextMeshProUGUI> actionTexts;
    [SerializeField] List<TextMeshProUGUI> moveTexts;

    [SerializeField] TextMeshProUGUI typeText;


   // public void SetBDialogue(string dialogue)
   // {
   //     dialogueText.text = dialogue;
   // }

    public IEnumerator TypeDialogue(string dialogue)
    {
        dialogueText.text = "";
        foreach(var letter in dialogue.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(1 / lettersPerSecond);
        }
        yield return new WaitForSeconds(1f);
    }

    public void EnableActionSelector(bool enable)
    {
        actionSelector.SetActive(enable);
    }

    public void EnableMoveSelector(bool enable)
    {
        moveSelector.SetActive(enable);
        moveDetails.SetActive(enable);
    }

    public void EnableDialogueText(bool enable)
    {
        dialogueText.enabled = enable;
    }

    public void SetMoveNames(List<Move> moves)
    {
        if (moves != null)
        {
            for (int i = 0; i < moveTexts.Count; ++i)
            {
                if (i < moves.Count)
                {
                    moveTexts[i].text = moves[i].Base.Name;
                }
                else
                {
                    moveTexts[i].name = "-";
                }
            }
        }
    }

    public void MoveDescription(int selectedMove, Move move)
    {

            typeText.text = move.Base.Type.ToString();
        
        
    }
}
