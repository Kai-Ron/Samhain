using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static InputManager instance;
    private PlayerController playerScript;
    public string lastText;

    public GameObject inputFieldObject;

    public enum INPUT_STATE
    {
        TYPING,
        NOT_TYPING,
    }

    private INPUT_STATE currentState;

    private void Start()
    {
        playerScript = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
    }

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

    public void ActivateInputField()
    {
        currentState = INPUT_STATE.TYPING;
        playerScript.currentState = PlayerController.STATE.TYPING;
        inputFieldObject.SetActive(true);
    }

    public void FinishedTyping()
    {
        currentState = INPUT_STATE.NOT_TYPING;
        playerScript.currentState = PlayerController.STATE.HAS_CONTROL;
        inputFieldObject.SetActive(false);
    }
}
