using System.Data.Common;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    // manages game states // attached to Game Manager Object
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public int puzzlesComplete;
    public GameObject ghostOne;
    void Start()
    {
        
    }

    public void SetGameState()
    {
        switch (puzzlesComplete)
        {
            default:
            {
                Debug.Log("No puzzles complete, game should start in this state");
                break;
            }

            case 1:
            {
                Debug.Log("First puzzle complete, trigger setup of next puzzle");

                string[] ghostOneDialogue =
                {
                    "Oh...you can SEE ME??", "In that case, we must not waste a second.",
                    "You see, I have been a spirit for what feels like an age, and I have forgotten my own name...",
                    "BUT! I had my initials engraved on a pipe I used to smoke long ago.",
                    "That very pipe may still be in this house! Please, find my old pipe and return to me when you know my initials."
                };
                ghostOne = GameObject.FindGameObjectWithTag("GhostOne");
                ghostOne.GetComponent<CharacterDialogue>().SetDialogueLines(ghostOneDialogue);
                break;
            }

            case 2:
            {
                Debug.Log("Second Puzzle complete, trigger setup of next puzzle");
                string[] ghostOneCompletedPuzzleLines =
                {
                    "Oh! Back so soon?", "You found my pipe already?", "Excellent! W.C you say...", "...", ".....", "........",
                    "That doesn't ring a bell.", "...", "Oh Well! I have always wanted to try a new name for myself!", "Thank you for your help."
                };
                ghostOne.GetComponent<CharacterDialogue>().SetDialogueLines(ghostOneCompletedPuzzleLines);
                break;
            }

            case 3:
            {
                Debug.Log("Third puzzle complete, trigger game completed");
                break;
            }
                return;
        }
    }

    public void TriggerNextPuzzle()
    {
        puzzlesComplete++;
        SetGameState();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P)) // for debugging 
        {
            TriggerNextPuzzle();
        }
    }

    public void ChangeScene(string SceneName)
    {
        SceneManager.LoadScene(SceneName);
    }
}
