using System.Data.Common;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    // manages game states // attached to Game Manager Object
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public int puzzlesComplete;
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

                case 1 :
            {
                Debug.Log("First puzzle complete, trigger setup of next puzzle");
                break;
            }
                
            case 2:
            {
                Debug.Log("Second Puzzle complete, trigger setup of next puzzle");
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
}
