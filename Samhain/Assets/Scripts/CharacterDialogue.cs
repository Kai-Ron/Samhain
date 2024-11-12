using UnityEngine;

public class CharacterDialogue : MonoBehaviour
{
    [TextArea(3, 10)]
    public string[] characterDialogueLines;  // Holds unique lines for each character
}