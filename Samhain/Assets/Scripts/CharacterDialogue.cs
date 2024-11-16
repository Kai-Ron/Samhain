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

    public string characterName;

    public bool correctlyGuessed;

    public override void Use()
    {
        if (!correctlyGuessed)
        {
            InputManager.instance.currentName = characterName;
            InputManager.instance.currentCharacter = GetComponent<CharacterDialogue>();
            Dialogue.instance.TriggerDialogue(characterDialogueLines);
            InputManager.instance.ActivateInputField();
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
        Dialogue.instance.TriggerDialogue(newDialogueLines);
    }
}