using UnityEngine;
using System;

public class CharacterDialogue : Interactable
{
    [TextArea(3, 10)]
    public string[] interactionLines;
    
    [TextArea(3, 10)]
    public string[] characterDialogueLines;  // Holds unique lines for each character

    [TextArea(3, 10)]
    public string[] correctDialogueLines;  // Holds unique lines for each character

    [TextArea(3, 10)]
    public string[] incorrectDialogueLines;  // Holds unique lines for each character

    [TextArea(3, 10)]
    public string[] completeDialogueLines;  // Holds unique lines for each character

    [TextArea(3, 10)]
    public string[] hint0;

    [TextArea(3, 10)]
    public string[] hint1;

    [TextArea(3, 10)]
    public string[] hint2;

    [TextArea(3, 10)]
    public string[][] hintDialogueLines;  // Holds unique lines for each character

    [TextArea(3, 10)]
    string[] singleListItem = new string[8];

    public string characterName;

    public bool correctlyGuessed;
    private int incorrectGuesses;
    private int interaction = 0;
    public int hintThreshold = 3;

    private Transform playerTransform;

    private void Start()
    {
        playerTransform = GameObject.FindWithTag("Player").transform;
        /*hintDialogueLines[0] = hint0;
        hintDialogueLines[1] = hint1;
        hintDialogueLines[2] = hint2;*/
    }

    public override void Use()
    {
        Vector3 dir = playerTransform.position - transform.position;
        dir.y = 0;
        transform.rotation = Quaternion.LookRotation(-dir);

        if (!correctlyGuessed)
        {
            InputManager.instance.currentName = characterName;
            InputManager.instance.currentCharacter = GetComponent<CharacterDialogue>();

            if(interaction == 0)
            {
                InputManager.instance.Interaction();
                Dialogue.instance.TriggerDialogue(characterDialogueLines);
                interaction++;
            }
            else
            {
                InputManager.instance.ActivateInputField();
                Dialogue.instance.TriggerDialogue(interactionLines);
            }
        }
        else
        {
            Dialogue.instance.TriggerDialogue(completeDialogueLines);
        }

    }
    
    // Method to replace the entire set of dialogue lines
    public void SetDialogueLines(string[] newDialogueLines)
    {
        characterDialogueLines = newDialogueLines;
    }

    public void CorrectName(string[] newDialogueLines)
    {
        correctlyGuessed = true;
        Dialogue.instance.TriggerDialogue(newDialogueLines);
        Dialogue.instance.NamesSolved++;
        Dialogue.instance.UpdateProgressUI();
    }

    public void IncorrectName(string[] newDialogueLines)
    {
        int hint = 0;

        for(int i = 0; i < incorrectDialogueLines.Length; i++)
        {
            singleListItem[hint] = incorrectDialogueLines[i];
            hint++;
        }

        if(incorrectGuesses > 0)
        {
            for(int i = 0; i < hint0.Length; i++)
            {
                singleListItem[hint] = hint0[i];
                hint++;
            }
        }

        if(incorrectGuesses > 1)
        {
            for(int i = 0; i < hint1.Length; i++)
            {
                singleListItem[hint] = hint1[i];
                hint++;
            }
        }

        if(incorrectGuesses > 2)
        {
            for(int i = 0; i < hint2.Length; i++)
            {
                singleListItem[hint] = hint2[i];
                hint++;
            }
        }
        
        if(incorrectGuesses < hintThreshold)
        {
            incorrectGuesses += 1;
        }

        Dialogue.instance.TriggerDialogue(singleListItem);
    }
}