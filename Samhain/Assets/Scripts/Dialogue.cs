using UnityEngine;
using TMPro;  

public class Dialogue : MonoBehaviour
{
    
    // this script is attached to the Game Manager object in the scene 
    // when the player interacts with a talking character, it should look for a "CharacterDialogue" script on that object and populate the UI with 
    // that objects assigned text 
    [TextArea(3, 10)]
    public string[] defaultDialogueLines; // Default lines needed for inspector [ignore]

    public TextMeshProUGUI dialogueText;  // Reference to the textmeshpro for the dialogue text 
    public GameObject dialogueUI;         // Dialogue box UI panel (can hide/show)
    
    private string[] currentDialogueLines; // Holds the active character's dialogue
    private int currentLineIndex = 0;      // Tracks the current line being displayed
    private bool isTalking = false;        // Tracks if the dialogue is active or not

    void Start()
    {
        if (dialogueUI != null)
        {
            dialogueUI.SetActive(false);  // Hide dialogue UI initially
        }
    }

    void Update()
    {
        // If dialogue is active and the player presses space, show the next line
        if (isTalking && Input.GetKeyDown(KeyCode.Space))
        {
            DisplayNextLine();
        }
    }

    // Method to start dialogue from a specific character
    public void StartDialogue(string[] newDialogueLines)
    {
        if (newDialogueLines.Length > 0)
        {
            currentDialogueLines = newDialogueLines;  // Set the character's lines
            isTalking = true;
            currentLineIndex = 0;                     // Reset line index
            dialogueUI.SetActive(true);               // Show dialogue UI
            DisplayNextLine();                        // Display the first line
        }
    }

    // Method to display the next line in the dialogue
    void DisplayNextLine()
    {
        if (currentLineIndex < currentDialogueLines.Length)
        {
            // Display the current line and increment index
            dialogueText.text = currentDialogueLines[currentLineIndex];
            currentLineIndex++;
        }
        else
        {
            // End dialogue if all lines are shown
            EndDialogue();
        }
    }

    // Method to end the dialogue
    void EndDialogue()
    {
        isTalking = false;
        dialogueUI.SetActive(false);  // Hide dialogue UI
        dialogueText.text = "";       // Clear text box
        currentDialogueLines = new string[0];  // Clear dialogue lines
    }

    // When a character triggers this dialogue, it should call this method
    public void TriggerDialogue(string[] characterDialogueLines)
    {
        StartDialogue(characterDialogueLines);
    }
    
}


