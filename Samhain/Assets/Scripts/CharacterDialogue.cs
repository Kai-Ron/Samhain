using UnityEngine;

public class CharacterDialogue : Interactable
{
    [TextArea(3, 10)]
    public string[] characterDialogueLines;  // Holds unique lines for each character

    [TextArea(3, 10)]
    public string[] correctDialogueLines;  // Holds unique lines for each character

    [TextArea(3, 10)]
    public string[] incorrectDialogueLines;  // Holds unique lines for each character

    [TextArea(3, 10)]
    public string[] completeDialogueLines;  // Holds unique lines for each character

    [TextArea(3, 10)]
    public string[] hintDialogueLines;  // Holds unique lines for each character

    public string characterName;

    public bool correctlyGuessed;
    private int incorrectGuesses;
    public int hintThreshold;

    public override void Use()
    {
        if (!correctlyGuessed)
        {
            InputManager.instance.currentName = characterName;
            InputManager.instance.currentCharacter = GetComponent<CharacterDialogue>();
            InputManager.instance.ActivateInputField();
            if (incorrectGuesses >= hintThreshold)
            {
                string[] singleListItem = new string[1];
                if (Mathf.Abs(hintThreshold - incorrectGuesses) > hintDialogueLines.Length - 1)
                {
                    singleListItem[0] = hintDialogueLines[hintDialogueLines.Length - 1];
                    Dialogue.instance.TriggerDialogue(singleListItem);
                    return;
                }
                singleListItem[0] = hintDialogueLines[Mathf.Abs(hintThreshold - incorrectGuesses)];
                Dialogue.instance.TriggerDialogue(singleListItem);
                return;
            }
            Dialogue.instance.TriggerDialogue(characterDialogueLines);
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
    }

    public void IncorrectName(string[] newDialogueLines)
    {
        incorrectGuesses += 1;
        Dialogue.instance.TriggerDialogue(newDialogueLines);
    }
}