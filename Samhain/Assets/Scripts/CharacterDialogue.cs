using UnityEngine;

public class CharacterDialogue : Interactable
{
    [TextArea(3, 10)]
    public string[] characterDialogueLines;  // Holds unique lines for each character
    

    public override void Use()
    {
        Dialogue.instance.TriggerDialogue(characterDialogueLines);
    }

    
}