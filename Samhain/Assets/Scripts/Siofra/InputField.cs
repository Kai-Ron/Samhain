using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InputField : MonoBehaviour
{
    [SerializeField] private Button sendButton;
    [SerializeField] private TMP_InputField inputField;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        sendButton.onClick.AddListener(EnterText);
    }

    void EnterText()
    {
        InputManager.instance.lastText = inputField.text;
        InputManager.instance.FinishedTyping();
    }
}
