using UnityEngine;
using TMPro;
using System.Collections;

public class Dialogue : MonoBehaviour
{
    public static Dialogue instance;

    [TextArea(3, 10)]
    public string[] defaultDialogueLines;

    public GameObject inputField;
    public TextMeshProUGUI dialogueText;
    public GameObject dialogueUI;
    public TextMeshProUGUI progressUIText;
    public int NamesSolved;

    private PlayerController playerScript;
    
    private string[] currentDialogueLines;
    private int currentLineIndex = 0;
    private bool isTalking = false;
    private Coroutine typingCoroutine;
    
    public float typingSpeed = 0.05f; // Speed of typewriter effect
    public GameObject itemDialogueIndicator;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }
    }

    void Start()
    {
        if (dialogueUI != null)
        {
            dialogueUI.SetActive(false);
        }

        playerScript = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
    }

    void Update()
    {
        if (isTalking && Input.GetMouseButtonDown(0)/*GetKeyDown(KeyCode.Space)*/)
        {
            if (typingCoroutine == null) // If typing is done, show the next line
            {
                DisplayNextLine();
            }
            else // If typing is still ongoing, finish it immediately
            {
                StopCoroutine(typingCoroutine);
                typingCoroutine = null;
                dialogueText.maxVisibleCharacters = dialogueText.text.Length;
            }
        }
    }

    // Method to start dialogue from a specific character
    public void StartDialogue(string[] newDialogueLines)
    {
        
            if (newDialogueLines.Length > 0)
            {
                currentDialogueLines = newDialogueLines;
                isTalking = true;
                currentLineIndex = 0;
                dialogueUI.SetActive(true);
                DisplayNextLine();
            }
            
    }
    
    void DisplayNextLine()
    {
        if (currentLineIndex < currentDialogueLines.Length)
        {
           
            if (typingCoroutine != null)
            {
                StopCoroutine(typingCoroutine);
            }

            dialogueText.text = currentDialogueLines[currentLineIndex];
            dialogueText.maxVisibleCharacters = 0; 
            typingCoroutine = StartCoroutine(RevealCharacters());
            currentLineIndex++;
        }
        else
        {
            EndDialogue();
        }
    }

    // Coroutine to reveal characters one by one
    private IEnumerator RevealCharacters()
    {
        for (int i = 0; i <= dialogueText.text.Length; i++)
        {
            dialogueText.maxVisibleCharacters = i;
            yield return new WaitForSeconds(typingSpeed);
        }
        typingCoroutine = null; // Reset coroutine flag after typing is done
    }

    // Method to end the dialogue
    void EndDialogue()
    {
        isTalking = false;
        dialogueUI.SetActive(false);
        inputField.SetActive(false);
        playerScript.currentState = PlayerController.STATE.HAS_CONTROL;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        dialogueText.text = "";
        currentDialogueLines = new string[0];
    }

    // When a character triggers this dialogue, it should call this method
    public void TriggerDialogue(string[] characterDialogueLines)
    {
        playerScript.currentState = PlayerController.STATE.NO_CONTROL;
        StartDialogue(characterDialogueLines);
    }

    public void TriggerItemDialogue(string[] characterDialogueLines)
    {
        itemDialogueIndicator.SetActive(true);
        StartDialogue(characterDialogueLines);
    }

    public void UpdateProgressUI()
    {
        progressUIText.text = NamesSolved.ToString();
    }
}
