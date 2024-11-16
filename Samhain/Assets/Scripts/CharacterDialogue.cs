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

    private Transform playerTransform;

    private void Start()
    {
        playerTransform = GameObject.FindWithTag("Player").transform;
    }

    public override void Use()
    {
        Vector3 dir = playerTransform.position - transform.position;
        dir.y = 0;
        transform.rotation = Quaternion.LookRotation(dir);

        if (!correctlyGuessed)
        {
            InputManager.instance.currentName = characterName;
            InputManager.instance.currentCharacter = GetComponent<CharacterDialogue>();
            InputManager.instance.ActivateInputField();
            if (incorrectGuesses >= hintThreshold && hintThreshold != 0)
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
        Dialogue.instance.NamesSolved++;
        Dialogue.instance.UpdateProgressUI();
    }

    public void IncorrectName(string[] newDialogueLines)
    {
        incorrectGuesses += 1;
        Dialogue.instance.TriggerDialogue(newDialogueLines);
    }
}