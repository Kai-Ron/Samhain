using UnityEngine;

public class Item : Interactable
{
    [TextArea(3, 10)]
    public string[]itemInformationLines;
    private Vector3 originalPosition;
    private Quaternion originalRotation;
    private Transform cameraTransform;
    private PlayerController playerScript;

    public void Start()
    {
        originalPosition = transform.position;
        originalRotation = transform.rotation;

        cameraTransform = GameObject.FindWithTag("MainCamera").transform;
        playerScript = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
    }

    public override void Use()
    {
        Dialogue.instance.TriggerItemDialogue(itemInformationLines);
        playerScript.heldItem = gameObject;

        Vector3 newItemPosition = cameraTransform.transform.position + cameraTransform.transform.forward * 1.5f;
        transform.position = newItemPosition;
    }

    public void ReturnToPosition()
    {
        transform.position = originalPosition;
        transform.rotation = originalRotation;
    }
}
